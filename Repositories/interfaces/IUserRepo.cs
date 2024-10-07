using intern_prj.Helper;

namespace intern_prj.Repositories.interfaces
{
    public interface IUserRepo
    {
        public Task<Api_response> GetUserAsync(string email);

    }
}
