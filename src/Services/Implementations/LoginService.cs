using System;
using System.Buffers.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BackendToyo.Enums;
using BackendToyo.Exceptions;
using BackendToyo.Middleware;
using BackendToyo.Models.DataEntities;
using BackendToyo.Repositories;
using BackendToyo.Utils;
using Microsoft.Extensions.Configuration;

namespace BackendToyo.Services.Implementations
{
   
    public class LoginService : ILoginService
    {
        private IUserRepository _userRepository;
        public LoginService(IConfiguration configuration, IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task ValidateLogin(UserInfo infos)
        {
            var userExists = await _userRepository.UserExists(infos);
            var isValidPassword = await _userRepository.IsValidPassword(infos);
            if (!userExists) throw new NotFoundException("User Not Found");
            if (!isValidPassword) throw new InvalidPasswordException("Invalid Password");
        }

        public async Task<UserInfo> GetUserInfos(string authenticationHeader)
        {
            if (!authenticationHeader.Contains("Basic ")) return null;

            var filteredHeader = authenticationHeader.Replace("Basic ", "", StringComparison.InvariantCultureIgnoreCase);

            string login = this.GetLogin(filteredHeader);
            string password = this.GetPassword(filteredHeader);
            string encryptedPassword = await this.EncryptPassword(password);

            return new UserInfo()
            {
                Login = login,
                Password = encryptedPassword
            };
        }

        public AuthorizationType GetAuthorizationType(string authorization)
        {
            string method = authorization.Split(" ",StringSplitOptions.TrimEntries)[0];
            if(method.Equals("basic", StringComparison.CurrentCultureIgnoreCase)) return AuthorizationType.BASIC;
            if(method.Equals("bearer", StringComparison.CurrentCultureIgnoreCase)) return AuthorizationType.BEARER;
            throw new InvalidCredentialsException("Invalid credential type");
        }

        private async Task<string> EncryptPassword(string password)
        {
            byte[] bytes;

            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                using (Stream inputStream = password.ToStream())
                {
                    // ComputeHash - returns byte array  
                    bytes = await sha256Hash.ComputeHashAsync(inputStream);
                }
            }
            StringBuilder builder = new StringBuilder();
            // Convert byte array to a string   
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        private string GetPassword(string filteredHeader)
        {
            string decodedAuth = EncodingUtils.Base64Decode(filteredHeader);
            int index = decodedAuth.IndexOf(":");
            string password = decodedAuth.Substring(++index);
            return password;
        }

        private string GetLogin(string filteredHeader)
        {
            string decodedAuth = EncodingUtils.Base64Decode(filteredHeader);
            string login = decodedAuth.Split(":", StringSplitOptions.TrimEntries)[0];
            return login;
        }

        public async Task<UserInfo> GetUser(string login)
        {
            return await _userRepository.FindByLogin(login);
        }
    }
}