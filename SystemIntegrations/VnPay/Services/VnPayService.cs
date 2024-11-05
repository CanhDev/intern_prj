using intern_prj.Data_response;
using intern_prj.SystemIntegrations.VnPay.Services.Interfaces;
using System.Security;

namespace intern_prj.SystemIntegrations.VnPay.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;

        public VnPayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreatePaymentUrl(HttpContext context, double amount, string orderCode)
        {
            try
            {
                if (amount <= 0)
                    throw new ArgumentException("Amount must be greater than 0");

                if (string.IsNullOrEmpty(orderCode))
                    throw new ArgumentException("OrderCode cannot be null or empty");

                var baseUrl = _configuration["VnPay:BaseUrl"];
                if (string.IsNullOrEmpty(baseUrl))
                    throw new ArgumentException("VnPay:BaseUrl configuration is missing");

                var tick = DateTime.Now.Ticks.ToString();
                string createDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                var vnpay = new VnPayLibrary();

                vnpay.AddRequestData("vnp_Version", _configuration["VnPay:Version"] ?? "2.1.0");
                vnpay.AddRequestData("vnp_Command", _configuration["VnPay:Command"] ?? "pay");
                vnpay.AddRequestData("vnp_TmnCode", _configuration["VnPay:TmnCode"]);
                vnpay.AddRequestData("vnp_Amount", ((long)(amount * 100)).ToString());
                vnpay.AddRequestData("vnp_CreateDate", createDate);
                vnpay.AddRequestData("vnp_CurrCode", _configuration["VnPay:CurrCode"] ?? "VND");
                vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
                vnpay.AddRequestData("vnp_Locale", _configuration["VnPay:Locale"] ?? "vn");
                vnpay.AddRequestData("vnp_OrderInfo", orderCode);
                vnpay.AddRequestData("vnp_OrderType", "other");
                vnpay.AddRequestData("vnp_ReturnUrl", _configuration["VnPay:PaymentBackReturnUrl"]);
                vnpay.AddRequestData("vnp_TxnRef", tick);

                var hashSecret = _configuration["VnPay:HashSecret"];
                if (string.IsNullOrEmpty(hashSecret))
                    throw new ArgumentException("VnPay:HashSecret configuration is missing");

                var paymentUrl = vnpay.CreateRequestUrl(baseUrl, hashSecret);
                return paymentUrl;
            }
            catch (Exception ex)
            {
                // Log the exception details here
                throw new Exception($"Error creating VnPay payment URL: {ex.Message}", ex);
            }
        }

        public bool PaymentExecute(IQueryCollection VnpayData)
        {
            try
            {
                var vnpay = new VnPayLibrary();
                foreach (var (key, value) in VnpayData)
                {
                    if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(key, value.ToString());
                    }
                }

                // Kiểm tra mã phản hồi
                var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                if (string.IsNullOrEmpty(vnp_ResponseCode))
                {
                    throw new ArgumentException("Response code is missing");
                }

                // Kiểm tra mã giao dịch
                var vnp_TransactionId = vnpay.GetResponseData("vnp_TransactionNo");
                if (string.IsNullOrEmpty(vnp_TransactionId))
                {
                    throw new ArgumentException("Transaction ID is missing");
                }

                var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
                if (string.IsNullOrEmpty(vnp_OrderInfo))
                {
                    throw new ArgumentException("Order info is missing");
                }

                var vnp_SecureHash = VnpayData.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
                if (string.IsNullOrEmpty(vnp_SecureHash))
                {
                    throw new ArgumentException("Secure hash is missing");
                }

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _configuration["VnPay:HashSecret"]);
                if (!checkSignature)
                {
                    throw new SecurityException("Invalid signature");
                }

                if (vnp_ResponseCode == "00") // Mã thành công
                {
                    return true; 
                }
                else
                {
                    throw new Exception($"Payment failed with response code: {vnp_ResponseCode}");
                }
            }
            catch 
            {
                return false;
            }
        }

    }
}
