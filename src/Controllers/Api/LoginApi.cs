using System.Threading.Tasks;
using BackendToyo.Models.ResponseEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendToyo.Controllers.Api
{
    [Produces("Application/json")]
    [Route("api/login")]
    [ApiController]
    public abstract class LoginApi : ControllerBase
    {

        [HttpGet("authorization")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseStatusEntity), StatusCodes.Status401Unauthorized)]
        public abstract Task<ActionResult<JsonWebTokenViewModel>> Authorize(
            [FromHeader(Name = "authorization")] string authorization,
            [FromHeader(Name = "refresh_token")] string refreshToken
            );

    }
}