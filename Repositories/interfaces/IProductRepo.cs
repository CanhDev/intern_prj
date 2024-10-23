using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using System.Runtime.CompilerServices;

namespace intern_prj.Repositories.interfaces
{
    public interface IProductRepo
    {
        //interact with database
        public Task<Api_response> GetProductsAsync
            (int? typeId, string? sortString, string? filterString, int pageNumber = 1, int pageSize = 3, string? role = "");
        public Task<List<Product>> GetProductsbyIds(List<int> ids);
        public Task<Product?> GetProductAsync(int? id);
        public Task<productReq?> AddProductAsync(Product productEntity);
        public Task<productReq?> EditProductAsync(Product productEntity);
        public Task EditProductsAsync(List<Product> productEntityList);
        public Task<bool> DeleteProductAsync(Product productEntity);


        // sort & filter
        public IQueryable<Product> handleSort(IQueryable<Product> productReqs, string? sortString);
        public IQueryable<Product> handleFilter(IQueryable<Product> productReqs, string? filterString);
    }
}
