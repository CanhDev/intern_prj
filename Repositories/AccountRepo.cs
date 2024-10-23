using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Helper.jwtSerivce;
using intern_prj.Repositories.interfaces;
using intern_prj.Services;
using intern_prj.Services.interfaces;
using Microsoft.AspNetCore.Identity;

namespace intern_prj.Repositories
{
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountRepo(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            DecorContext context,
            
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApplicationUser?> CheckAccount(LoginRes model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
                if (user == null || !passwordValid) return null;
                return user;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateAccount(ApplicationUser userAccount, string password)
        {
            var result = await _userManager.CreateAsync(userAccount, password);
            return result.Succeeded;
        }
    }
}
