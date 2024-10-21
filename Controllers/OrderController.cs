using intern_prj.Data_response;
using intern_prj.Helper;
using intern_prj.Helper.jwtSerivce;
using intern_prj.Repositories;
using intern_prj.Repositories.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace intern_prj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOderRepo _oderRepo;

        public OrderController(IOderRepo oderRepo)
        {
            _oderRepo = oderRepo;
        }

        [HttpGet("GetAllOrder")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> getOrderList()
        {
            try
            {
                var res = await _oderRepo.GetAllOrders();
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
        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetOrdersByUser(string userId)
        {
            try
            {
                var res = await _oderRepo.GetOrdersByUser(userId);
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

        [HttpGet("GetSingleOrder/{orderId}")]
        [Authorize]
        public async Task<IActionResult> GetOder(int orderId)
        {
            try
            {
                var res = await _oderRepo.GetOder(orderId);
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
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderRes orderRes)
        {
            try
            {
                var res= await _oderRepo.CreateOrder(orderRes);
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
        [HttpDelete("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var res = await _oderRepo.DeleteOrder(id);
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

        [HttpPut("ChangeOrderStatus")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> UpdateStatus([FromBody] ChangeOrderStatusRes model)
        {
            try
            {
                var res = await _oderRepo.UpdateStatus(model);
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
        //
        [HttpGet("GetOrderDetail/{orderId}")]
        [Authorize]
        public async Task<IActionResult> GetOrdersDetail(int orderId)
        {
            try
            {
                var res = await _oderRepo.GetOrdersDetail(orderId);
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
