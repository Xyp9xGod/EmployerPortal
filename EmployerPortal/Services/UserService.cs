using EmployerPortal.Data;
using EmployerPortal.Interfaces;
using EmployerPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployerPortal.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            // Parameterized query using Entity Framework Core
            return await _dbContext.Users
                .FromSqlInterpolated($"SELECT * FROM Users WHERE Username = {username}")
                .FirstOrDefaultAsync();
        }
    }
}
