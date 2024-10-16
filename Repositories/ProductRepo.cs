using AutoMapper;
using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Repositories.interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using X.PagedList.EF;
namespace intern_prj.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly DecorContext _context;
        private readonly IMapper _mapper;
        private readonly ImageHepler _imageHepler;

        public ProductRepo(DecorContext context, IMapper mapper, ImageHepler imageHepler) {
            _context = context;
            _mapper = mapper;
            _imageHepler = imageHepler;
        }

        public async Task<Api_response> GetProductsAsync(int? typeId, string? sortString, string? filterString, int pageNumber = 1, int pageSize = 3)
        {
            // query
            IQueryable<Product> products = _context.Products
            .Include(p => p.Images)
            .Include(p => p.Category);

            int total = products.Count();

            if (typeId != null)
            {
                products = products.Where(p => p.CategoryId == typeId);
            }
            // filter & sort
            products = handleSort(products, sortString);
            products = handleFilter(products, filterString);

            // paging
            var pagedProducts = await products.ToPagedListAsync(pageNumber, pageSize);

            // map
            var productsRequest = _mapper.Map<List<productReq>>(pagedProducts);

            return new Api_response
            {
                success = true,
                data = new
                {
                    totalProduct = total,
                    products = productsRequest
                }
            };
        }

        public async Task<Api_response> GetProductAsync(int id)
        {
            var product = await _context.Products
                            .Include(p => p.Images)
                            .FirstOrDefaultAsync(p => p.Id == id);
            if (product!= null)
            {
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<productReq>(product)
                };
            }
            else
            {
                return new Api_response
                {
                    success = true,
                    data= null
                };
            }
        }
        public async Task<Api_response> AddProductAsync(productRes productRes)
        {
            var productReqest = _mapper.Map<productReq>(productRes);

            if (productRes.imgs != null && productRes.imgs.Count > 0)
            {
                foreach (var img in productRes.imgs)
                {
                    var imgFileName = await _imageHepler.saveImage(img, "products");
                    if (imgFileName != null)
                    {
                        productReqest.Images.Add(new imageReq { ImageUrl = imgFileName });
                    }
                }
            }

            var newProduct = _mapper.Map<Product>(productReqest);
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            //
            var returnProduct = _mapper.Map<productReq>(newProduct);

            return new Api_response
            {
                success = true,
                data = returnProduct
            };
        }
        public async Task<Api_response> EditProductAsync(productRes productRes, int id)
        {
            var editProduct = await _context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
            //
            if(editProduct != null)
            {
                 _mapper.Map(productRes, editProduct);
                //
                if (productRes.ImageUrls.Count > 0)
                {
                    editProduct.Images.Clear();
                    foreach (var img in productRes.ImageUrls)
                    {
                        editProduct.Images.Add(new Image {ProductId = id, ImageUrl = img });
                    }
                }
                if (productRes.imgs != null && productRes.imgs.Count > 0)
                {
                    foreach (var img in productRes.imgs)
                    {
                        var imgFileName = await _imageHepler.saveImage(img, "products");
                        if (imgFileName != null)
                        {
                            editProduct.Images.Add(new Image { ProductId = id, ImageUrl = imgFileName });
                        }
                    }
                }
                //
               await _context.SaveChangesAsync();
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<productReq>(editProduct)
                };
            }
            else
            {
                return new Api_response
                {
                    success = false,
                    data = "Sản phẩm không tồn tại"
                };
            }
        }
        public async Task<Api_response> DeleteProductAsync(int id)
        {
            var deleteProduct = await _context.Products.FindAsync(id);
            if (deleteProduct != null)
            {
                _context.Products.Remove(deleteProduct);
                try
                {
                    await _context.SaveChangesAsync();
                    return new Api_response
                    {
                        success = true,
                        data = id
                    };
                }
                catch (DbUpdateException ex)
                {
                    return new Api_response
                    {
                        success = false,
                        message = "Lỗi khi xóa sản phẩm: " + ex.InnerException?.Message
                    };
                }
            }
            return new Api_response
            {
                success = false,
                message = "Sản phẩm không tồn tại"
            };
        }

        //sort by data & price
        public IQueryable<Product> handleSort(IQueryable<Product> products, string? sortString)
        {
            products = products.OrderBy(p => p.CreateDate);
            if (!string.IsNullOrEmpty(sortString))
            {
                switch(sortString)
                {
                    case "date_desc":
                        products = products.OrderBy(p => p.CreateDate); break;
                    case "price_asc":
                        products = products.OrderBy(p => p.Price); break;
                    case "price_desc":
                        products = products.OrderByDescending(p => p.Price); break;
                }
            }
            return products;
        }
        //filter by name
        public IQueryable<Product> handleFilter(IQueryable<Product> products, string? filterString)
        {
            if (!string.IsNullOrEmpty(filterString))
            {
                products = products.Where(p => p.Name.Contains(filterString));
            }
            return products;
        }
    }
}
