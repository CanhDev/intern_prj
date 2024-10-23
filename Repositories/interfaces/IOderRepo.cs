using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;

namespace intern_prj.Repositories.interfaces
{
    public interface IOderRepo
    {
        public Task<Api_response> GetAllOrders(string? filterString, int pageNumber = 1, int pageSize = 12);
        public Task<List<Order>> GetOrdersByUser(string userId);
        public Task<Order?> GetOder(int orderId);
        public Task<Order> CreateOrder(Order OrderEntity);
        public Task<Order> UpdateOrder(Order OrderEntity);
        public Task DeleteOrder(Order OrderEntity);
        public Task<Api_response> UpdateStatus(ChangeOrderStatusRes model);
        public Task<List<OrderDetail>> GetOrdersDetail(int orderId);
    }
}
