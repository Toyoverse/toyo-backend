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

    public class ToyoBoxController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ToyoBoxController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getBoxes")]
        public async Task<ActionResult<List<BoxesViewModel>>> GetBoxes(string walletAddress, string chainId)
        {            
            var query = from sctt in _context.Set<SmartContractToyoTransfer>()
                        join sctm in _context.Set<SmartContractToyoMint>()
                            on sctt.TokenId equals sctm.TokenId
                        join sctty in _context.Set<SmartContractToyoType>()
                            on sctm.TypeId equals sctty.TypeId
                        where sctt.WalletAddress == walletAddress && sctt.ChainId == chainId  && sctty.Name.Contains("Box") == true
                        select new BoxesViewModel(sctt.TokenId, sctm.TypeId, sctty.Name);

            return await query.ToListAsync();
        }

        [HttpGet("sortBox")]
        public SortViewModel sortBox(string TypeId, string TokenId, string name, bool Fortified = false)
        {
            
            return raffle.main(Fortified);

            /*
                {
                    "name": "Tatsu",
                    "attributes": [
                        {
                            "display_type": "number",
                            "trait_type": "Type",
                            "value": 9
                        },
                        {
                            "trait_type": "Toyo",
                            "value": "Tatsu"
                        },
                        {
                            "trait_type": "Region",
                            "value": "Kytunt"
                        },
                        {
                            "trait_type": "Rarity",
                            "value": "Common Edition"
                        },
                        {
                            "display_type": "number", 
                            "trait_type": "Vitality", 
                            "value": 1
                        },
                        {
                            "display_type": "number", 
                            "trait_type": "Strength", 
                            "value": 1
                        },
                        {
                            "display_type": "number", 
                            "trait_type": "Resistance", 
                            "value": 1
                        },
                        {
                            "display_type": "number", 
                            "trait_type": "CyberForce", 
                            "value": 1
                        },
                        {
                            "display_type": "number", 
                            "trait_type": "Resilience", 
                            "value": 1
                        },
                        {
                            "display_type": "number", 
                            "trait_type": "Precision", 
                            "value": 1
                        },
                        {
                            "display_type": "number", 
                            "trait_type": "Technique", 
                            "value": 1
                        },
                        {
                            "display_type": "number", 
                            "trait_type": "Analysis", 
                            "value": 1
                        },
                        {
                            "display_type": "number", 
                            "trait_type": "Speed", 
                            "value": 1
                        },
                        {
                            "display_type": "number", 
                            "trait_type": "Agility", 
                            "value": 1
                        },
                        {
                            "display_type": "number", 
                            "trait_type": "Stamina", 
                            "value": 1
                        },
                        {
                            "display_type": "number", 
                            "trait_type": "Luck", 
                            "value": 1
                        }
                    ]
                }
            */
        }

        [HttpPost("postPorcentageBonus")]
        public bool postPorcentageBonus(int toyoId, int bonus)
        {
            Console.WriteLine("toyoID: " + toyoId + " bonus: " + bonus);
            return true;
        }
    }
}