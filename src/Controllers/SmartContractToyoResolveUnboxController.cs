using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendToyo.Data;
using BackendToyo.Models;
using BackendToyo.Models.DataEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        //[Authorize(Roles = "Block Chain Service")]
        public async Task<ActionResult<IEnumerable<SmartContractToyoSwap>>> GetSmartContractToyoResolveUnbox(int tokenId, String walletAdress)
        {
            var swap = await _context.SmartContractToyoSwaps.Where(p=> p.ToTokenId == tokenId).ToListAsync();
            var transfer = await _context.SmartContractToyoTransfers.Where(p => p.TokenId == tokenId).ToListAsync();
            var mint = await _context.SmartContractToyoMints.Where(p=> p.TokenId == tokenId).ToListAsync();
            var toyoPlayer = await _context.ToyosPlayer.Where(p=> p.WalletAddress == walletAdress && p.TokenId == tokenId).ToListAsync();
            
            try
            {
                if (swap ==null || swap.Count() != 1) error();
                if (transfer ==null || transfer.Count() != 1) error();
                if (mint == null || mint.Count() != 1) error();
                if (toyoPlayer == null || toyoPlayer.Count() != 1) error();
            }
            catch(System.Exception e)
            {
                Console.WriteLine("Error: ");
                Console.WriteLine(e.ToString());
                return NotFound(e);
            }
            //toDo
            return null; //Alterar o tipo de retorno ao termino da task
        }

        private void error()
        {
            throw new SystemException();
        }
    }
}