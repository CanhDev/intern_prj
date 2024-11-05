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
        private readonly ImageHepler _imageHepler;

        public CategoryRepo(DecorContext context, IMapper mapper, ImageHepler imageHepler) {
            _context = context;
            _mapper = mapper;
            _imageHepler = imageHepler;
        }

        public async Task<List<Category>> getCategories()
        {
             var Categories = await _context.Categories.ToListAsync();
             return Categories;
        }

        public async Task<Category?> getCategory(int id)
        {
             var categoryQuery = await _context.Categories.FindAsync(id);
             return categoryQuery != null ? categoryQuery : null;
        }

        public async Task<CategoryReq?> addCategory(Category categoryEntity)
        {
             _context.Categories.Add(categoryEntity);
             await _context.SaveChangesAsync();
             return _mapper.Map<CategoryReq>(categoryEntity);
        }

        public async Task<bool> deleteCategory(Category categoryEntity)
        {
             _context.Categories.Remove(categoryEntity);
             await _context.SaveChangesAsync();
             return true;
        }

        public async Task<CategoryReq?> editCategory(Category categoryEntity)
        {
             _context.Categories.Attach(categoryEntity);
             _context.Entry(categoryEntity).State = EntityState.Modified;
             await _context.SaveChangesAsync();
             return _mapper.Map<CategoryReq>(categoryEntity);
        }
    }
}
