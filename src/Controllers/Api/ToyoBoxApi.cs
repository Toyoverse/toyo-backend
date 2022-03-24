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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseStatusEntity), StatusCodes.Status404NotFound)]
        public abstract Task<ActionResult<List<BoxesViewModel>>> GetBoxes([FromQuery] string walletAddress);        

        [HttpGet("sortBox")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseStatusEntity), StatusCodes.Status404NotFound)]
        public abstract Task<ActionResult<SortViewModel>> sortBox(int TokenId, string walletAddress);
    
        [HttpGet("getParts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseStatusEntity), StatusCodes.Status404NotFound)]
        public abstract Task<ActionResult<List<BoxesViewModel>>> getParts(string walletAddress);
    }
}