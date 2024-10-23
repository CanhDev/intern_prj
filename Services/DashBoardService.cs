using intern_prj.Helper;
using intern_prj.Repositories;
using intern_prj.Repositories.interfaces;
using intern_prj.Services.interfaces;

namespace intern_prj.Services
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IDashboardRepo _dashboardRepo;

        public DashBoardService(IDashboardRepo dashboardRepo)
        {
            _dashboardRepo = dashboardRepo;
        }
        public async Task<Api_response> GetRevenueStatistics(string? startDate, string? endDate)
        {
            return await _dashboardRepo.GetRevenueStatistics(startDate, endDate);
        }

        public async Task<Api_response> GetTopSelling(int count = 5)
        {
            return await _dashboardRepo.GetTopSelling(count);
        }
    }
}
