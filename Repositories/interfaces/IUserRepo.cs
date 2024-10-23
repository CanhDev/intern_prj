using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;

namespace intern_prj.Repositories.interfaces
{
    public interface IUserRepo
    {
        public Task<List<ApplicationUser>> GetUsers_Admin();
        public Task<ApplicationUser?> GetUser_Admin(string id);
        public Task<ApplicationUser?> CreateUserAsync_Admin(ApplicationUser userEntity, string password);
        public Task<ApplicationUser?> UpdateUserAsync_Admin(ApplicationUser userEntity);
        public Task<bool> DeleteUserAsync_Admin(string id);
        public Task<ApplicationUser?> GetUserAsync_Client(string id);
        public Task<ApplicationUser?> UpdateUserAsync_Client(ApplicationUser userEntity);
        public Task<ApplicationUser?> ChangePassword_Client(string id, string oldPassword, string newPassword);
    }
}
