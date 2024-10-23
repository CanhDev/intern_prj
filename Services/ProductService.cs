using AutoMapper;
using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Repositories.interfaces;
using intern_prj.Services.interfaces;

namespace intern_prj.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        private readonly ImageHepler _imageHepler;


        public ProductService(IProductRepo productRepo, IMapper mapper, ImageHepler imageHepler) {
            _productRepo = productRepo;
            _mapper = mapper;
            _imageHepler = imageHepler;
        }

        public Task<Api_response> GetProductsAsync
            (int? typeId, string? sortString, string? filterString, int pageNumber = 1, int pageSize = 3, string? role = "")
        {
            return _productRepo.GetProductsAsync(typeId, sortString, filterString, pageNumber, pageSize, role);
        }

        public async Task<Api_response> GetProductAsync(int id)
        {
            var productEntity = await _productRepo.GetProductAsync(id);
            if(productEntity == null)
            {
                return new Api_response
                {
                    success = false,
                    message = "Product does not exist"
                };
            }
            else
            {
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<productReq>(productEntity)
                };
            }
        }

        public async Task<Api_response> AddProductAsync(productRes productRes)
        {
            try
            {
                var productEntity = _mapper.Map<Product>(productRes);

                if (productRes.imgs != null && productRes.imgs.Count > 0)
                {
                    foreach (var img in productRes.imgs)
                    {
                        var imgFileName = await _imageHepler.saveImage(img, "products");
                        if (imgFileName != null)
                        {
                            productEntity.Images.Add(new Image { ImageUrl = imgFileName });
                        }
                    }
                }
                productEntity.CreateDate = DateTime.Now;
                var productRequest = await _productRepo.AddProductAsync(productEntity);
                return new Api_response
                {
                    success = true,
                    data = productRequest
                };
            }
            catch(Exception ex)
            {
                return new Api_response
                {
                    success = false,
                    message = ex.Message
                };
            }
        }

        public async Task<Api_response> DeleteProductAsync(int id)
        {
            try
            {
                var productEntity = await _productRepo.GetProductAsync(id);
                if(productEntity == null)
                {
                    return new Api_response
                    {
                        success = false,
                        message = "product does not exist"
                    };
                }
                else
                {
                    await _productRepo.DeleteProductAsync(productEntity);
                    return new Api_response
                    {
                        success = true,
                        data = id
                    };
                }
                
            }
            catch(Exception ex)
            {
                return new Api_response
                {
                    success = false,
                    message = ex.Message
                };
            }
        }

        public async Task<Api_response> EditProductAsync(productRes productRes, int id)
        {
            try
            {
                var productEntity = await _productRepo.GetProductAsync(id);
                if(productEntity == null)
                {
                    return new Api_response
                    {
                        success = false,
                        message = "product does not exist"
                    };
                }
                else
                {
                    productRes.CreateDate = productEntity.CreateDate;
                    _mapper.Map(productRes, productEntity);
                    if (productRes.ImageUrls.Count > 0)
                    {
                        productEntity.Images.Clear();
                        foreach (var img in productRes.ImageUrls)
                        {
                            productEntity.Images.Add(new Image { ProductId = id, ImageUrl = img });
                        }
                    }
                    if (productRes.imgs != null && productRes.imgs.Count > 0)
                    {
                        foreach (var img in productRes.imgs)
                        {
                            var imgFileName = await _imageHepler.saveImage(img, "products");
                            if (imgFileName != null)
                            {
                                productEntity.Images.Add(new Image { ProductId = id, ImageUrl = imgFileName });
                            }
                        }
                    }
                    var productRequest = await _productRepo.EditProductAsync(productEntity);
                    return new Api_response
                    {
                        success = true,
                        data = productRequest
                    };
                }
            }
            catch(Exception ex)
            {
                return new Api_response
                {
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
