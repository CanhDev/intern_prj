using intern_prj.Helper;
using intern_prj.Repositories;
using intern_prj.Repositories.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace intern_prj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartRepo _cartRepo;

        public CartController(ICartRepo cartRepo) {
            _cartRepo = cartRepo;
        }

        [HttpGet]
        public async Task<IActionResult> getCart()
        {
            try
            {
                string? id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var res = await _cartRepo.GetCartAsync(id);
                return Ok(res);
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
    }
}
