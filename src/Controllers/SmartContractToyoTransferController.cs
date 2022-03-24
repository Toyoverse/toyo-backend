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
using Microsoft.AspNetCore.Authorization;

namespace BackendToyo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartContractToyoTransferController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SmartContractToyoTransferController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SmartContractToyoTransfer
        [HttpGet]
        [Authorize(Roles = "Block Chain Service")]
        public async Task<ActionResult<IEnumerable<SmartContractToyoTransfer>>> GetSmartContractToyoTransfers()
        {
            return await _context.SmartContractToyoTransfers.ToListAsync();
        }

        // GET: api/SmartContractToyoTransfer/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Block Chain Service")]
        public async Task<ActionResult<SmartContractToyoTransfer>> GetSmartContractToyoTransfer(string id)
        {
            var smartContractToyoTransfer = await _context.SmartContractToyoTransfers.FindAsync(id);

            if (smartContractToyoTransfer == null)
            {
                return NotFound();
            }

            return smartContractToyoTransfer;
        }

        // PUT: api/SmartContractToyoTransfer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Block Chain Service")]
        public async Task<IActionResult> PutSmartContractToyoTransfer(string id, SmartContractToyoTransfer smartContractToyoTransfer)
        {
            if (id != smartContractToyoTransfer.TransactionHash)
            {
                return BadRequest();
            }

            _context.Entry(smartContractToyoTransfer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmartContractToyoTransferExists(id))
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

        // POST: api/SmartContractToyoTransfer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Block Chain Service")]
        public async Task<ActionResult<SmartContractToyoTransfer>> PostSmartContractToyoTransfer(SmartContractToyoTransfer smartContractToyoTransfer)
        {

             var ToyoTrans = await _context.SmartContractToyoTransfers.FirstOrDefaultAsync(p => p.TransactionHash == smartContractToyoTransfer.TransactionHash && p.TokenId == smartContractToyoTransfer.TokenId && p.ChainId == smartContractToyoTransfer.ChainId);
            
            try
            {
                 if (ToyoTrans == null)
                {
                    _context.SmartContractToyoTransfers.Add(smartContractToyoTransfer);
                } else {
                    ToyoTrans = smartContractToyoTransfer;
                }

                await _context.SaveChangesAsync();
                return smartContractToyoTransfer;
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error: ");
                Console.WriteLine(e);
                return NotFound(e);
            }              

            /* 

            _context.SmartContractToyoTransfers.Add(smartContractToyoTransfer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SmartContractToyoTransferExists(smartContractToyoTransfer.TransactionHash))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSmartContractToyoTransfer", new { id = smartContractToyoTransfer.TransactionHash }, smartContractToyoTransfer); */
        }

        // DELETE: api/SmartContractToyoTransfer/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Block Chain Service")]
        public async Task<IActionResult> DeleteSmartContractToyoTransfer(string id)
        {
            var smartContractToyoTransfer = await _context.SmartContractToyoTransfers.FindAsync(id);
            if (smartContractToyoTransfer == null)
            {
                return NotFound();
            }

            _context.SmartContractToyoTransfers.Remove(smartContractToyoTransfer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SmartContractToyoTransferExists(string id)
        {
            return _context.SmartContractToyoTransfers.Any(e => e.TransactionHash == id);
        }
    }
}
