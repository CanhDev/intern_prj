using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;

namespace intern_prj.Repositories.interfaces
{
    public interface ICategoryRepo
    {
        public Task<List<Category>> getCategories();
        public Task<Category?> getCategory(int id);
        public Task<CategoryReq?> addCategory(Category categoryEntity);
        public Task<CategoryReq?> editCategory(Category categoryEntity);
        public Task<bool> deleteCategory(Category categoryEntity);
    }
}
