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
            try
            {
                var users = await _userManager.Users.ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApplicationUser?> GetUser_Admin(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                return user ?? null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApplicationUser?> CreateUserAsync_Admin(ApplicationUser userEntity, string password)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApplicationUser?> UpdateUserAsync_Admin(ApplicationUser userEntity)
        {
            try
            {
                var result = await _userManager.UpdateAsync(userEntity);
                return result.Succeeded ? userEntity : null;
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }

        public async Task<bool> DeleteUserAsync_Admin(string id)
        {
            try
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApplicationUser?> GetUserAsync_Client(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                return user != null ? user : null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ApplicationUser?> UpdateUserAsync_Client(ApplicationUser userEntity)
        {
            try
            {
                var updateResult = await _userManager.UpdateAsync(userEntity);
                return updateResult.Succeeded ? userEntity : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApplicationUser?> ChangePassword_Client(string id, string oldPassword, string newPassword)
        {
            try
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
