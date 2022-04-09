using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendToyo.Models;
using BackendToyo.Models.DataEntities;
using BackendToyo.Models.ResponseEntities;
using BackendToyo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BackendToyo.Controllers
{
    [Produces("application/json")]
    [Route("api/ToyoBox")]
    [ApiController]
    public abstract class ToyoBoxApi : ControllerBase
    {
        [HttpGet("getBoxes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ResponseStatusEntity), 404)]
        public abstract Task<ActionResult<List<BoxesViewModel>>> GetBoxes([FromQuery] string walletAddress);        

        [HttpGet("sortBox")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ResponseStatusEntity),404)]
        public abstract Task<ActionResult<SortViewModel>> sortBox(int TokenId, string walletAddress);
    
        [HttpGet("getParts")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ResponseStatusEntity), 404)]
        public abstract Task<ActionResult<List<BoxesViewModel>>> getParts(string walletAddress);

        [HttpPost("postPercentageBonus")]
        [ProducesResponseType(typeof(ResponseStatusEntity), 200)]
        public abstract Task<ActionResult<ResponseStatusEntity>> postPorcentageBonus(PorcentageBonusView porcentageBonusView);
    }
}