using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;

namespace intern_prj.Services.interfaces
{
    public interface IProductService
    {
        public Task<Api_response> GetProductsAsync
            (int? typeId, string? sortString, string? filterString, int pageNumber = 1, int pageSize = 3, string? role = "");
        public Task<Api_response> GetProductAsync(int id);
        public Task<Api_response> AddProductAsync(productRes productRes);
        public Task<Api_response> EditProductAsync(productRes productRes, int id);
        public Task<Api_response> DeleteProductAsync(int id);
    }
}
