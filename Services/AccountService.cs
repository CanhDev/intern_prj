using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Helper.jwtSerivce;
using intern_prj.Repositories;
using intern_prj.Repositories.interfaces;
using intern_prj.Services.interfaces;
using Microsoft.AspNetCore.Identity;

namespace intern_prj.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICartService _cartService;
        private readonly JwtHelper _jwtHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(IAccountRepo accountRepo,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ICartService cartService,
            JwtHelper jwtHelper,
            IHttpContextAccessor httpContextAccessor)
        {
            _accountRepo = accountRepo;
            _userManager = userManager;
            _roleManager = roleManager;
            _cartService = cartService;
            _jwtHelper = jwtHelper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Api_response> SignUpAsync(SignUpRes model)
        {
            try
            {
                var request = _httpContextAccessor.HttpContext?.Request;
                var defaultImageUrl = $"{request.Scheme}://{request.Host}/resource/images/default/no_avatar.png";

                // check exist email
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    return new Api_response
                    {
                        success = false,
                        message = "Email đã tồn tại. Vui lòng sử dụng email khác."
                    };
                }
                var userAccount = new ApplicationUser
                {
                    FirstName = model.fname,
                    LastName = model.lname,
                    Email = model.Email,
                    UserName = model.Email,
                    avatarUrl = defaultImageUrl
                };
                var result = await _accountRepo.CreateAccount(userAccount, model.Password);
                if (result)
                {
                    if (!await _roleManager.RoleExistsAsync(AppRole.Customer))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(AppRole.Customer));
                    }
                    if (!await _roleManager.RoleExistsAsync(AppRole.Admin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(AppRole.Admin));
                    }
                    if (userAccount.Email == "Admin@gmail.com")
                    {
                        await _userManager.AddToRoleAsync(userAccount, AppRole.Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(userAccount, AppRole.Customer);
                    }

                    await _cartService.InitCart(userAccount.Id);
                }
                return new Api_response
                {
                    success = result,
                    message = result ? "SignUp successful" : "SignUp failed"
                };
            }
            catch (Exception ex)
            {
                return new Api_response
                {
                    success = false,
                    message = ex.Message,
                };
            }

        }

        public async Task<Api_response> LoginAsync(LoginRes model)
        {
            try
            {
                var accountValid = await _accountRepo.CheckAccount(model);
                if (accountValid != null)
                {
                    var result = await _jwtHelper.GenerateToken(accountValid);
                    return new Api_response
                    {
                        success = true,
                        message = "Login successfull",
                        data = new
                        {
                            token = result,
                            userInfo = new UserReq
                            {
                                Id = accountValid.Id,
                                FirstName = accountValid.FirstName,
                                LastName = accountValid.LastName,
                                Email = accountValid.Email ?? "",
                                avatarUrl = accountValid.avatarUrl,
                                PhoneNum = accountValid.PhoneNum,
                                Address = accountValid.Address,
                            }
                        }
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                        message = "Wrong password or username"
                    };
                }
            }
            catch(Exception ex)
            {
                return new Api_response
                {
                    success = false,
                    message = ex.Message,
                };
            }
        }

        public async Task<Api_response> RenewTokenAsync(TokenModel model)
        {
            return await _jwtHelper.RefreshToken(model);
        }
    }
}
