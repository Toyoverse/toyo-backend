using System.Threading.Tasks;
using BackendToyo.Models.DataEntities;

namespace BackendToyo.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> UserExists(UserInfo info);
        public Task<bool> IsValidPassword(UserInfo info);
        public Task<UserInfo> FindByLogin(string login);
    }
}