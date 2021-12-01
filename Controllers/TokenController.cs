using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendToyo.Data;
using BackendToyo.Models;
using BackendToyo.Middleware;

namespace BackendToyo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TokenController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TokenController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Token
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Token>>> GetTokens()
        {
            return await _context.Tokens.ToListAsync();
        }

        // GET: api/Token/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Token>> GetToken(Guid id)
        {
            var token = await _context.Tokens.FindAsync(id);

            if (token == null)
            {
                return NotFound();
            }

            return token;
        }

        // PUT: api/Token/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToken(Guid id, Token token)
        {
            if (id != token.Id)
            {
                return BadRequest();
            }

            _context.Entry(token).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TokenExists(id))
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

        // POST: api/Token
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Token>> PostToken(Token token)
        {
            _context.Tokens.Add(token);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToken", new { id = token.Id }, token);
        }

        // POST: api/Token/New
        [HttpPost("New")]
        public async Task<ActionResult<Token>> PostNewToken(TokenViewModel tokenView)
        {
            Token token = new Token();
            TxTokenPlayer txTokenPlayer = new TxTokenPlayer();
            Player player = new Player();

            var _typeToken = await _context.TypeTokens.FirstOrDefaultAsync(p => p.TypeId == tokenView.TypeId);

            token.Id = Guid.NewGuid();
            token.NFTId = tokenView.TokenId;
            token.TypeId = _typeToken.Id;
            
            player.Id = Guid.NewGuid();
            player.WalletAddress = tokenView.WalletAddress;
            player.JoinTimeStamp = generateTimestamp.GetTimestamp(DateTime.Now);
            
            _context.Tokens.Add(token);
            _context.Players.Add(player);

            await _context.SaveChangesAsync();

            txTokenPlayer.Id = Guid.NewGuid();
            txTokenPlayer.TxHash = tokenView.TransactionHash;
            txTokenPlayer.BlockNumber = tokenView.BlockNumber;
            txTokenPlayer.PlayerId = player.Id;
            txTokenPlayer.TokenId = token.Id;
            txTokenPlayer.TxTimeStamp = generateTimestamp.GetTimestamp(DateTime.Now);
            txTokenPlayer.ChainId = tokenView.ChainId;

            _context.TxsTokenPlayer.Add(txTokenPlayer);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToken", new { id = token.Id }, token);
        }

        // DELETE: api/Token/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToken(Guid id)
        {
            var token = await _context.Tokens.FindAsync(id);
            if (token == null)
            {
                return NotFound();
            }

            _context.Tokens.Remove(token);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TokenExists(Guid id)
        {
            return _context.Tokens.Any(e => e.Id == id);
        }
    }
}
