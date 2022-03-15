using System.Collections.Generic;
using System.Threading.Tasks;
using BackendToyo.Models;
using BackendToyo.Models.ResponseEntities;
using BackendToyo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace BackendToyo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ToyoBoxApi : ControllerBase
    {
        [HttpGet("getBoxes")]
        public abstract Task<ActionResult<List<BoxesViewModel>>> GetBoxes(string walletAddress, string chainId);

        [HttpGet("sortBox")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseStatusEntity), StatusCodes.Status404NotFound)]
        public abstract Task<ActionResult<SortViewModel>> sortBox(
            int TypeId,
            int TokenId,
            string walletAddress,
            string chainId,
            bool Fortified = false, 
            bool Jakana = false);
    }
}