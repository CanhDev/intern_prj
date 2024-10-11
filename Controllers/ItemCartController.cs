using intern_prj.Data_response;
using intern_prj.Helper;
using intern_prj.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace intern_prj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCartController : ControllerBase
    {
        private readonly IItemCartRepo _itemCartRepo;

        public ItemCartController(IItemCartRepo itemCartRepo)
        {
            _itemCartRepo = itemCartRepo;
        }

        [HttpGet("GetByCart/{cartId}")]
        public async Task<IActionResult> GetItemsCartByCartId(int cartId)
        {
            try
            {
                var res = await _itemCartRepo.GetItemCart_Cart(cartId);
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
        public async Task<IActionResult> DeleteItem(int itemCartId)
        {
            try
            {
                var res = await _itemCartRepo.DeleteItemCart(itemCartId);
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
