using System;
using System.Threading.Tasks;
using BackendToyo.Controllers.Api;
using BackendToyo.Enums;
using BackendToyo.Exceptions;
using BackendToyo.Models.DataEntities;
using BackendToyo.Models.ResponseEntities;
using BackendToyo.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendToyo.Controllers
{
    public class LoginController : LoginApi
    {
        private ILoginService _loginService;
        private ITokenService _tokenService;
        public LoginController(ILoginService loginService, ITokenService tokenService)
        {
            _loginService = loginService;
            _tokenService = tokenService;
        }
        public override async Task<ActionResult<JsonWebTokenViewModel>> Authorize(string authorization, string refreshToken)
        {
            AuthorizationType authorizationType = _loginService.GetAuthorizationType(authorization);
            switch (authorizationType)
            {
                case AuthorizationType.BASIC:
                    return await GetTokenFromBasicAuth(authorization);
                case AuthorizationType.BEARER:
                    return await GetTokenFromRefreshToken(authorization, refreshToken);
                default:
                    throw new InvalidCredentialsException("Tipo de autenticação inválido");
            };
        }

        private async Task<ActionResult<JsonWebTokenViewModel>> GetTokenFromRefreshToken(string authorization, string refreshToken)
        {
            string token = authorization.Replace("bearer","", StringComparison.CurrentCultureIgnoreCase).Trim();
            await _tokenService.ValidateRefreshToken(refreshToken, token);
            UserInfo info = await _tokenService.GetUserInfo(token);
            token = _tokenService.GenerateToken(info);
            refreshToken = _tokenService.GenerateRefreshToken(token);   
            return Ok(new JsonWebTokenViewModel(){
                AccessToken = token,
                ExpiresInMinutes = _tokenService.GetExpiringJwtTime(),
                RefreshToken = refreshToken
            });
        }

        private async Task<ActionResult<JsonWebTokenViewModel>> GetTokenFromBasicAuth(string authorization)
        {
            UserInfo infos;
            JsonWebTokenViewModel jwt;
            infos = await _loginService.GetUserInfos(authorization);
            await _loginService.ValidateLogin(infos);
            UserInfo user = await _loginService.GetUser(infos.Login);
            var token = _tokenService.GenerateToken(user);
            jwt = new JsonWebTokenViewModel()
            {
                AccessToken = token,
                ExpiresInMinutes = _tokenService.GetExpiringJwtTime(),
                RefreshToken = _tokenService.GenerateRefreshToken(token)
            };
            return Ok(jwt);
        }
    }
}