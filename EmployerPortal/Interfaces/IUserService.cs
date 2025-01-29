using EmployerPortal.Models;

namespace EmployerPortal.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByUsernameAsync(string username);
    }
}
