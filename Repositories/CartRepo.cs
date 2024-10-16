using AutoMapper;
using intern_prj.Data_request;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Repositories.interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace intern_prj.Repositories
{
    public class CartRepo : ICartRepo
    {
        private readonly DecorContext _context;
        private readonly IMapper _mapper;

        public CartRepo(DecorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Api_response> GetCartAsync(string userid)
        {
            try
            {
                var cart = await _context.Carts
                    .Include(c => c.ItemCarts)
                    .ThenInclude(ic => ic.Product)
                    .ThenInclude(p => p.Images)
                    .FirstOrDefaultAsync(c => c.UserId == userid);
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<CartReq>(cart)
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
        public async Task<Api_response> InitCart(string userId)
        {
            try
            {
                var cartInit = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
                if (cartInit != null)
                {
                    return new Api_response
                    {
                        success = false,
                        message = "Cart already exists"
                    };
                }
                else
                {
                    cartInit = new Cart
                    {
                        UserId = userId,
                        CreateDate = DateTime.UtcNow
                    };
                    _context.Carts.Add(cartInit);
                    await _context.SaveChangesAsync();
                    return new Api_response
                    {
                        success = true,
                        message = "Init Cart successful"
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
        public async Task<Api_response> DeleteCart(int idCart)
        {
            try
            {
                var cartDelete = await _context.Carts.FindAsync(idCart);
                if (cartDelete != null)
                {
                     _context.Carts.Remove(cartDelete);
                    await _context.SaveChangesAsync();
                    return new Api_response
                    {
                        success = true,
                        message = "Delete cart successful"
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                        message = "Cart does not exist"
                    };
                }
            }
            catch(Exception ex)
            {
                return new Api_response
                {
                    success = false,
                    message = ex?.Message,
                };
            }
        }
    }
}
