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
    [Authorize(Roles = AppRole.Admin)]
    public class DashboardController : ControllerBase
    {
        private readonly IDashBoardService _dashBoardService;

        public DashboardController(IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }

        [HttpGet("Statistic")]
        public async Task<IActionResult> GetRevenueStatistics(string? startDate, string? endDate)
        {
            try
            {
                var res = await _dashBoardService.GetRevenueStatistics(startDate, endDate);
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
        [HttpGet("TopSelling")]
        public async Task<IActionResult> GetTopSelling(int count = 5)
        {
            try
            {
                var res = await _dashBoardService.GetTopSelling(count);
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
