using intern_prj.Data_response;

namespace intern_prj.SystemIntegrations.VnPay.Services.Interfaces
{
    public interface IVnPayService
    {
        public string CreatePaymentUrl(HttpContext context
            , double amount, string orderCode);
        public bool PaymentExecute(IQueryCollection VnpayData);
    }
}
