using intern_prj.Data_response;
using intern_prj.Helper;

namespace intern_prj.Repositories.interfaces
{
    public interface IUserRepo
    {
        public Task<Api_response> GetUsers_Admin();
        public Task<Api_response> GetUser_Admin(string id);
        public Task<Api_response> CreateUserAsync_Admin(UserRes userRes);
        public Task<Api_response> UpdateUserAsync_Admin(UserRes userRes, string id, string newPassword);
        public Task<Api_response> DeleteUserAsync_Admin(string id);
        public Task<Api_response> GetUserAsync_Client(string id);
        public Task<Api_response> UpdateUserAsync_Client(UserRes userRes, string id);
        public Task<Api_response> ChangePassword_Client(string id, string oldPassword, string newPassword);
    }
}
