using AutoMapper;
using intern_prj.Data_request;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace intern_prj.Repositories
{
    public class DashboardRepo : IDashboardRepo
    {
        private readonly DecorContext _context;
        private readonly IMapper _mapper;

        public DashboardRepo(DecorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Api_response> GetRevenueStatistics(string? startDate, string? endDate)
        {
            try
            {
                var query = from o in _context.Orders
                            join od in _context.OrderDetails on o.Id equals od.OrderId
                            join p in _context.Products on od.ProductId equals p.Id
                            where o.OrderDate.HasValue
                            select new
                            {
                                CreatedDate = o.OrderDate.Value.Date,
                                Quantity = od.UnitQuantity ?? 0,
                                Price = p.Price,
                                OriginalPrice = p.OriginalPrice
                            };

                // Định dạng lại ngày tháng
                if (!string.IsNullOrEmpty(startDate) && DateTime.TryParseExact(startDate, "yyyy-MM-dd",
                    null, System.Globalization.DateTimeStyles.None, out DateTime start))
                {
                    query = query.Where(x => x.CreatedDate >= start);
                }

                if (!string.IsNullOrEmpty(endDate) && DateTime.TryParseExact(endDate, "yyyy-MM-dd",
                    null, System.Globalization.DateTimeStyles.None, out DateTime end))
                {
                    // Thay đổi cách so sánh ngày kết thúc để bao gồm cả ngày cuối cùng
                    query = query.Where(x => x.CreatedDate < end.AddDays(1));
                }

                var result = await query
                    .GroupBy(x => x.CreatedDate)
                    .Select(x => new
                    {
                        Date = x.Key,
                        TotalBuy = x.Sum(y => y.Quantity * y.OriginalPrice),
                        TotalSell = x.Sum(y => y.Quantity * y.Price)
                    })
                    .Select(x => new
                    {
                        Date = x.Date,
                        Revenue = x.TotalSell,
                        Profit = x.TotalSell - x.TotalBuy,
                        ProfitMargin = x.TotalSell != 0 ? (x.TotalSell - x.TotalBuy) / x.TotalSell * 100 : 0
                    })
                    .OrderBy(x => x.Date)
                    .ToListAsync();

                return new Api_response
                {
                    success = true,
                    data = result
                };
            }
            catch (Exception ex)
            {
                return new Api_response
                {
                    success = false,
                    message = ex.Message
                };
            }
        }


        public async Task<Api_response> GetTopSelling(int count = 5)
        {
            try
            {
                var products = await _context.Products.Include(p => p.Images)
                                                    .OrderByDescending(p => p.SoldedCount).Take(count).ToListAsync();
                if(products != null)
                {
                    return new Api_response
                    {
                        success = true,
                        data = _mapper.Map<List<productReq>>(products)
                    };
                }
                return new Api_response
                {
                    success = false,
                    message = "Lỗi tải danh sách"
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
    }
}
