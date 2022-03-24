using System.Threading.Tasks;
using BackendToyo.Enums;
using BackendToyo.Models.DataEntities;

namespace BackendToyo.Services
{
     public interface ILoginService
    {
        public Task<UserInfo> GetUserInfos(string authenticationHeader);
        public Task ValidateLogin(UserInfo infos);
        public Task<UserInfo> GetUser(string login);
        public AuthorizationType GetAuthorizationType(string authorization);
    }

}