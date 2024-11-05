using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;

namespace intern_prj.Repositories.interfaces
{
    public interface IItemCartRepo
    {
        public Task<List<ItemCart>> GetItemCart_Cart(int cartId);

        public Task<ItemCart?> GetItemCart_productId_cartId(int? productId, int? cartId);

        public Task<ItemCart?> GetItemCart(int id);
        public Task<ItemCart?> UpdateItemCart(ItemCart item);
        public Task<ItemCart> AddItemCart(ItemCart itemCartEntity);
        public Task DeleteItemCart(ItemCart itemCartEntity);
        public Task DeleteAllItemCart(int cartId);
    }
}
