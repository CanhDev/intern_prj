using intern_prj.Helper;

namespace intern_prj.Services.interfaces
{
    public interface ICartService
    {
        public Task<Api_response> GetCartAsync(string userId);
        public Task<Api_response> InitCart(string userId);
    }
}
