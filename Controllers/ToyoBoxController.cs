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
using System.Text.Json;
using System.Text.Json.Serialization;

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

        [HttpGet("getParts")]
        public async Task<ActionResult<List<BoxesViewModel>>> getParts(string walletAddress, string chainId)
        {            
            var query = from sctt in _context.Set<SmartContractToyoTransfer>()
                        join sctm in _context.Set<SmartContractToyoMint>()
                            on sctt.TokenId equals sctm.TokenId
                        join sctty in _context.Set<SmartContractToyoType>()
                            on sctm.TypeId equals sctty.TypeId
                        where sctt.WalletAddress == walletAddress && sctt.ChainId == chainId && sctty.TypeId >= 3 && sctty.TypeId <= 13
                        select new BoxesViewModel(sctt.TokenId, sctm.TypeId, sctty.Name);

            return await query.ToListAsync();
        }

        [HttpGet("getToyos")]
        public async Task<ActionResult<List<BoxesViewModel>>> getToyos(string walletAddress, string chainId)
        {            
            var query = from sctt in _context.Set<SmartContractToyoTransfer>()
                        join sctm in _context.Set<SmartContractToyoMint>()
                            on sctt.TokenId equals sctm.TokenId
                        join sctty in _context.Set<SmartContractToyoType>()
                            on sctm.TypeId equals sctty.TypeId
                        where sctt.WalletAddress == walletAddress && sctt.ChainId == chainId && sctty.TypeId == 21
                        select new BoxesViewModel(sctt.TokenId, sctm.TypeId, sctty.Name);

            return await query.ToListAsync();
        }

        /* [HttpPost("postToyo")]
        public Task<ActionResult<bool>> postToyo(string walletAddress, string chainId) {

            return false;
        } */

        [HttpGet("sortBox")]
        public SortViewModel sortBox(string TypeId, string TokenId, string walletAddress, bool Fortified = false)
        {
            SortViewModel toyoRaffle = new SortViewModel();
            toyoRaffle = raffle.main(Fortified);

            /* var query = from scts in _context.Set<SmartContractToyoSwap>()
                        join sctt in _context.Set<SmartContractToyoTransfer>()
                            on new {
                                _toyoTransaction = scts.TransactionHash,
                                _toyoChain = scts.ChainId
                            } equals new {
                                _toyoTransaction = sctt.TransactionHash,
                                _toyoChain = sctt.ChainId
                            }
                        where scts.FromTokenId == TokenId && sctt.WalletAddress == WalletAddress && scts.ToTypeId == 21
                        select new { scts.TransactionHash, scts.ChainId, scts.ToTokenId };

            var swapValue = await query.ToListAsync();

            if(swapVallue.length > 0) {
                List<data> _data = new List<data>();
                _data.Add(new data()
                {
                    FromTypeId = TypeId,
                    ToTypeId = 21,
                    FromTokenId = TokenId,
                    ToTokenId = swapValue.ToTokenId,
                    WalletAddress = walletAddress,
                    attributes = new Array()
                });
            } */

            return toyoRaffle;




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
                       <data> _data = new List<data>();
                _data.Add(new data()
                {
                    FromTypeId = TypeId,
                    ToTypeId = 22,
                    FromTokenId = TokenId,
                    ToTokenId = ,
                    WalletAddress = ,
                });     "trait_type": "Vitality", 
                            "value": 1
                        },
                        {
                            "display_type": "number", 
                            "trait_type": "Strength", 
                            "value": 1
                        },  //baseURL: "https://0.0.0.0:5001/api",
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

        [HttpPost("postPercentageBonus")]
        public bool postPorcentageBonus([FromForm] string bonus)
        {
            Console.WriteLine("bonus: " + bonus);
            return true;
        }
    }
}