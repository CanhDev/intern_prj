using AutoMapper;
using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Helper.jwtSerivce;
using intern_prj.Repositories.interfaces;
using intern_prj.Services.interfaces;
using Microsoft.AspNetCore.Identity;

namespace intern_prj.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ImageHepler _imageHepler;
        private readonly ICartService _cartService;

        public UserService(IUserRepo userRepo, IMapper mapper, ImageHepler imageHepler, ICartService cartService,
            UserManager<ApplicationUser> userManager)
        {
            _userRepo = userRepo;
            _userManager = userManager;
            _mapper = mapper;
            _imageHepler = imageHepler;
            _cartService = cartService;
        }

        public async Task<Api_response> GetUsers_Admin()
        {
            try
            {
                var userEntityList = await _userRepo.GetUsers_Admin();
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<List<UserReq>>(userEntityList)
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
                var userEntity = await _userRepo.GetUser_Admin(id);
                if(userEntity == null)
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
                        data = _mapper.Map<UserReq>(userEntity)
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
                user.UserName = userRes.Email;
                if (userRes.AvatarImage != null)
                {
                    var imageUrl = await _imageHepler.saveImage(userRes.AvatarImage, "avatars");
                    user.avatarUrl = imageUrl;
                }
                user = await _userRepo.CreateUserAsync_Admin(user, userRes.Password ?? "1");
                if (user != null)
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
                        message = "Something went wrong when creating"
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

        public async Task<Api_response> DeleteUserAsync_Admin(string id)
        {
            try
            {
                var result = await _userRepo.DeleteUserAsync_Admin(id);
                if (result)
                {
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
                        message = "Something went wrong when delete"
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
        public async Task<Api_response> UpdateUserAsync_Admin(UserRes userRes, string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    _mapper.Map(userRes, user);
                    if (userRes.AvatarImage != null)
                    {
                        var imageUrl = await _imageHepler.saveImage(userRes.AvatarImage, "avatars");
                        user.avatarUrl = imageUrl;
                    }
                    user = await _userRepo.UpdateUserAsync_Admin(user);
                    if (user != null)
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
                            message = "Some thing went wrong when updating"
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
            catch (Exception ex)
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
                var userEntity = await _userRepo.GetUserAsync_Client(id);
                if(userEntity != null)
                {
                    return new Api_response
                    {
                        success = true,
                        data = _mapper.Map<UserReq>(userEntity)
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                        message = "User does not exist"
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
                var userEntity = await _userRepo.ChangePassword_Client(id, oldPassword, newPassword);
                if(userEntity != null)
                {
                    return new Api_response
                    {
                        success = true,
                        data = _mapper.Map<UserReq>(userEntity)
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                        message = "wrong old password"
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
                if (user != null)
                {
                    _mapper.Map(userRes, user);
                    if (userRes.AvatarImage != null)
                    {
                        var imageUrl = await _imageHepler.saveImage(userRes.AvatarImage, "avatars");
                        user.avatarUrl = imageUrl;
                    }
                    user = await _userRepo.UpdateUserAsync_Client(user);
                    if (user != null)
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
                            message = "Something went wrong when updating"
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
