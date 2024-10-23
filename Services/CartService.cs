using AutoMapper;
using intern_prj.Data_request;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Repositories.interfaces;
using intern_prj.Services.interfaces;

namespace intern_prj.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartRepo;
        private readonly IMapper _mapper;

        public CartService(ICartRepo cartRepo, IMapper mapper)
        {
            _cartRepo = cartRepo;
            _mapper = mapper;
        }

        public async Task<Api_response> GetCartAsync(string userId)
        {
            try
            {
                var cartEntity = await _cartRepo.GetCartAsync(userId);
                return new Api_response
                {
                    success = true,
                    data = _mapper.Map<CartReq>(cartEntity)
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

        public async Task<Api_response> InitCart(string userId)
        {
            try
            {
                var cartEntity = await _cartRepo.GetCartAsync(userId);
                if(cartEntity != null)
                {
                    return new Api_response
                    {
                        success = false,
                        message = "Cart is already exist"
                    };
                }
                else
                {
                    cartEntity = new Cart
                    {
                        UserId = userId,
                        CreateDate = DateTime.UtcNow
                    };
                    await _cartRepo.InitCart(cartEntity);
                    return new Api_response
                    {
                        success = true,
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
