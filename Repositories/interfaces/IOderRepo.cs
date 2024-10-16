using intern_prj.Data_response;
using intern_prj.Helper;

namespace intern_prj.Repositories.interfaces
{
    public interface IOderRepo
    {
        public Task<Api_response> GetOrdersByUser(string userId);
        public Task<Api_response> GetOder(int orderId);
        public Task<Api_response> CreateOrder(OrderRes orderRes);
        public Task<Api_response> DeleteOrder(int orderId);
        public Task<Api_response> UpdateStatus(int id, string StatusPayment, string StatusShipping);
        //
        public Task<Api_response> GetOrdersDetail(int orderId);
    }
}
