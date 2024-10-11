using intern_prj.Data_response;
using intern_prj.Helper;
using intern_prj.Repositories;
using intern_prj.Repositories.interfaces;
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
        [HttpGet("{userId}")]
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            try
            {
                var res = await _oderRepo.UpdateStatus(id, status);
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
