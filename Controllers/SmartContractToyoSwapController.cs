using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendToyo.Data;
using BackendToyo.Models;

namespace BackendToyo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartContractToyoSwapController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SmartContractToyoSwapController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SmartContractToyoSwap
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<IEnumerable<SmartContractToyoSwap>>> GetSmartContractToyoSwaps()
        {
            return await _context.SmartContractToyoSwaps.ToListAsync();
        }

        // GET: api/SmartContractToyoSwap/5
        [HttpGet("{FromTypeId}")]
        [ApiExplorerSettings(IgnoreApi = true)]

        public async Task<ActionResult<SmartContractToyoSwap>> GetSmartContractToyoSwap(int FromTokenId, string ChainId)
        {
            var smartContractToyoSwap = await _context.SmartContractToyoSwaps.FindAsync();

            if (smartContractToyoSwap == null)
            {
                return NotFound();
            }

            return smartContractToyoSwap;
        }

        // PUT: api/SmartContractToyoSwap/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> PutSmartContractToyoSwap(string id, SmartContractToyoSwap smartContractToyoSwap)
        {
            if (id != smartContractToyoSwap.TransactionHash)
            {
                return BadRequest();
            }

            _context.Entry(smartContractToyoSwap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmartContractToyoSwapExists(id))
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

        // POST: api/SmartContractToyoSwap
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SmartContractToyoSwap>> PostSmartContractToyoSwap(SmartContractToyoSwap smartContractToyoSwap)
        { 
            var ToyoSwap = await _context.SmartContractToyoSwaps.FirstOrDefaultAsync(p => p.TransactionHash == smartContractToyoSwap.TransactionHash && p.ToTokenId == smartContractToyoSwap.ToTokenId && p.ChainId == smartContractToyoSwap.ChainId);
            
            if (ToyoSwap == null)
            {
                _context.SmartContractToyoSwaps.Add(smartContractToyoSwap);
            } else {
                ToyoSwap = smartContractToyoSwap;
            }
            
            await _context.SaveChangesAsync();
            
            return smartContractToyoSwap;
        }

        // DELETE: api/SmartContractToyoSwap/5
        [HttpDelete("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> DeleteSmartContractToyoSwap(string id)
        {
            var smartContractToyoSwap = await _context.SmartContractToyoSwaps.FindAsync(id);
            if (smartContractToyoSwap == null)
            {
                return NotFound();
            }

            _context.SmartContractToyoSwaps.Remove(smartContractToyoSwap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SmartContractToyoSwapExists(string id)
        {
            return _context.SmartContractToyoSwaps.Any(e => e.TransactionHash == id);
        }
    }
}
