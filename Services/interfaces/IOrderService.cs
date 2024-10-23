using intern_prj.Data_response;
using intern_prj.Helper;

namespace intern_prj.Services.interfaces
{
    public interface IOrderService
    {
        public Task<Api_response> GetAllOrders(string? filterString, int pageNumber = 1, int pageSize = 12);
        public Task<Api_response> GetOrdersByUser(string userId);
        public Task<Api_response> GetOder(int orderId);
        public Task<Api_response> CreateOrder(OrderRes orderRes);
        public Task<Api_response> DeleteOrder(int orderId);
        public Task<Api_response> UpdateStatus(ChangeOrderStatusRes model);
        public Task<Api_response> GetOrdersDetail(int orderId);
    }
}
