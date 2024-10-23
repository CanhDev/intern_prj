using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Helper.jwtSerivce;
using Microsoft.AspNetCore.Identity;

namespace intern_prj.Repositories.interfaces
{
    public interface IAccountRepo
    {
        public Task<bool> CreateAccount(ApplicationUser userAccount, string password);
        public Task<ApplicationUser?> CheckAccount(LoginRes model);
    }
}
