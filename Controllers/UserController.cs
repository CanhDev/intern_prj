using intern_prj.Data_response;
using intern_prj.Helper;
using intern_prj.Helper.jwtSerivce;
using intern_prj.Repositories.interfaces;
using intern_prj.Services;
using intern_prj.Services.interfaces;
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
        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }
        [Authorize(Roles = AppRole.Admin)]
        [HttpGet("Admin/User")]
        public async Task<IActionResult> GetUsers_Admin()
        {
            try
            {
                var res = await _userService.GetUsers_Admin();
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
                var res = await _userService.GetUser_Admin(id);
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
                var res = await _userService.CreateUserAsync_Admin(userRes);
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
        public async Task<IActionResult> UpdateUserAsync_Admin([FromForm] UserRes userRes, string id)
        {
            try
            {
                var res = await _userService.UpdateUserAsync_Admin(userRes, id);
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
                var res = await _userService.DeleteUserAsync_Admin(id);
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

        
        [HttpGet("Client/User")]
        [Authorize]
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
                var res = await _userService.GetUserAsync_Client(id);
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
                var res = await _userService.UpdateUserAsync_Client(userRes, id);
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
        public async Task<IActionResult> ChangePassword_Client([FromBody] ChangePasswordRes model)
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
                var res = await _userService.ChangePassword_Client(id, model.OldPassword, model.NewPassword);
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
