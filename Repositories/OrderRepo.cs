using AutoMapper;
using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

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
        public async Task<Api_response> GetOrdersByUser(string userId)
        {
            try
            {
                var orders = await _context.Orders.Where(o => o.UserId == userId).Include(o => o.OrderDetails).ToListAsync();
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<List<OrderReq>>(orders)
                };
            }
            catch(Exception ex)
            {
                return new Api_response
                {
                    success = false,
                    message = ex.Message,
                };
            }
        }
        public async Task<Api_response> CreateOrder(OrderRes orderRes)
        {
            try
            {
                var orderEntity = _mapper.Map<Order>(orderRes);
                _context.Orders.Add(orderEntity);
                await _context.SaveChangesAsync();
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<OrderReq>(orderEntity)
                };
            }
            catch (DbUpdateException dbEx) 
            {
                return new Api_response
                {
                    success = false,
                    message = dbEx.InnerException?.Message ?? dbEx.Message, 
                };
            }
            catch (Exception ex) 
            {
                return new Api_response
                {
                    success = false,
                    message = ex.InnerException?.Message ?? ex.Message, 
                };
            }
        }


        public async Task<Api_response> DeleteOrder(int orderId)
        {
            try
            {
                var orderDelete = await _context.Orders.FindAsync(orderId);
                if (orderDelete != null)
                {
                    _context.Orders.Remove(orderDelete);
                    await _context.SaveChangesAsync();
                    return new Api_response
                    {
                        success = true,
                        data = orderId
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                        message = "order does not exist"
                    };
                }
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

        public async Task<Api_response> UpdateStatus(int id, string status)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);
                if (order != null)
                {
                    order.Status = status;
                    await _context.SaveChangesAsync();
                    return new Api_response
                    {
                        success = true,
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                    };
                }
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
    }
}
