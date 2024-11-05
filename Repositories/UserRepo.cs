using intern_prj.Entities;
using intern_prj.Helper.jwtSerivce;
using intern_prj.Repositories.interfaces;
using intern_prj.Services.interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace intern_prj.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly DecorContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartService _cartService;

        public UserRepo(DecorContext context, ICartService cartService,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _cartService = cartService;
        }
        public async Task<List<ApplicationUser>> GetUsers_Admin()
        {
                var users = await _userManager.Users.ToListAsync();
                return users;
        }

        public async Task<ApplicationUser?> GetUser_Admin(string id)
        {
                var user = await _userManager.FindByIdAsync(id);
                return user ?? null;
        }

        public async Task<ApplicationUser?> CreateUserAsync_Admin(ApplicationUser userEntity, string password)
        {
                if(userEntity.Email == null)
                {
                    return null;
                }
                else
                {
                    var existingUser = await _userManager.FindByEmailAsync(userEntity.Email);
                    if(existingUser == null)
                    {
                        var res = await _userManager.CreateAsync(userEntity, password);
                        if (res.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(userEntity, AppRole.Customer);
                            await _cartService.InitCart(userEntity.Id);
                            return userEntity;
                        }
                        else return null;
                    }
                    else
                    {
                        return null;
                    }
                }
        }

        public async Task<ApplicationUser?> UpdateUserAsync_Admin(ApplicationUser userEntity)
        {
                var result = await _userManager.UpdateAsync(userEntity);
                return result.Succeeded ? userEntity : null;
        }

        public async Task<bool> DeleteUserAsync_Admin(string id)
        {
                var userDelete = await _userManager.FindByIdAsync(id);
                if (userDelete != null)
                {
                    var result =  await _userManager.DeleteAsync(userDelete);
                    return result.Succeeded;
                }
                else
                {
                    return false;
                }
        }

        public async Task<ApplicationUser?> GetUserAsync_Client(string id)
        {
                var user = await _userManager.FindByIdAsync(id);
                return user != null ? user : null;
        }

        public async Task<ApplicationUser?> UpdateUserAsync_Client(ApplicationUser userEntity)
        {
                var updateResult = await _userManager.UpdateAsync(userEntity);
                return updateResult.Succeeded ? userEntity : null;
        }

        public async Task<ApplicationUser?> ChangePassword_Client(string id, string oldPassword, string newPassword)
        {
                var user = await _userManager.FindByIdAsync(id);
                if(user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                    return result.Succeeded ? user : null;
                }
                else
                {
                    return null;
                }
        }
    }
}
