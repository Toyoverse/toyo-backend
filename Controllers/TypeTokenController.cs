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
    public class TypeTokenController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TypeTokenController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TypeToken
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeToken>>> GetTypeTokens()
        {
            return await _context.TypeTokens.ToListAsync();
        }

        // GET: api/TypeToken/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeToken>> GetTypeToken(Guid id)
        {
            var typeToken = await _context.TypeTokens.FindAsync(id);

            if (typeToken == null)
            {
                return NotFound();
            }

            return typeToken;
        }

        // PUT: api/TypeToken/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeToken(Guid id, TypeToken typeToken)
        {
            if (id != typeToken.Id)
            {
                return BadRequest();
            }

            _context.Entry(typeToken).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeTokenExists(id))
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

        // POST: api/TypeToken
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeToken>> PostTypeToken(TypeToken typeToken)
        {
            _context.TypeTokens.Add(typeToken);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeToken", new { id = typeToken.Id }, typeToken);
        }

        // DELETE: api/TypeToken/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeToken(Guid id)
        {
            var typeToken = await _context.TypeTokens.FindAsync(id);
            if (typeToken == null)
            {
                return NotFound();
            }

            _context.TypeTokens.Remove(typeToken);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeTokenExists(Guid id)
        {
            return _context.TypeTokens.Any(e => e.Id == id);
        }
    }
}
