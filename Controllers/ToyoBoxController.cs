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
                new AttributesJson { display_type = "string", trait_type = "Type", value = "9" },
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

            ToyoPlayer _toyoPlayer = new ToyoPlayer {
                ToyoId = toyoRaffle.toyoRaridade, 
                TokenId = swapReturn[0].ToTokenId,
                Vitality = toyoRaffle.qStats[1],
                Strength = toyoRaffle.qStats[2],
                Resistance = toyoRaffle.qStats[3],
                CyberForce = toyoRaffle.qStats[4],
                Resilience = toyoRaffle.qStats[5],
                Precision = toyoRaffle.qStats[6],
                Technique = toyoRaffle.qStats[7],
                Analysis = toyoRaffle.qStats[8],
                Speed = toyoRaffle.qStats[9],
                Agility = toyoRaffle.qStats[10],
                Stamina = toyoRaffle.qStats[11],
                Luck = toyoRaffle.qStats[12],
                WalletAddress = walletAddress,
                ChainId = chainId,
                ChangeValue = false
            };

            await saveToyoPlayer(_toyoPlayer);

            for(int i = 1; i <= 10; i++) {
                if(i != 2) {
                    await savePartPlayer(toyoRaffle.toyoRaridade, i, swapReturn[0].ToTokenId, walletAddress, chainId, toyoRaffle.qParts[i][0], toyoRaffle.qParts[i][1]);
                }
            }
            
            string json = JsonSerializer.Serialize(toyoJson);
            json = json.Replace("\"display_type\":\"string\",", "");
            for(int i = 0; i<10;i++) {
                json = json.Replace($"\"{i}", $"{i}");
                json = json.Replace($"{i}\"", $"{i}");
            }
            json = json.Replace("mp4", "mp4\"");

            await System.IO.File.WriteAllTextAsync($"/tmp/toyoverse/{swapReturn[0].ToTokenId}.json", json);
            //await System.IO.File.WriteAllTextAsync($"/tmp/toyoverse/1010.json", json);

            toyoRaffle.toyoId = swapReturn[0].ToTokenId;

            return toyoRaffle;
        }

        [HttpPost("postPercentageBonus")]
        public async Task<bool> postPorcentageBonus(string Ym9udXM, string dG9rZW5JZA, string wallet)
        {
            Console.WriteLine("MARS >>>> ENTROU");
            int _bonusCode = Convert.ToInt32(base64DecodeEncode.Base64Decode(Ym9udXM));
            string tokenId = base64DecodeEncode.Base64Decode(dG9rZW5JZA);
            string chainId = wallet.Split(";")[1];
            string walletAddress = wallet.Split(";")[0];
            float[] porcentageBonus = new float[] { 1, 1.01f, 1.02f, 1.03f, 1.04f, 1.05f, 1.08f, 1.11f, 1.14f, 1.17f, 1.2f };

            int[] numBase = new int[] { 0, 582, 49751, 67412, 714, 65852, 4414, 8857445, 5114514, 222541, 6367 };

            ToyoPlayer _toyoPlayer = new ToyoPlayer(); 
            Toyo _toyo = new Toyo();
                    
            var queryToyoPlayer = from toyoPlayer in _context.Set<ToyoPlayer>()
                                        where toyoPlayer.TokenId == Convert.ToInt32(tokenId) && toyoPlayer.WalletAddress == walletAddress && toyoPlayer.ChainId == chainId 
                                        select toyoPlayer;

            _toyoPlayer = await queryToyoPlayer.FirstOrDefaultAsync();
            Console.WriteLine("MARS >>>> CHANGE VALUE {0}", _toyoPlayer.ChangeValue);
            if (_toyoPlayer.ChangeValue == false) {
                Console.WriteLine("MARS >>>> ENTROU IF");

                int _bonus = Array.IndexOf(numBase, _bonusCode);
                Console.WriteLine("MARS >>>> PEGOU BONUS {0}", _bonus);
                if(_bonus <= 10 && _bonus > 0 ) {
                    Console.WriteLine("MARS >>>> ENTROU BONUS");
                    var queryToyo = from toyo in _context.Set<Toyo>()
                                                where toyo.Id == _toyoPlayer.ToyoId
                                                select toyo;

                    _toyo = await queryToyo.FirstOrDefaultAsync();

                    _toyoPlayer.Vitality = Convert.ToInt32(Math.Floor(_toyoPlayer.Vitality * porcentageBonus[_bonus]));  
                    _toyoPlayer.Strength = Convert.ToInt32(Math.Floor(_toyoPlayer.Strength * porcentageBonus[_bonus]));  
                    _toyoPlayer.Resistance = Convert.ToInt32(Math.Floor(_toyoPlayer.Resistance * porcentageBonus[_bonus]));  
                    _toyoPlayer.CyberForce = Convert.ToInt32(Math.Floor(_toyoPlayer.CyberForce * porcentageBonus[_bonus]));  
                    _toyoPlayer.Resilience = Convert.ToInt32(Math.Floor(_toyoPlayer.Resilience * porcentageBonus[_bonus]));  
                    _toyoPlayer.Precision = Convert.ToInt32(Math.Floor(_toyoPlayer.Precision * porcentageBonus[_bonus]));  
                    _toyoPlayer.Technique = Convert.ToInt32(Math.Floor(_toyoPlayer.Technique * porcentageBonus[_bonus]));  
                    _toyoPlayer.Analysis = Convert.ToInt32(Math.Floor(_toyoPlayer.Analysis * porcentageBonus[_bonus]));  
                    _toyoPlayer.Speed = Convert.ToInt32(Math.Floor(_toyoPlayer.Speed * porcentageBonus[_bonus]));  
                    _toyoPlayer.Agility = Convert.ToInt32(Math.Floor(_toyoPlayer.Agility * porcentageBonus[_bonus]));  
                    _toyoPlayer.Stamina = Convert.ToInt32(Math.Floor(_toyoPlayer.Stamina * porcentageBonus[_bonus]));  
                    _toyoPlayer.Luck = Convert.ToInt32(Math.Floor(_toyoPlayer.Luck * porcentageBonus[_bonus]));   
                    _toyoPlayer.ChangeValue = true;

                    List<AttributesJson> attributes = new List<AttributesJson> {
                        new AttributesJson { display_type = "string", trait_type = "Type", value = "9" },
                        new AttributesJson { display_type = "string", trait_type = "Toyo", value = _toyo.Name },
                        new AttributesJson { display_type = "string", trait_type = "Region", value = _toyo.Region },
                        new AttributesJson { display_type = "string", trait_type = "Rarity", value = (_toyo.Rarity == 1 ? "Common Edition" : (_toyo.Rarity == 2 ? "Uncommon Edition" : (_toyo.Rarity == 3 ? "Rare Edition" : (_toyo.Rarity == 4 ? "limited Edition" : (_toyo.Rarity == 5 ? "Collectors Edition" : "Prototype Edition" ))))) },
                        new AttributesJson { display_type = "number", trait_type = "Vitality", value = _toyoPlayer.Vitality.ToString() },
                        new AttributesJson { display_type = "number", trait_type = "Strength", value = _toyoPlayer.Strength.ToString() },
                        new AttributesJson { display_type = "number", trait_type = "Resistance", value = _toyoPlayer.Resistance.ToString() },
                        new AttributesJson { display_type = "number", trait_type = "CyberForce", value = _toyoPlayer.CyberForce.ToString() },
                        new AttributesJson { display_type = "number", trait_type = "Resilience", value = _toyoPlayer.Resilience.ToString() },
                        new AttributesJson { display_type = "number", trait_type = "Precision", value = _toyoPlayer.Precision.ToString() },
                        new AttributesJson { display_type = "number", trait_type = "Technique", value = _toyoPlayer.Technique.ToString() },
                        new AttributesJson { display_type = "number", trait_type = "Analysis", value = _toyoPlayer.Analysis.ToString() },
                        new AttributesJson { display_type = "number", trait_type = "Speed", value = _toyoPlayer.Speed.ToString() },
                        new AttributesJson { display_type = "number", trait_type = "Agility", value = _toyoPlayer.Agility.ToString() },
                        new AttributesJson { display_type = "number", trait_type = "Stamina", value = _toyoPlayer.Stamina.ToString() },
                        new AttributesJson { display_type = "number", trait_type = "Luck", value = _toyoPlayer.Luck.ToString() }
                    };

                    await _context.SaveChangesAsync();

                    ToyoJson toyoJson = new ToyoJson() {
                        name =_toyo.Name,
                        description = _toyo.Desc,
                        image = _toyo.Thumb,
                        animation_url = _toyo.Video,
                        attributes = attributes.ToArray()
                    };

                    string json = JsonSerializer.Serialize(toyoJson);
                    json = json.Replace("\"display_type\":\"string\",", "");
                    for(int i = 0; i<10;i++) {
                        json = json.Replace($"\"{i}", $"{i}");
                        json = json.Replace($"{i}\"", $"{i}");
                    }
                    json = json.Replace("mp4", "mp4\"");
                    await System.IO.File.WriteAllTextAsync($"/tmp/toyoverse/{tokenId}.json", json);
                }
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
        public async Task<bool> saveToyoPlayer(ToyoPlayer _toyoPlayer) {
            var toyoPlayer = await _context.ToyosPlayer.FirstOrDefaultAsync(p => p.WalletAddress == _toyoPlayer.WalletAddress && p.ChainId == _toyoPlayer.ChainId && p.TokenId == _toyoPlayer.TokenId && p.ToyoId == _toyoPlayer.ToyoId);

            if (toyoPlayer == null)
            {
                _context.ToyosPlayer.Add(_toyoPlayer);
            } else {
                toyoPlayer = _toyoPlayer;
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