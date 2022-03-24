using System.Threading.Tasks;
using BackendToyo.Data;
using BackendToyo.Models.DataEntities;
using Microsoft.EntityFrameworkCore;

namespace BackendToyo.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<UserInfo> _users;
        
        public UserRepository(AppDbContext context)
        {
            _users = context.Users;
        }

        public async Task<UserInfo> FindByLogin(string login)
        {
            return await _users.AsNoTracking().SingleOrDefaultAsync(u => u.Login == login);
        }

        public async Task<bool> IsValidPassword(UserInfo info)
        {
            return await _users.AnyAsync(p => p.Login == info.Login && p.Password == info.Password);
        }

        public async Task<bool> UserExists(UserInfo info)
        {
            return await _users.AnyAsync(p => p.Login == info.Login);
        }
    }
}