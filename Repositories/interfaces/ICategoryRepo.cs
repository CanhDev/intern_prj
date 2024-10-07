using intern_prj.Data_response;
using intern_prj.Helper;

namespace intern_prj.Repositories.interfaces
{
    public interface ICategoryRepo
    {
        public Task<Api_response> getCategories();
        public Task<Api_response> getCategory(int id);
        public Task<Api_response> addCategory(CategoryRes categoryRes);
        public Task<Api_response> editCategory(int id, CategoryRes categoryRes);
        public Task<Api_response> deleteCategory(int id);
    }
}
