using intern_prj.Data_response;
using intern_prj.Helper;
using intern_prj.Helper.jwtSerivce;
using intern_prj.Repositories.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace intern_prj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo) {
            _userRepo = userRepo;
        }
        [Authorize(Roles = AppRole.Admin)]
        [HttpGet("Admin/User")]
        public async Task<IActionResult> GetUsers_Admin()
        {
            try
            {
                var res = await _userRepo.GetUsers_Admin();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success = false,
                    message = ex.Message,
                });
            }
        }
        [Authorize(Roles = AppRole.Admin)]
        [HttpGet("Admin/User/{id}")]
        public async Task<IActionResult> GetUser_Admin(string id)
        {
            try
            {
                var res = await _userRepo.GetUser_Admin(id);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success = false,
                    message = ex.Message,
                });
            }
        }
        [Authorize(Roles = AppRole.Admin)]
        [HttpPost("Admin/User")]
        public async Task<IActionResult> CreateUserAsync_Admin([FromForm] UserRes userRes)
        {
            try
            {
                var res = await _userRepo.CreateUserAsync_Admin(userRes);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success = false,
                    message = ex.Message,
                });
            }
        }
        [Authorize(Roles = AppRole.Admin)]
        [HttpPut("Admin/User/{id}")]
        public async Task<IActionResult> UpdateUserAsync_Admin(UserRes userRes, string id, string newPassword)
        {
            try
            {
                var res = await _userRepo.UpdateUserAsync_Admin(userRes, id, newPassword);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success = false,
                    message = ex.Message,
                });
            }
        }
        [Authorize(Roles = AppRole.Admin)]
        [HttpDelete("Admin/User/{id}")]
        public async Task<IActionResult> DeleteUserAsync_Admin(string id)
        {
            try
            {
                var res = await _userRepo.DeleteUserAsync_Admin(id);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success = false,
                    message = ex.Message,
                });
            }
        }

        [Authorize]
        [HttpGet("Client/User")]
        public async Task<IActionResult> GetUserAsync_Client()
        {
            try
            {
                string? id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if(id == null)
                {
                    return Unauthorized(new Api_response
                    {
                        success = false,
                        message = "Unauthorized"
                    });
                }
                var res = await _userRepo.GetUserAsync_Client(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success = false,
                    message = ex.Message,
                });
            }
        }
        [Authorize]
        [HttpPut("Client/User")]
        public async Task<IActionResult> UpdateUserAsync_Client([FromForm] UserRes userRes)
        {
            try
            {
                string? id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if(id == null)
                {
                    return Unauthorized(new Api_response
                    {
                        success = false,
                        message = "Unauthorized"
                    });
                }
                var res = await _userRepo.UpdateUserAsync_Client(userRes, id);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success = false,
                    message = ex.Message,
                });
            }
        }
        [Authorize]
        [HttpPut("Client/User/ChangePassword")]
        public async Task<IActionResult> ChangePassword_Client(string oldPassword, string newPassword)
        {
            try
            {
                string? id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if(id == null)
                {
                    return Unauthorized(new Api_response
                    {
                        success = false,
                        message = "Unauthorized"
                    });
                }
                var res = await _userRepo.ChangePassword_Client(id, oldPassword, newPassword);   
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success = false,
                    message = ex.Message,
                });
            }
        }
    }
}
