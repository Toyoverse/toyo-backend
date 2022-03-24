using System.Threading.Tasks;
using BackendToyo.Models.DataEntities;

namespace BackendToyo.Services
{
    public interface ITokenService
    {
        public string GenerateToken(UserInfo info);
        public int GetExpiringJwtTime();
        public string GenerateRefreshToken(string token);
        public Task ValidateRefreshToken(string refreshToken, string jwtToken);
        public Task<UserInfo> GetUserInfo(string token);
    }
}