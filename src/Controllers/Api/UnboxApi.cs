using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendToyo.Models;
using BackendToyo.Models.ResponseEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackendToyo.Middleware;

namespace BackendToyo.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Unbox")]
    [ApiController]
    public abstract class UnboxApi : ControllerBase
    {
        [HttpGet("sortUnbox")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ResponseStatusEntity), 404)]
        public abstract Task<ActionResult<SortViewModel>> sortUnbox(int tokenId, string walletAddress);
    }
}