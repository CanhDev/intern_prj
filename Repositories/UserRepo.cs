using AutoMapper;
using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Helper.jwtSerivce;
using intern_prj.Repositories.interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace intern_prj.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly DecorContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ImageHepler _imageHepler;

        public UserRepo(DecorContext context, IMapper mapper,
            UserManager<ApplicationUser> userManager
           ,RoleManager<IdentityRole> roleManager
           ,ImageHepler imageHepler)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _imageHepler = imageHepler;
        }
        public async Task<Api_response> GetUsers_Admin()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<List<UserReq>>(users)
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

        public async Task<Api_response> GetUser_Admin(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if(user == null)
                {
                    return new Api_response
                    {
                        success = false,
                        message = "User does not exist"
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = true,
                        data = _mapper.Map<UserReq>(user)
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

        public async Task<Api_response> CreateUserAsync_Admin(UserRes userRes)
        {
            try
            {
                var user = _mapper.Map<ApplicationUser>(userRes);
                if(userRes.avatarImage != null)
                {
                    var imageUrl = await _imageHepler.saveImage(userRes.avatarImage, "avatars");
                    user.avatarUrl = imageUrl;
                }
                var res = await _userManager.CreateAsync(user, userRes.Password);
                if (res.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, AppRole.Admin);
                    return new Api_response
                    {
                        success = true,
                        data = _mapper.Map<UserReq>(user)
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                        message = "Create User fail"
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
        public async Task<Api_response> UpdateUserAsync_Admin(UserRes userRes, string id, string? newPassword)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    _mapper.Map(userRes, user);
                    if (userRes.avatarImage != null)
                    {
                        var imageUrl = await _imageHepler.saveImage(userRes.avatarImage, "avatars");
                        user.avatarUrl = imageUrl;
                    }
                    var updateResult = await _userManager.UpdateAsync(user);
                    if (!updateResult.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(newPassword))
                        {
                            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                            var res = await _userManager.ResetPasswordAsync(user, token, newPassword);
                            if (!res.Succeeded)
                            {
                                return new Api_response
                                {
                                    success = false,
                                    message = string.Join("; ", res.Errors.Select(e => e.Description))
                                };
                            }
                        }
                    }
                    return new Api_response
                    {
                        success = true,
                        message = "User updated successfully.",
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                        message = "User not found."
                    };
                }
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

        public async Task<Api_response> DeleteUserAsync_Admin(string id)
        {
            try
            {
                var userDelete = await _userManager.FindByIdAsync(id);
                if (userDelete != null)
                {
                    await _userManager.DeleteAsync(userDelete);
                    return new Api_response
                    {
                        success = true,
                        data = id
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                        message = "user does not exist"
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

        public async Task<Api_response> GetUserAsync_Client(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if(user != null)
                {
                    return new Api_response
                    {
                        success = true,
                        data = _mapper.Map<UserReq>(user)
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                        message = "user does not exist"
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
        public async Task<Api_response> UpdateUserAsync_Client(UserRes userRes, string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if(user != null)
                {
                    _mapper.Map(userRes, user);
                    if (userRes.avatarImage != null)
                    {
                        var imageUrl = await _imageHepler.saveImage(userRes.avatarImage, "avatars");
                        user.avatarUrl = imageUrl;
                    }
                    var updateResult = await _userManager.UpdateAsync(user);
                    if (!updateResult.Succeeded)
                    {
                        return new Api_response
                        {
                            success = false,
                            message = "update user fail"
                        };
                    }
                    return new Api_response
                    {
                        success = true,
                        data = _mapper.Map<UserReq>(user)
                    };

                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                        message = "user does not exist"
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

        public async Task<Api_response> ChangePassword_Client(string id, string oldPassword, string newPassword)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if(user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                    if (result.Succeeded)
                    {
                        return new Api_response
                        {
                            success = true,
                            message = "Change Password successful"
                        };
                    }
                    else
                    {
                        return new Api_response
                        {
                            success = false,
                            message = "Wrong current Password"
                        };
                    }
                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                        message = "user does not exist"
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
    }
}
