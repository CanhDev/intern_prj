﻿using AutoMapper;
using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace intern_prj.Repositories
{
    public class ItemCartRepo : IItemCartRepo
    {
        private readonly DecorContext _context;
        private readonly IMapper _mapper;

        public ItemCartRepo(DecorContext context ,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Api_response> GetItemCart_Cart(int cartId)
        {
            try
            {
                var items = await _context.ItemCarts.Where(i => i.CartId == cartId).ToListAsync();
                return new Api_response
                {
                    success = true,
                    data = items
                };
            }
            catch(Exception ex){
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
                var item = await _context.ItemCarts.FirstOrDefaultAsync(i => i.CartId == itemCartRes.CartId
                                                                        && i.ProductId == itemCartRes.ProductId);
                var product = await _context.Products.FindAsync(itemCartRes.ProductId);
                if(product != null)
                {
                    if (item == null)
                    {
                        item = new ItemCart();
                        _mapper.Map(itemCartRes, item);
                        _context.ItemCarts.Add(item);
                    }
                    else
                    {
                        item.Quantity += itemCartRes.Quantity;
                    }
                    product.Quantity -= itemCartRes.Quantity.Value;
                    await _context.SaveChangesAsync();
                    return new Api_response
                    {
                        success = true,
                        data = _mapper.Map<ItemCartReq>(item)
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                        message = "product does not exist",
                    };
                }
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

        public async Task<Api_response> DeleteItemCart(int itemCartId)
        {
            try
            {
                var item = await _context.ItemCarts.FindAsync(itemCartId);
                var product = await _context.Products.FindAsync(item.ProductId);
                //
                if(product != null || item.Quantity.HasValue)
                {
                    product.Quantity += item.Quantity.Value;
                }
                _context.ItemCarts.Remove(item);
                await _context.SaveChangesAsync();
                return new Api_response
                {
                    success = true,
                    data = itemCartId
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

        public async Task<Api_response> UpdateQuantity(ItemCartRes itemCartRes, int id)
        {
            try
            {
                var item = await _context.ItemCarts.FindAsync(id);
                if(item != null)
                {
                    item.Quantity += itemCartRes.Quantity;
                    await _context.SaveChangesAsync();
                    return new Api_response
                    {
                        success = true,
                        data = _mapper.Map<ItemCartReq>(item)
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                        message = "ItemCart does not exist"
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
