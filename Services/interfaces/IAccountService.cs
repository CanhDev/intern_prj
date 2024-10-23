using intern_prj.Data_response;
using intern_prj.Helper.jwtSerivce;
using intern_prj.Helper;

namespace intern_prj.Services.interfaces
{
    public interface IAccountService
    {
        public Task<Api_response> SignUpAsync(SignUpRes model);
        public Task<Api_response> LoginAsync(LoginRes model);
        public Task<Api_response> RenewTokenAsync(TokenModel model);
    }
}
