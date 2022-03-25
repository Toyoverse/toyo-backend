using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendToyo.Data;
using BackendToyo.Models.DataEntities;
using Microsoft.AspNetCore.Authorization;

namespace BackendToyo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartContractToyoMintController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SmartContractToyoMintController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SmartContractToyoMint
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        //[Authorize(Roles = "Block Chain Service")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<SmartContractToyoMint>>> GetSmartContractToyoMints()
        {
            return await _context.SmartContractToyoMints.ToListAsync();
        }

        // GET: api/SmartContractToyoMint/5
        [HttpGet("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Roles = "Block Chain Service")]
        public async Task<ActionResult<SmartContractToyoMint>> GetSmartContractToyoMint(string id)
        {
            var smartContractToyoMint = await _context.SmartContractToyoMints.FindAsync(id);

            if (smartContractToyoMint == null)
            {
                return NotFound();
            }

            return smartContractToyoMint;
        }

        // PUT: api/SmartContractToyoMint/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Block Chain Service")]
        public async Task<IActionResult> PutSmartContractToyoMint(string id, SmartContractToyoMint smartContractToyoMint)
        {
            if (id != smartContractToyoMint.TransactionHash)
            {
                return BadRequest();
            }

            _context.Entry(smartContractToyoMint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmartContractToyoMintExists(id))
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

        // POST: api/SmartContractToyoMint
        [HttpPost]
        [Authorize(Roles = "Block Chain Service")]
        public async Task<ActionResult<SmartContractToyoMint>> PostSmartContractToyoMint(SmartContractToyoMint smartContractToyoMint)
        {

            var ToyoMint = await _context.SmartContractToyoMints.FirstOrDefaultAsync(p => p.TransactionHash == smartContractToyoMint.TransactionHash && p.TokenId == smartContractToyoMint.TokenId && p.ChainId == smartContractToyoMint.ChainId);
            
            try
            {
                  if (ToyoMint == null)
                {
                    _context.SmartContractToyoMints.Add(smartContractToyoMint);
                } else {
                    ToyoMint = smartContractToyoMint;
                }
                
                await _context.SaveChangesAsync();
                
                return smartContractToyoMint;
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error: ");
                Console.WriteLine(e.ToString());
                return NotFound(e);
            }   
           

           /*  _context.SmartContractToyoMints.Add(smartContractToyoMint);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SmartContractToyoMintExists(smartContractToyoMint.TransactionHash))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSmartContractToyoMint", new { id = smartContractToyoMint.TransactionHash }, smartContractToyoMint); */
        }

        // DELETE: api/SmartContractToyoMint/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Block Chain Service")]
        public async Task<IActionResult> DeleteSmartContractToyoMint(string id)
        {
            if (!SmartContractToyoMintExists(id))
            {
                return NotFound();
            }

            var smartContractToyoMint = await _context.SmartContractToyoMints.FindAsync(id);
            _context.SmartContractToyoMints.Remove(smartContractToyoMint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SmartContractToyoMintExists(string id)
        {
            return _context.SmartContractToyoMints.Any(e => e.TransactionHash == id);
        }
    }
}
