using AutoMapper;
using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;

namespace intern_prj.Helper
{
    public class Mapper : Profile
    {
        public Mapper() {
            //productMap
            CreateMap<Product, productReq>().ReverseMap();
            CreateMap<Product, productRes>().ReverseMap();
            CreateMap<productRes, productReq>().ReverseMap();
            CreateMap<Image, imageReq>().ReverseMap();
            CreateMap<imageReq, imageRes>().ReverseMap();
            CreateMap<Image, imageRes>().ReverseMap();

            //CategoryMap
            CreateMap<Category, CategoryReq>().ReverseMap();
            CreateMap<Category, CategoryRes>().ReverseMap();
            CreateMap<CategoryRes, CategoryReq>().ReverseMap(); 

            //ItemcartMap
            CreateMap<ItemCart, ItemCartReq>().ReverseMap();
            CreateMap<ItemCart, ItemCartRes>().ReverseMap();
            CreateMap<ItemCartRes,  ItemCartReq>().ReverseMap();    

            //Order & OrderDetail
            CreateMap<Order, OrderRes>().ReverseMap();
            CreateMap<Order, OrderReq>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailRes>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailReq>().ReverseMap();

            //FeedBack
            CreateMap<FeedBack, FeedBackRes>().ReverseMap();    
            CreateMap<FeedBack, FeedBackReq>().ReverseMap();

            //User
            CreateMap<UserRes, UserReq>().ReverseMap();
            CreateMap<ApplicationUser, UserReq>().ReverseMap();
            CreateMap<ApplicationUser, UserRes>().ReverseMap();
            
            //Cart
            CreateMap<Cart, CartReq>().ReverseMap();
        }
    }
}
