using AutoMapper;
using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

namespace intern_prj.Repositories
{
    public class FeedBackRepo : IFeedBackRepo
    {
        private readonly DecorContext _context;
        private readonly IMapper _mapper;

        public FeedBackRepo(DecorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Api_response> GetFeedBacks(int orderId)
        {
            try
            {
                var feedBacks = await _context.FeedBacks.Where(f => f.OrderId == orderId).ToListAsync();
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<List<FeedBackReq>>(feedBacks)
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

        public async Task<Api_response> SendFeedBack(FeedBackRes feedBackRes)
        {
            try
            {
                var feedBackEntity = _mapper.Map<FeedBack>(feedBackRes);
                _context.FeedBacks.Add(feedBackEntity);
                await _context.SaveChangesAsync();
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<FeedBackReq>(feedBackEntity)
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
        public async Task<Api_response> DeleteFeedBack(int FeedBackId)
        {
            try
            {
                var feedBackDelete = await _context.FeedBacks.FindAsync(FeedBackId);
                if(feedBackDelete != null)
                {
                    _context.FeedBacks.Remove(feedBackDelete);
                    await _context.SaveChangesAsync();
                    return new Api_response
                    {
                        success = true,
                        data = FeedBackId
                    };
                }
                else
                {
                    return new Api_response
                    {
                        success = false,
                        message = "Feedback does not exist"
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
