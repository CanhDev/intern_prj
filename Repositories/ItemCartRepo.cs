using AutoMapper;
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

        public async Task<List<ItemCart>> GetItemCart_Cart(int cartId)
        {
                var items = await _context.ItemCarts
                    .Where(i => i.CartId == cartId)
                    .Include(i => i.Product)               
                    .ThenInclude(p => p.Images)            
                    .ToListAsync();
                foreach(var item in items)
                {
                    if(item?.Product?.Quantity <= 0)
                    {
                        _context.ItemCarts.Remove(item);
                    }
                }
                await _context.SaveChangesAsync();
                return items;
        }

        public Task<ItemCart?> GetItemCart_productId_cartId(int? productId, int? cartId)
        {
                var itemCartEntity = _context.ItemCarts.FirstOrDefaultAsync(i => i.CartId == cartId
                                                                        && i.ProductId == productId);
                return itemCartEntity;
        }

        public async Task<ItemCart?> GetItemCart(int id)
        {
                var itemCartEntity =  await _context.ItemCarts
                    .Include(i => i.Product)
                    .ThenInclude(i => i.Images)
                    .FirstOrDefaultAsync(i => i.Id == id);
                return itemCartEntity;
        }

        public async Task<ItemCart?> UpdateItemCart(ItemCart item)
        {
            _context.ItemCarts.Attach(item);
            _context.ItemCarts.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<ItemCart> AddItemCart(ItemCart itemCartEntity)
        {
                _context.ItemCarts.Add(itemCartEntity);
                await _context.SaveChangesAsync();
                return itemCartEntity;
        }


        public async Task DeleteItemCart(ItemCart itemCartEntity)
        {
                _context.ItemCarts.Remove(itemCartEntity);
                await _context.SaveChangesAsync();
        }
        public async Task DeleteAllItemCart(int cartId)
        {
            var items = await _context.ItemCarts.Where(i => i.CartId == cartId).AsNoTracking().ToListAsync();
            if (items.Any())
            {
                _context.ItemCarts.RemoveRange(items);
                await _context.SaveChangesAsync();
            }
        }
    }
}
