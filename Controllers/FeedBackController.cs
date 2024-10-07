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
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackRepo _feedBackRepo;

        public FeedBackController(IFeedBackRepo feedBackRepo)
        {
            _feedBackRepo = feedBackRepo;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetFeedBacks(int orderId)
        {
            try
            {
                var res = await _feedBackRepo.GetFeedBacks(orderId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success = true,
                    message = ex.Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> SendFeedBack(FeedBackRes feedBackRes)
        {
            try
            {
                var res = await _feedBackRepo.SendFeedBack(feedBackRes);
                return Ok(res);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedBack(int id)
        {
            try
            {
                var res = await _feedBackRepo.DeleteFeedBack(id);
                return Ok(res);
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
