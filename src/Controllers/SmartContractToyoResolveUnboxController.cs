using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendToyo.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendToyo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartContractToyoResolveUnboxController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SmartContractToyoResolveUnboxController(AppDbContext context)
        {
            _context = context;
        }
        //GET: api/SmartContractToyoResolveUnbox
        [HttpGet]
        [Authorize(Roles = "Block Chain Service")]
        public async Task<IEnumerable> GetSmartContractToyoResolveUnbox(int tokenId, String walletAdress)
        {
            return null;
        }
    }
}