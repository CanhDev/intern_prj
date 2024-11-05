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

        public async Task<Api_response> GetProductsAsync
                (int? typeId, string? sortString, string? filterString, int pageNumber = 1, int pageSize = 3, string? role = "")
        {
            // query
            IQueryable<Product> products = _context.Products
                    .Include(p => p.Images)
                    .Include(p => p.Category);
            if (string.IsNullOrEmpty(role))
            {
                products = products.Where(p => p.OutOfStockstatus == false);
            }
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


        public async Task<List<Product>> GetProductsbyIds(List<int> ids)
        {
             var productsEntityList = await _context.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
             return productsEntityList;
        }

        public async Task<Product?> GetProductAsync(int? id)
        {
            var product = await _context.Products
                            .Include(p => p.Images)
                            .Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            return product != null ? product : null;
        }

        public async Task<productReq?> AddProductAsync(Product productEntity)
        {
            await _context.Products.AddAsync(productEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<productReq>(productEntity);
        }

        public async Task<productReq?> EditProductAsync(Product productEntity)
        {
            _context.Products.Attach(productEntity);
            _context.Entry(productEntity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return _mapper.Map<productReq?>(productEntity);
        }

        public async Task EditProductsAsync(List<Product> productEntityList)
        {
            _context.Products.AttachRange(productEntityList);
            foreach (var product in productEntityList)
            {
                _context.Entry(product).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductAsync(Product productEntity)
        {
             _context.Products.Remove(productEntity);
             await _context.SaveChangesAsync();
             return true;
        }

        //sort by data & price
        public IQueryable<Product> handleSort(IQueryable<Product> products, string? sortString)
        {
            products = products.OrderByDescending(p => p.CreateDate);
            if (!string.IsNullOrEmpty(sortString))
            {
                switch(sortString)
                {
                    case "date_desc":
                        products = products.OrderByDescending(p => p.CreateDate); break;
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
