using intern_prj.Data_response;
using intern_prj.Helper;
using intern_prj.Helper.jwtSerivce;
using intern_prj.Repositories.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace intern_prj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo _accountRepo;

        public AccountController(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpRes model)
        {
            try
            {
                return Ok(await _accountRepo.SignUpAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success = false,
                    message = ex.Message    
                });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRes model)
        {
            try
            {
                return Ok(await _accountRepo.LoginAsync(model));
            }
            catch(Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success=false,
                    message = ex.Message
                });
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenModel OldToken)
        {
            try
            {
                return Ok(await _accountRepo.RenewTokenAsync(OldToken));
            }
            catch(Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
        [HttpPost("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var Email = User.FindFirst(ClaimTypes.Email)?.Value;
                return Email != null ? Ok(await _accountRepo.GetUserAsync(Email)) : Unauthorized(new Api_response
                {
                    success = false,
                    message = "Unauthorized"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
