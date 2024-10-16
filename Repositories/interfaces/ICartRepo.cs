using intern_prj.Helper;

namespace intern_prj.Repositories.interfaces
{
    public interface ICartRepo
    {
        public Task<Api_response> GetCartAsync(string userId);
        public Task<Api_response> InitCart(string userId);
        public Task<Api_response> DeleteCart(int idCart);
    }
}
