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
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TxTokenPlayerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TxTokenPlayerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TxTokenPlayer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TxTokenPlayer>>> GetTxsTokenPlayer()
        {
            return await _context.TxsTokenPlayer.ToListAsync();
        }

        // GET: api/TxTokenPlayer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TxTokenPlayer>> GetTxTokenPlayer(Guid id)
        {
            var txTokenPlayer = await _context.TxsTokenPlayer.FindAsync(id);

            if (txTokenPlayer == null)
            {
                return NotFound();
            }

            return txTokenPlayer;
        }

        // PUT: api/TxTokenPlayer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTxTokenPlayer(Guid id, TxTokenPlayer txTokenPlayer)
        {
            if (id != txTokenPlayer.Id)
            {
                return BadRequest();
            }

            _context.Entry(txTokenPlayer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TxTokenPlayerExists(id))
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

        // POST: api/TxTokenPlayer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TxTokenPlayer>> PostTxTokenPlayer(TxTokenPlayer txTokenPlayer)
        {
            _context.TxsTokenPlayer.Add(txTokenPlayer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTxTokenPlayer", new { id = txTokenPlayer.Id }, txTokenPlayer);
        }

        // DELETE: api/TxTokenPlayer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTxTokenPlayer(Guid id)
        {
            var txTokenPlayer = await _context.TxsTokenPlayer.FindAsync(id);
            if (txTokenPlayer == null)
            {
                return NotFound();
            }

            _context.TxsTokenPlayer.Remove(txTokenPlayer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TxTokenPlayerExists(Guid id)
        {
            return _context.TxsTokenPlayer.Any(e => e.Id == id);
        }
    }
}
