using AutoMapper;
using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace intern_prj.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly DecorContext _context;
        private readonly IMapper _mapper;

        public CategoryRepo(DecorContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Api_response> getCategories()
        {
            var Categories = await _context.Categories.ToListAsync();
            if(Categories == null)
            {
                return new Api_response
                {
                    success = false,
                    message = "Danh mục không tồn tại"
                };
            }
            return new Api_response
            {
                success = true,
                data = _mapper.Map<List<CategoryReq>>(Categories)
            };
        }

        public async Task<Api_response> getCategory(int id)
        {
            var categoryQuery = await _context.Categories.FindAsync(id);
            if(categoryQuery == null)
            {
                throw new Exception();
            }
            return new Api_response
            {
                success = true,
                data = _mapper.Map<CategoryReq>(categoryQuery)
            };
        }
        public async Task<Api_response> addCategory(CategoryRes categoryRes)
        {
            try
            {
                var categoryOrigin = _mapper.Map<Category>(categoryRes);
                _context.Categories.Add(categoryOrigin);
                await _context.SaveChangesAsync();
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<CategoryReq>(categoryOrigin)
                };
            }
            catch (Exception ex)
            {
                return new Api_response{
                    success = false,
                    message = ex.Message
                };
            }

        }

        public async Task<Api_response> deleteCategory(int id)
        {
            var categoryOrigin = await _context.Categories.FindAsync(id);
            if( categoryOrigin == null)
            {
                return new Api_response
                {
                    success = false,
                    message = "Danh mục không tồn tại"
                };
            }
            else
            {
                _context.Categories.Remove(categoryOrigin);
                await _context.SaveChangesAsync();
                return new Api_response
                {
                    success = true,
                    data = id
                };
            }
        }

        public async Task<Api_response> editCategory(int id, CategoryRes categoryRes)
        {
            var categoryOriginal = await _context.Categories.FindAsync(id);

            if (categoryOriginal == null)
            {
                return new Api_response
                {
                    success = false,
                    message = "Danh mục không tồn tại."
                };
            }

            _mapper.Map(categoryRes, categoryOriginal);

            await _context.SaveChangesAsync();

            return new Api_response
            {
                success = true,
                data = _mapper.Map<CategoryReq>(categoryOriginal)
            };
        }
    }
}
