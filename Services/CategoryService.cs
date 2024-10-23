using AutoMapper;
using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Repositories;
using intern_prj.Repositories.interfaces;
using intern_prj.Services.interfaces;

namespace intern_prj.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;
        private readonly ImageHepler _imageHepler;

        public CategoryService(ICategoryRepo categoryRepo, IMapper mapper, ImageHepler imageHepler)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _imageHepler = imageHepler;
        }

        public async Task<Api_response> getCategories()
        {
            try
            {
                var categories = await _categoryRepo.getCategories();
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<List<CategoryReq>>(categories)
                };
            }
            catch(Exception ex)
            {
                return new Api_response
                {
                    success = false,
                    message = ex.Message,
                };
            }
        }

        public async Task<Api_response> getCategory(int id)
        {
            try
            {
                var categoryEntity = await _categoryRepo.getCategory(id);
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<CategoryReq>(categoryEntity)
                };
            }
            catch (Exception ex)
            {
                return new Api_response
                {
                    success = false,
                    message = ex.Message,
                };
            }
        }
        public async Task<Api_response> addCategory(CategoryRes categoryRes)
        {
            try
            {
                var categoryEntity = _mapper.Map<Category>(categoryRes);
                if (categoryRes.image != null)
                {
                    var imageFileName = await _imageHepler.saveImage(categoryRes.image, "types");
                    categoryEntity.imageUrl = imageFileName;
                }
                var categoryRequest = await _categoryRepo.addCategory(categoryEntity);
                return new Api_response
                {
                    success = true,
                    data = categoryRequest
                };
            }
            catch(Exception ex)
            {
                return new Api_response
                {
                    success = false,
                    message = ex.Message,
                };
            }
        }

        public async Task<Api_response> deleteCategory(int id)
        {
            try
            {
                var categoryEntity = await _categoryRepo.getCategory(id);
                if (categoryEntity == null)
                {
                    return new Api_response
                    {
                        success = false,
                        message = "category does not exist"
                    };
                }
                else
                {
                    await _categoryRepo.deleteCategory(categoryEntity);
                    return new Api_response
                    {
                        success = true,
                        data = id
                    };
                }
            }
            catch (Exception ex)
            {
                return new Api_response
                {
                    success = false,
                    message = ex.Message,
                };
            }
        }

        public async Task<Api_response> editCategory(int id, CategoryRes categoryRes)
        {
            try
            {
                var categoryEntity =  await _categoryRepo.getCategory(id);
                if(categoryEntity == null)
                {
                    return new Api_response
                    {
                        success = false,
                        message = "Category does not exist"
                    };
                }
                else
                {
                    _mapper.Map(categoryRes, categoryEntity);
                    if (categoryRes.image != null)
                    {
                        var imageFileName = await _imageHepler.saveImage(categoryRes.image, "types");
                        categoryEntity.imageUrl = imageFileName;
                    }
                    var categoryRequest = await _categoryRepo.editCategory(categoryEntity);
                    return new Api_response
                    {
                        success = true,
                        data = categoryRequest
                    };
                }
            }
            catch(Exception ex)
            {
                return new Api_response
                {
                    success = false,
                    message = ex.Message,
                };
            }
        }
    }
}
