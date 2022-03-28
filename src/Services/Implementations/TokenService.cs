using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using BackendToyo.Utils;
using BackendToyo.Models.DataEntities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Linq;
using BackendToyo.Exceptions;
using BackendToyo.Repositories;
using System.Threading.Tasks;
using System.Threading;

namespace BackendToyo.Services
{

    public class TokenService : ITokenService
    {
        private const string ALGORITHM = "HS256";
        private readonly string _jwtSecret;
        private readonly int _timeToExpire;
        private readonly int _timeToRefresh;
        private readonly IUserRepository _userRepository;

        public TokenService(IConfiguration configuration, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _timeToRefresh = int.Parse(
                Environment.GetEnvironmentVariable("Jwt_Expiring_Time_Minutes") ??
                DefaultValues.TIME_JWT_EXPIRES.ToString()
                );
            _timeToExpire = int.Parse(
                Environment.GetEnvironmentVariable("Jwt_RefreshToken_Expires")
                ?? DefaultValues.TIME_JWT_REFRESH.ToString()
                );
            _jwtSecret =  Environment.GetEnvironmentVariable("Jwt_Secret");
        }

        public string GenerateRefreshToken(string token)
        {
            var concatenatedString = String.Concat(token, _jwtSecret);
            StringBuilder sb = new StringBuilder();
            using (SHA256 sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(concatenatedString.ToByteArray());
                bytes.AsEnumerable().Select(b => b.ToString("x2")).ToList().ForEach(s => sb.Append(s));
            }
            return sb.ToString();
        }

        public string GenerateToken(UserInfo info)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecret);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user", info.Login),
                    new Claim(ClaimTypes.Role, info.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_timeToExpire),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                )
            };
            SecurityToken token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }

        public int GetExpiringJwtTime()
        {
            return this._timeToExpire;
        }

        public async Task<UserInfo> GetUserInfo(string token)
        {
            JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            string login = jwtToken.Claims.SingleOrDefault(c => c.Type.Equals("user", StringComparison.CurrentCultureIgnoreCase)).Value;
            UserInfo info = await _userRepository.FindByLogin(login);
            return info;
        }

        public async Task ValidateRefreshToken(string refreshToken, string jwtToken)
        {
            if (this.GenerateRefreshToken(jwtToken) != refreshToken)
                throw new InvalidTokenException("Invalid refresh token");

            JwtSecurityToken tokenS =new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;            
            
            if(await refreshTokenExpired(tokenS))
                throw new InvalidTokenException("Expired Refresh Token");
        }

        private async Task<bool> refreshTokenExpired(JwtSecurityToken tokenS)
        {            
            return await Task.Run(() => tokenS.ValidTo.AddMinutes(_timeToRefresh) < DateTime.UtcNow );
        }
    }
}