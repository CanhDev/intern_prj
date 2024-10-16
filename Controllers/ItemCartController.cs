using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Repositories.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace intern_prj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemCartController : ControllerBase
    {
        private readonly IItemCartRepo _itemCartRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DecorContext _context;

        public ItemCartController(IItemCartRepo itemCartRepo, UserManager<ApplicationUser> userManager, DecorContext context)
        {
            _itemCartRepo = itemCartRepo;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet("GetByCart/{id}")]
        public async Task<IActionResult> GetItemsCartByCartId(int id)
        {
            try
            {
                    var res = await _itemCartRepo.GetItemCart_Cart(id);
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

        [HttpPost]
        public async Task<IActionResult> AddItem(ItemCartRes itemCartRes)
        {
            try
            {
                var res = await _itemCartRepo.AddItemCart(itemCartRes);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success= false,
                    message = ex.Message,
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var res = await _itemCartRepo.DeleteItemCart(id);
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
        [HttpPut("{id}")]
        public async Task<IActionResult> EditItem(ItemCartRes itemCartRes, int id)
        {
            try
            {
                var res = await _itemCartRepo.UpdateQuantity(itemCartRes, id);
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
