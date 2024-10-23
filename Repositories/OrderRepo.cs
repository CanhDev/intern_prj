using AutoMapper;
using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;
using X.PagedList.EF;

namespace intern_prj.Repositories
{
    public class OrderRepo : IOderRepo
    {
        private readonly DecorContext _context;
        private readonly IMapper _mapper;

        public OrderRepo(DecorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Api_response> GetAllOrders(string? filterString, int pageNumber = 1, int pageSize = 12)
        {
            try
            {
                IQueryable<Order> orders =  _context.Orders.Include(o => o.User);
                orders = orders.OrderBy(o => o.OrderDate);
                if(!string.IsNullOrEmpty(filterString))
                {
                    orders = orders.Where(o => o.OrderCode.Contains(filterString));
                }
                //paging
                var ordersPaging = await orders.ToPagedListAsync(pageNumber, pageSize);
                    return new Api_response
                    {
                        success = true,
                        data = new
                        {
                            ordersList = _mapper.Map<List<OrderReq>>(ordersPaging),
                            ordersLength = orders.Count()
                        }
                    };
            }
            catch (Exception ex)
            {
                return new Api_response
                {
                    success = false,
                    message = ex.Message,
                };
            }
        }

        public async Task<List<Order>> GetOrdersByUser(string userId)
        {
            try
            {
                var orders = await _context.Orders.Where(o => o.UserId == userId).Include(o => o.OrderDetails).ToListAsync();
                return orders;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Order?> GetOder(int orderId)
        {
            try
            {
                var order = await _context.Orders
                                .Include(o => o.OrderDetails)
                                .Include(o => o.FeedBacks)
                                .FirstOrDefaultAsync(o => o.Id == orderId);
                return order;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Order> CreateOrder(Order OrderEntity)
        {
            try
            {
                _context.Orders.Add(OrderEntity);
                await _context.SaveChangesAsync();
                return OrderEntity;
            }
            
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteOrder(Order OrderEntity)
        {
            try
            {
                _context.Orders .Remove(OrderEntity);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Api_response> UpdateStatus(ChangeOrderStatusRes model)
        {
            try
            {
                var order = await _context.Orders.FindAsync(model.orderId);
                    order.StatusShipping = model.StatusShipping;
                    order.StatusPayment = model.StatusPayment;
                    await _context.SaveChangesAsync();
                    return new Api_response
                    {
                        success = true,
                        data = _mapper.Map<OrderReq>(order)
                    };
            }
            catch(Exception ex)
            {
                return new Api_response
                {
                    success = false,
                    message = ex.Message
                };
            }
        }
        public async Task<Order> UpdateOrder(Order OrderEntity)
        {
            try
            {
                _context.Orders.Attach(OrderEntity);
                _context.Orders.Entry(OrderEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return OrderEntity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<OrderDetail>> GetOrdersDetail(int orderId)
        {
            try
            {
                var ordersDetail = await _context.OrderDetails.Include(od => od.Product.Images)
                    .Where(od => od.OrderId == orderId).ToListAsync();
                return ordersDetail;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
