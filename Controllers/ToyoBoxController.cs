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
using System.Diagnostics;

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
                        join owners in (
                            from _sctt in _context.Set<SmartContractToyoTransfer>()
                            where _sctt.ChainId == chainId
                            select new { _sctt.TokenId, _sctt.ChainId, _sctt.BlockNumber } into _scttS
                            group _scttS by new { _scttS.TokenId, _scttS.ChainId  } into _scttGroup
                            select new { TokenId = _scttGroup.Key.TokenId, ChainId = _scttGroup.Key.ChainId, BlockNumber = _scttGroup.Select(p => p.BlockNumber).Max() } 
                        ) on new {
                                _toyoTokenId = sctt.TokenId,
                                _toyoChain = sctt.ChainId,
                                _toyoBlockNumber = sctt.BlockNumber
                            } equals new {
                                _toyoTokenId = owners.TokenId ,
                                _toyoChain = owners.ChainId,
                                _toyoBlockNumber = owners.BlockNumber
                            } 
                        join sctm in _context.Set<SmartContractToyoMint>()
                            on owners.TokenId equals sctm.TokenId 
                        join sctty in _context.Set<SmartContractToyoType>()
                            on sctm.TypeId equals sctty.TypeId        
                        join tt in _context.Set<TypeToken>()
                            on sctty.TypeId equals tt.TypeId
                        where sctt.WalletAddress == walletAddress && sctt.ChainId == chainId && tt.Type == "box"
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
                        join tt in _context.Set<TypeToken>()
                            on sctty.TypeId equals tt.TypeId
                        where sctt.WalletAddress == walletAddress && sctt.ChainId == chainId && tt.Type == "parts"
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
                        join tt in _context.Set<TypeToken>()
                            on sctty.TypeId equals tt.TypeId
                        where sctt.WalletAddress == walletAddress && sctt.ChainId == chainId && tt.Type == "toyo"
                        select new BoxesViewModel(sctt.TokenId, sctm.TypeId, sctty.Name);

            return await query.ToListAsync();
        }

        /* [HttpPost("postToyo")]
        public Task<ActionResult<bool>> postToyo(string walletAddress, string chainId) {

            return false;
        } */

        [HttpGet("sortBox")]
        public async Task<ActionResult<SortViewModel>> sortBox(int TypeId, int TokenId, string walletAddress, string chainId, bool Fortified = false)
        {
            SortViewModel toyoRaffle = new SortViewModel();
            toyoRaffle = raffle.main(Fortified);
      
            var queryToyo = from toyo in _context.Set<Toyo>()
                            where toyo.Id == toyoRaffle.toyoRaridade
                            select toyo;

            var toyoReturn = await queryToyo.ToListAsync();   

            List<AttributesJson> attributes = new List<AttributesJson> {
                new AttributesJson { display_type = "number", trait_type = "Type", value = "9" },
                new AttributesJson { display_type = "string", trait_type = "Toyo", value = toyoReturn[0].Name },
                new AttributesJson { display_type = "string", trait_type = "Region", value = toyoReturn[0].Region },
                new AttributesJson { display_type = "string", trait_type = "Rarity", value = (toyoRaffle.raridade == 1 ? "Common Edition" : (toyoRaffle.raridade == 2 ? "Uncommon Edition" : (toyoRaffle.raridade == 3 ? "Rare Edition" : (toyoRaffle.raridade == 4 ? "limited Edition" : (toyoRaffle.raridade == 5 ? "Collectors Edition" : "Prototype Edition" ))))) },
                new AttributesJson { display_type = "number", trait_type = "Vitality", value = toyoRaffle.qStats[1].ToString() },
                new AttributesJson { display_type = "number", trait_type = "Strength", value = toyoRaffle.qStats[2].ToString() },
                new AttributesJson { display_type = "number", trait_type = "Resistance", value = toyoRaffle.qStats[3].ToString() },
                new AttributesJson { display_type = "number", trait_type = "CyberForce", value = toyoRaffle.qStats[4].ToString() },
                new AttributesJson { display_type = "number", trait_type = "Resilience", value = toyoRaffle.qStats[5].ToString() },
                new AttributesJson { display_type = "number", trait_type = "Precision", value = toyoRaffle.qStats[6].ToString() },
                new AttributesJson { display_type = "number", trait_type = "Technique", value = toyoRaffle.qStats[7].ToString() },
                new AttributesJson { display_type = "number", trait_type = "Analysis", value = toyoRaffle.qStats[8].ToString() },
                new AttributesJson { display_type = "number", trait_type = "Speed", value = toyoRaffle.qStats[9].ToString() },
                new AttributesJson { display_type = "number", trait_type = "Agility", value = toyoRaffle.qStats[10].ToString() },
                new AttributesJson { display_type = "number", trait_type = "Stamina", value = toyoRaffle.qStats[11].ToString() },
                new AttributesJson { display_type = "number", trait_type = "Luck", value = toyoRaffle.qStats[12].ToString() }
            };

            ToyoJson toyoJson = new ToyoJson() {
                name = toyoReturn[0].Name,
                description = toyoReturn[0].Desc,
                image = toyoReturn[0].Thumb,
                animation_url = toyoReturn[0].Video,
                attributes = attributes.ToArray()
            };

            List<SwapToyo> swapReturn = new List<SwapToyo>();
            do {
                swapReturn = await SwapFunction(TokenId, walletAddress, chainId);
            } while(swapReturn.Count() == 0);

            await saveToyoPlayer(toyoRaffle.toyoRaridade, swapReturn[0].ToTokenId, toyoRaffle.qStats[1], toyoRaffle.qStats[2], toyoRaffle.qStats[3], toyoRaffle.qStats[4], toyoRaffle.qStats[5], toyoRaffle.qStats[6], toyoRaffle.qStats[7], toyoRaffle.qStats[8], toyoRaffle.qStats[9], toyoRaffle.qStats[10], toyoRaffle.qStats[11], toyoRaffle.qStats[12], walletAddress, chainId);

            for(int i = 1; i <= 10; i++) {
                if(i != 2) {
                    await savePartPlayer(toyoRaffle.toyoRaridade, i, swapReturn[0].ToTokenId, walletAddress, chainId, toyoRaffle.qParts[i][0], toyoRaffle.qParts[i][1]);
                }
            }
            
            string json = JsonSerializer.Serialize(toyoJson);
            await System.IO.File.WriteAllTextAsync($"/tmp/toyoverse/{swapReturn[0].ToTokenId}.json", json);
            //await System.IO.File.WriteAllTextAsync($"/tmp/toyoverse/1010.json", json);

            toyoRaffle.toyoId = swapReturn[0].ToTokenId;

            return toyoRaffle;
        }

        [HttpPost("postPercentageBonus")]
        public async Task<bool> postPorcentageBonus([FromForm] string bonus, [FromForm] string tokenId)
        {
            int _bonus = Convert.ToInt32(bonus);
            //int _tokenId = Convert.ToInt32(bonus.Split(';')[1]);
            float[] porcentageBonus = new float[] { 1, 1.01f, 1.02f, 1.03f, 1.04f, 1.05f, 1.08f, 1.11f, 1.14f, 1.17f, 1.2f };
            Console.WriteLine(_bonus);
            Console.WriteLine(tokenId);
            if(_bonus <= 10 && _bonus > 0 ) {
                System.IO.StreamReader file = new System.IO.StreamReader($"/tmp/toyoverse/{tokenId}.json");
                //System.IO.StreamReader file = new System.IO.StreamReader($"/tmp/toyoverse/1010.json");
                string jsonString = file.ReadToEnd();
                ToyoJson toyoJson = JsonSerializer.Deserialize<ToyoJson>(jsonString);
                AttributesJson[] attributes = toyoJson.attributes;
                
                for(int i = 4; i < 16; i++) {                    
                    float newValue = Convert.ToInt32(attributes[i].value) * porcentageBonus[_bonus];
                    attributes[i].value = Convert.ToInt32(Math.Floor(newValue)).ToString();
                }

                toyoJson.attributes = attributes;

                string json = JsonSerializer.Serialize(toyoJson);
                await System.IO.File.WriteAllTextAsync($"/tmp/toyoverse/{tokenId}.json", json);
            }

            return true;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<List<SwapToyo>> SwapFunction(int TokenId, string walletAddress, string chainId) {
            var query = from scts in _context.Set<SmartContractToyoSwap>()
                        join sctt in _context.Set<SmartContractToyoTransfer>()
                            on new {
                                _toyoTransaction = scts.TransactionHash,
                                _toyoChain = scts.ChainId,
                                _toyoTokenId = scts.ToTokenId
                            } equals new {
                                _toyoTransaction = sctt.TransactionHash,
                                _toyoChain = sctt.ChainId,
                                _toyoTokenId = sctt.TokenId
                            }
                        join sctty in _context.Set<SmartContractToyoType>()
                            on scts.ToTypeId equals sctty.TypeId   
                        join tt in _context.Set<TypeToken>()
                            on sctty.TypeId equals tt.TypeId
                        where scts.FromTokenId == TokenId && sctt.WalletAddress == walletAddress && sctt.ChainId == chainId && tt.Type == "toyo"
                        select new SwapToyo { TransactionHash = scts.TransactionHash, ChainId = scts.ChainId, ToTokenId = scts.ToTokenId, TypeToken = tt.TypeId, Name = sctty.Name };

            return await query.ToListAsync();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> saveToyoPlayer(int toyoId, int tokenId, int vitality, int strength, int resistance, int cyberForce, int resilience, int precision, int technique, int analysis, int speed, int agility, int stamina, int luck, string walletAddress, string chainId) {
            var toyoPlayer = await _context.ToyosPlayer.FirstOrDefaultAsync(p => p.WalletAddress == walletAddress && p.ChainId == chainId && p.TokenId == tokenId && p.ToyoId == toyoId);

            var newToyo = new ToyoPlayer{
                    ToyoId = toyoId,
                    TokenId = tokenId,
                    Vitality = vitality,
                    Strength = strength,
                    Resistance = resistance,
                    CyberForce = cyberForce,
                    Resilience = resilience,
                    Precision = precision,
                    Technique = technique,
                    Analysis = analysis,
                    Speed = speed,
                    Agility = agility,
                    Stamina = stamina,
                    Luck = luck,
                    WalletAddress = walletAddress,
                    ChainId = chainId
                };

            if (toyoPlayer == null)
            {
                _context.ToyosPlayer.Add(newToyo);
            } else {
                toyoPlayer = newToyo;
            }
            
            await _context.SaveChangesAsync();
            
            return true;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> savePartPlayer(int toyoId, int part, int tokenId, string walletAddress, string chainId, int statId, int bonusStat) {
            var partFind = await _context.Parts.FirstOrDefaultAsync(p => p.TorsoId == toyoId && p.Part == part);
            var partPlayer = await _context.PartsPlayer.FirstOrDefaultAsync(p => p.WalletAddress == walletAddress && p.ChainId == chainId && p.TokenId == tokenId && p.PartId == partFind.Id);
            
            var newPart = new PartPlayer {
                    PartId = partFind.Id,
                    StatId = statId,
                    BonusStat = bonusStat,
                    TokenId = tokenId,
                    WalletAddress = walletAddress,
                    ChainId = chainId 
                };

            if (partPlayer == null)
            {
                _context.PartsPlayer.Add(newPart);
            } else {
                partPlayer = newPart;
            }
            
            await _context.SaveChangesAsync();
            
            return true;
        }
    }
}