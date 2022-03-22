using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendToyo.Data;
using BackendToyo.Models;
using BackendToyo.Models.DataEntities;

namespace BackendToyo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartContractToyoSyncController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SmartContractToyoSyncController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SmartContractToyoSync
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<IEnumerable<SmartContractToyoSync>>> GetSmartContractToyoSyncs()
        {
            return await _context.SmartContractToyoSyncs.ToListAsync();
        }

        // GET: api/SmartContractToyoSync/5
        [HttpGet("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<SmartContractToyoSync>> GetSmartContractToyoSync(string id)
        {
            var smartContractToyoSync = await _context.SmartContractToyoSyncs.FindAsync(id);

            if (smartContractToyoSync == null)
            {
                return NotFound();
            }

            return smartContractToyoSync;
        }

        [HttpGet("getLastBlockNumber")]
        public async Task<ActionResult<int>> GetLastBlockNumber(string chainId, string contractAddress, string eventName)
        {            
            var smartContractToyoSync = await _context.SmartContractToyoSyncs.FirstOrDefaultAsync(p => p.ChainId == chainId && p.ContractAddress == contractAddress && p.EventName == eventName);
            
            if (smartContractToyoSync == null)
            {
                return NotFound();
            }

            return smartContractToyoSync.LastBlockNumber;

        }

        // PUT: api/SmartContractToyoSync/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> PutSmartContractToyoSync(string id, SmartContractToyoSync smartContractToyoSync)
        {
            if (id != smartContractToyoSync.ChainId)
            {
                return BadRequest();
            }

            _context.Entry(smartContractToyoSync).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmartContractToyoSyncExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SmartContractToyoSync
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SmartContractToyoSync>> PostSmartContractToyoSync(SmartContractToyoSync smartContractToyoSync)
        { 
            var ToyoSync = await _context.SmartContractToyoSyncs.FirstOrDefaultAsync(p => p.ChainId == smartContractToyoSync.ChainId && p.ContractAddress == smartContractToyoSync.ContractAddress && p.EventName == smartContractToyoSync.EventName);
            try
            {
                 if (ToyoSync == null)
                {
                    _context.SmartContractToyoSyncs.Add(smartContractToyoSync);
                } else {
                    ToyoSync.LastBlockNumber = smartContractToyoSync.LastBlockNumber;
                }
                
                await _context.SaveChangesAsync();
                
                return smartContractToyoSync;
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error: ");
                Console.WriteLine(e.ToString());
                return NotFound(e);
            }   
            
        }

        // DELETE: api/SmartContractToyoSync/5
        [HttpDelete("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> DeleteSmartContractToyoSync(string id)
        {
            var smartContractToyoSync = await _context.SmartContractToyoSyncs.FindAsync(id);
            if (smartContractToyoSync == null)
            {
                return NotFound();
            }

            _context.SmartContractToyoSyncs.Remove(smartContractToyoSync);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SmartContractToyoSyncExists(string id)
        {
            return _context.SmartContractToyoSyncs.Any(e => e.ChainId == id);
        }
    }
}
