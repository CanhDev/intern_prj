using intern_prj.Data_response;
using intern_prj.Helper;

namespace intern_prj.Repositories.interfaces
{
    public interface IFeedBackRepo
    {
        public Task<Api_response> GetFeedBacks(int orderId);
        public Task<Api_response> SendFeedBack(FeedBackRes feedBackRes);
        public Task<Api_response> DeleteFeedBack(int FeedBackId);
    }
}
