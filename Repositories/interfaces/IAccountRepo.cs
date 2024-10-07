using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Helper;
using intern_prj.Helper.jwtSerivce;
using Microsoft.AspNetCore.Identity;

namespace intern_prj.Repositories.interfaces
{
    public interface IAccountRepo
    {
        public Task<Api_response> SignUpAsync(SignUpRes model);
        public Task<Api_response> LoginAsync(LoginRes model);
        public Task<Api_response> RenewTokenAsync(TokenModel model);
        public Task<Api_response> GetUserAsync(string email);
    }
}
