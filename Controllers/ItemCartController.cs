using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Repositories.interfaces;
using intern_prj.Services;
using intern_prj.Services.interfaces;
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
        private readonly IItemCartService _itemCartService;

        public ItemCartController(IItemCartService itemCartService)
        {
            _itemCartService = itemCartService;
        }

        [HttpGet("GetByCart/{id}")]
        public async Task<IActionResult> GetItemsCartByCartId(int id)
        {
            try
            {
                    var res = await _itemCartService.GetItemCart_Cart(id);
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
                var res = await _itemCartService.AddItemCart(itemCartRes);
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
                var res = await _itemCartService.DeleteItemCart(id);
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
        [HttpDelete("DeleteAll/{cartId}")]
        public async Task<IActionResult> DeleteAll(int cartId)
        {
            try
            {
                var res = await _itemCartService.DeleteAllItemCart(cartId);
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
        [HttpPut("{id}")]
        public async Task<IActionResult> EditItem(ItemCartRes itemCartRes, int id)
        {
            try
            {
                var res = await _itemCartService.UpdateQuantity(itemCartRes, id);
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
