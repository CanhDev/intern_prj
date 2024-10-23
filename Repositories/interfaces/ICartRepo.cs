using intern_prj.Entities;
using intern_prj.Helper;

namespace intern_prj.Repositories.interfaces
{
    public interface ICartRepo
    {
        public Task<Cart?> GetCartAsync(string userId);
        public Task InitCart(Cart cartEntity);
    }
}
