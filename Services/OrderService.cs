using AutoMapper;
using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Repositories;
using intern_prj.Repositories.interfaces;
using intern_prj.Services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace intern_prj.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOderRepo _oderRepo;
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;

        public OrderService(IOderRepo oderRepo, IMapper mapper, IProductRepo productRepo)
        {
            _oderRepo = oderRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<Api_response> GetAllOrders(string? filterString, int pageNumber = 1, int pageSize = 12)
        {
            return await _oderRepo.GetAllOrders(filterString, pageNumber, pageSize);
        }

        public async Task<Api_response> GetOder(int orderId)
        {
            try
            {
                var orderEntity = await _oderRepo.GetOder(orderId);
                if(orderEntity == null)
                {
                    return new Api_response
                    {
                        success = false,
                        message = "Order does not exist"
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = true,
                        data = _mapper.Map<OrderReq>(orderEntity)
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

        public async Task<Api_response> GetOrdersByUser(string userId)
        {
            try
            {
                var ordersList = await _oderRepo.GetOrdersByUser(userId);
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<List<OrderReq>>(ordersList)
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

        public async Task<Api_response> GetOrdersDetail(int orderId)
        {
            try
            {
                var orderEntityList = await _oderRepo.GetOrdersDetail(orderId);
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<List<OrderDetailReq>>(orderEntityList)
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
                orderEntity =  await _oderRepo.CreateOrder(orderEntity);

                var listOderDetail = orderEntity.OrderDetails?.ToList() ?? new List<OrderDetail>();
                var productIds = listOderDetail.Select(od => od.ProductId).ToList();
                var products = await _productRepo.GetProductsbyIds(productIds);

                foreach (var orderDetail in listOderDetail)
                {
                    var productEntity = products.FirstOrDefault(p => p.Id == orderDetail.ProductId);
                    if (productEntity != null)
                    {
                        if (productEntity.SoldedCount == null)
                        {
                            productEntity.SoldedCount = 0;
                        }
                        productEntity.SoldedCount += orderDetail.UnitQuantity;
                        productEntity.Quantity -= orderDetail.UnitQuantity.Value;
                    }
                }

                await _productRepo.EditProductsAsync(products);
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<OrderReq>(orderEntity)
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

        public async Task<Api_response> DeleteOrder(int orderId)
        {
            try
            {
                var orderEntity = await _oderRepo.GetOder(orderId);
                if (orderEntity != null)
                {
                    await _oderRepo.DeleteOrder(orderEntity);
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
                        message = "Order does not exits"
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
        
        public async Task<Api_response> UpdateStatus(ChangeOrderStatusRes model)
        {
            try
            {
                var orderEntity = await _oderRepo.GetOder(model.orderId);
                if(orderEntity != null)
                {
                    orderEntity.StatusShipping = model.StatusShipping;
                    orderEntity.StatusPayment = model.StatusPayment;
                    orderEntity = await _oderRepo.UpdateOrder(orderEntity);
                    return new Api_response
                    {
                        success = true,
                        data = _mapper.Map<OrderReq>(orderEntity)
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
                    message = ex.Message,
                };
            }
        }
    }
}
