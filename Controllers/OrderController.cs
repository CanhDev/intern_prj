using intern_prj.Data_response;
using intern_prj.Helper;
using intern_prj.Helper.jwtSerivce;
using intern_prj.Repositories;
using intern_prj.Repositories.interfaces;
using intern_prj.Services;
using intern_prj.Services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace intern_prj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAllOrder")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> getOrderList(string? filterString, int pageNumber = 1, int pageSize = 12)
        {
            try
            {
                var res = await _orderService.GetAllOrders(filterString, pageNumber, pageSize);
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
                var res = await _orderService.GetOrdersByUser(userId);
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
                var res = await _orderService.GetOder(orderId);
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
                var res= await _orderService.CreateOrder(orderRes);
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
                var res = await _orderService.DeleteOrder(id);
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
        public async Task<IActionResult> UpdateStatus(ChangeOrderStatusRes model)
        {
            try
            {
                var res = await _orderService.UpdateStatus(model);
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
                var res = await _orderService.GetOrdersDetail(orderId);
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
