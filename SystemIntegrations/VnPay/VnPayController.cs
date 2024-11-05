using Azure.Core;
using intern_prj.Data_response;
using intern_prj.Helper;
using intern_prj.SystemIntegrations.VnPay.Services;
using intern_prj.SystemIntegrations.VnPay.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace intern_prj.SystemIntegrations.VnPay
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnPayController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IConfiguration _configuration;

        public VnPayController(IVnPayService vnPayService, IConfiguration configuration)
        {
            _vnPayService = vnPayService;
            _configuration = configuration;
        }
        [Authorize]
        [HttpGet("VnPay_checkout")]
        public IActionResult createUrl(double amount, string orderCode)
        {
            try
            {
                var urlString = _vnPayService.CreatePaymentUrl(HttpContext, amount, orderCode);
                return Ok(new Api_response
                {
                    success = true,
                    data = urlString
                });
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
        [HttpGet("vnpay_return")]
        public IActionResult PaymentExecute()
        {
            try
            {
                bool result = _vnPayService.PaymentExecute(Request.Query);
                var resultUrl = $"{_configuration["VnPay:NotificationUrl"]}/?status={result}";
                return Redirect(resultUrl);
            }
            catch
            {
                var errorUrl = $"{_configuration["VnPay:NotificationUrl"]}/?status={false}";
                return Redirect(errorUrl);
            }
        }
    }
}
