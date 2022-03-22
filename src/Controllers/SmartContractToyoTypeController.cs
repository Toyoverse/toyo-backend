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
    public class SmartContractToyoTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SmartContractToyoTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SmartContractToyoType
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<IEnumerable<SmartContractToyoType>>> GetSmartContractToyoTypes()
        {
            return await _context.SmartContractToyoTypes.ToListAsync();
        }

        // GET: api/SmartContractToyoType/5
        [HttpGet("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<SmartContractToyoType>> GetSmartContractToyoType(string id)
        {
            var smartContractToyoType = await _context.SmartContractToyoTypes.FindAsync(id);

            if (smartContractToyoType == null)
            {
                return NotFound();
            }

            return smartContractToyoType;
        }

        // PUT: api/SmartContractToyoType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> PutSmartContractToyoType(string id, SmartContractToyoType smartContractToyoType)
        {
            if (id != smartContractToyoType.TransactionHash)
            {
                return BadRequest();
            }

            _context.Entry(smartContractToyoType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmartContractToyoTypeExists(id))
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

        // POST: api/SmartContractToyoType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SmartContractToyoType>> PostSmartContractToyoType(SmartContractToyoType smartContractToyoType)
        {
            _context.SmartContractToyoTypes.Add(smartContractToyoType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SmartContractToyoTypeExists(smartContractToyoType.TransactionHash))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSmartContractToyoType", new { id = smartContractToyoType.TransactionHash }, smartContractToyoType);
        }

        // DELETE: api/SmartContractToyoType/5
        [HttpDelete("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> DeleteSmartContractToyoType(string id)
        {
            var smartContractToyoType = await _context.SmartContractToyoTypes.FindAsync(id);
            if (smartContractToyoType == null)
            {
                return NotFound();
            }

            _context.SmartContractToyoTypes.Remove(smartContractToyoType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SmartContractToyoTypeExists(string id)
        {
            return _context.SmartContractToyoTypes.Any(e => e.TransactionHash == id);
        }
    }
}
