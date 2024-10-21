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

        public async Task<Api_response> GetAllOrders()
        {
            try
            {
                var orders = await _context.Orders.ToListAsync();
                    return new Api_response
                    {
                        success = true,
                        data = _mapper.Map<List<OrderReq>>(orders)
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
        public async Task<Api_response> GetOder(int orderId)
        {
            try
            {
                var order = await _context.Orders
                                .Include(o => o.OrderDetails)
                                .Include(o => o.FeedBacks)
                                .FirstOrDefaultAsync(o => o.Id == orderId);
                if (order == null)
                {
                    return new Api_response
                    {
                        success = false,
                        message = "order does not exist"
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = true,
                        data = _mapper.Map<OrderReq>(order)
                    };
                }
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

                var listOderDetail = orderEntity.OrderDetails?.ToList() ?? new List<OrderDetail>();
                var productIds = listOderDetail.Select(od => od.ProductId).ToList();
                var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

                foreach (var orderDetail in listOderDetail)
                {
                    var productEntity = products.FirstOrDefault(p => p.Id == orderDetail.ProductId);
                    if (productEntity != null)
                    {
                        if(productEntity.SoldedCount == null)
                        {
                            productEntity.SoldedCount = 0;
                        }
                        productEntity.SoldedCount += orderDetail.UnitQuantity;
                        productEntity.Quantity -= orderDetail.UnitQuantity.Value;
                    }
                }

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

        public async Task<Api_response> UpdateStatus(ChangeOrderStatusRes model)
        {
            try
            {
                var order = await _context.Orders.FindAsync(model.orderId);
                if (order != null)
                {
                    order.StatusShipping = model.StatusShipping;
                    order.StatusPayment = model.StatusShipping;
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

        public async Task<Api_response> GetOrdersDetail(int orderId)
        {
            try
            {
                var ordersDetail = await _context.OrderDetails.Include(od => od.Product.Images)
                    .Where(od => od.OrderId == orderId).ToListAsync();
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<List<OrderDetailReq>>(ordersDetail)
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
    }
}
