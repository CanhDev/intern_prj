using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Helper.jwtSerivce;
using intern_prj.Repositories.interfaces;
using Microsoft.AspNetCore.Identity;

namespace intern_prj.Repositories
{
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtHelper _jwtHelper;
        private readonly ICartRepo _cartRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountRepo(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            DecorContext context,
            JwtHelper jwtHelper,
            ICartRepo cartRepo,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtHelper = jwtHelper;
            _cartRepo = cartRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Api_response> SignUpAsync(SignUpRes model)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            var defaultImageUrl = $"{request.Scheme}://{request.Host}/resource/images/default/no_avatar.png";

            // Kiểm tra xem email đã tồn tại hay chưa
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

            var result = await _userManager.CreateAsync(userAccount, model.Password);
            if (result.Succeeded)
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

                await _cartRepo.InitCart(userAccount.Id);
            }
            return new Api_response
            {
                success = result.Succeeded,
                message = result.Succeeded ? "SignUp successful" : "SignUp failed"
            };
        }

        public async Task<Api_response> LoginAsync(LoginRes model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if(user == null || !passwordValid)
            {
                return new Api_response
                {
                    success = false,
                    message = "Sai tên đăng nhập hoặc mật khẩu"
                };
            }
            else
            {
                var result = await _jwtHelper.GenerateToken(user);
                return new Api_response
                {
                    success = true,
                    message = "Login successfull",
                    data = new {
                        token = result,
                        userInfo = new UserReq
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            avatarUrl = user.avatarUrl,
                            PhoneNum = user.PhoneNum,
                            Address = user.Address,
                        }
                    }
                };
            }
        }

        public async Task<Api_response> RenewTokenAsync(TokenModel model)
        {
            return await _jwtHelper.RefreshToken(model);
        }

    }
}
