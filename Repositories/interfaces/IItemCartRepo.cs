﻿using intern_prj.Data_response;
using intern_prj.Helper;

namespace intern_prj.Repositories.interfaces
{
    public interface IItemCartRepo
    {
        public Task<Api_response> GetItemCart_Cart(int cartId);
        public Task<Api_response> AddItemCart(ItemCartRes itemCartRes);
        public Task<Api_response> DeleteItemCart(int itemCartId);
        public Task<Api_response> UpdateQuantity(ItemCartRes itemCartRes, int id);
    }
}
