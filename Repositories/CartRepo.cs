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
        public async Task<Cart?> GetCartAsync(string userid)
        {
            try
            {
                var cart = await _context.Carts
                    .Include(c => c.ItemCarts)
                    .ThenInclude(ic => ic.Product)
                    .ThenInclude(p => p.Images)
                    .FirstOrDefaultAsync(c => c.UserId == userid);
                return cart;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InitCart(Cart cartEntity)
        {
            try
            {
                 _context.Carts.Add(cartEntity);
                 await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
