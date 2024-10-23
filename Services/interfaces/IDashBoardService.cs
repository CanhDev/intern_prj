using intern_prj.Helper;

namespace intern_prj.Services.interfaces
{
    public interface IDashBoardService
    {
        public Task<Api_response> GetRevenueStatistics(string? startDate, string? endDate);
        public Task<Api_response> GetTopSelling(int count = 5);
    }
}
