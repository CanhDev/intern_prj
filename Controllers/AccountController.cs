using intern_prj.Data_response;
using intern_prj.Helper;
using intern_prj.Helper.jwtSerivce;
using intern_prj.Repositories.interfaces;
using intern_prj.Services;
using intern_prj.Services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace intern_prj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpRes model)
        {
            try
            {
                return Ok(await _accountService.SignUpAsync(model));
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
                return Ok(await _accountService.LoginAsync(model));
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
                return Ok(await _accountService.RenewTokenAsync(OldToken));
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
