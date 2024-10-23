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
    public class ItemCartService : IItemCartService
    {
        private readonly IItemCartRepo _itemCartRepo;
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;


        

        public ItemCartService(IItemCartRepo itemCartRepo, IMapper mapper, IProductRepo productRepo)
        {
            _itemCartRepo = itemCartRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<Api_response> GetItemCart_Cart(int cartId)
        {
            try
            {
                var ItemCarts = await _itemCartRepo.GetItemCart_Cart(cartId);
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<List<ItemCartReq>>(ItemCarts)
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
        public async Task<Api_response> AddItemCart(ItemCartRes itemCartRes)
        {
            try
            {
                var itemCartEntity = await _itemCartRepo.GetItemCart_productId_cartId(itemCartRes.ProductId, itemCartRes.CartId);
                var productEntity = await _productRepo.GetProductAsync(itemCartRes.ProductId);
                //
                if (productEntity != null && productEntity.OutOfStockstatus == false)
                {
                    if (itemCartEntity == null)
                    {
                        itemCartEntity = new ItemCart();
                        _mapper.Map(itemCartRes, itemCartEntity);
                        itemCartEntity = await _itemCartRepo.AddItemCart(itemCartEntity);
                        return new Api_response
                        {
                            success = true,
                            data = _mapper.Map<ItemCartReq>(itemCartEntity)
                        };
                    }
                    else
                    {
                        itemCartEntity.Quantity += itemCartRes.Quantity;
                        itemCartEntity.Price += itemCartRes.Price;
                    }
                    productEntity.Quantity -= itemCartRes.Quantity.Value;

                    await _itemCartRepo.UpdateItemCart(itemCartEntity);
                    await _productRepo.EditProductAsync(productEntity);

                    return new Api_response
                    {
                        success = true,
                        data = _mapper.Map<ItemCartReq>(itemCartEntity)
                    };
                }
                else if (productEntity?.OutOfStockstatus == true)
                {
                    return new Api_response
                    {
                        success = false,
                        message = "product is out of stock"
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                        message = "Product does not exist",
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

        public async Task<Api_response> DeleteItemCart(int itemCartId)
        {
            try
            {
                var itemCartEntity = await _itemCartRepo.GetItemCart(itemCartId);
                var productEntity = await _productRepo.GetProductAsync(itemCartEntity?.ProductId);
                //
                if(productEntity != null && itemCartEntity != null)
                {
                    if (itemCartEntity.Quantity.HasValue)
                    {
                        productEntity.Quantity += itemCartEntity.Quantity.Value;
                    }
                    await _itemCartRepo.DeleteItemCart(itemCartEntity);
                    await _productRepo.EditProductAsync(productEntity);
                    return new Api_response
                    {
                        success = true,
                        data = itemCartId
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                        message = "product or itemcart does not exist"
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

        
        public async Task<Api_response> UpdateQuantity(ItemCartRes itemCartRes, int id)
        {
            try
            {
                var itemCartEntity = await _itemCartRepo.GetItemCart(id);
                if(itemCartEntity == null)
                {
                    return new Api_response
                    {
                        success = false,
                        message = "itemcart does not exist"
                    };
                }
                else
                {
                    itemCartEntity.Quantity = itemCartRes.Quantity;
                    itemCartEntity.Price = itemCartRes.Price;
                    itemCartEntity = await _itemCartRepo.UpdateItemCart(itemCartEntity);
                    return new Api_response
                    {
                        success = true,
                        data = _mapper.Map<ItemCartReq>(itemCartEntity)
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
