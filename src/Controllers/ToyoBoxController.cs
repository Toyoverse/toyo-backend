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
using BackendToyo.Services;
using Microsoft.Extensions.Configuration;
using System.IO;
using BackendToyo.Models.ResponseEntities;
using System.Threading;
using BackendToyo.Models.DataEntities;
using BackendToyo.Utils;

namespace BackendToyo.Controllers
{
    public class ToyoBoxController : ToyoBoxApi
    {
        private readonly AppDbContext _context;
        private readonly int _timeoutSwapFunction;
        private readonly int _intervalSwapFunctionQuery;
        private readonly string _jsonFolderPath;
        private readonly string _chainId;
        public ToyoBoxController(AppDbContext context, IConfiguration configuration)
        {
            _jsonFolderPath = configuration["Json_Folder"];
            _chainId = configuration["Chain_Id"];
            _timeoutSwapFunction = int.Parse(configuration["Timeout_Swap_Milliseconds"]);
            _intervalSwapFunctionQuery = int.Parse(configuration["Swap_Interval_Milliseconds"]);
            _context = context;
        }

        public override async Task<ActionResult<List<BoxesViewModel>>> GetBoxes(string walletAddress)
        {
            var query = from sctt in _context.Set<SmartContractToyoTransfer>()
                        join owners in (
                            from _sctt in _context.Set<SmartContractToyoTransfer>()
                            where _sctt.ChainId == _chainId
                            select new { _sctt.TokenId, _sctt.ChainId, _sctt.BlockNumber } into _scttS
                            group _scttS by new { _scttS.TokenId, _scttS.ChainId } into _scttGroup
                            select new { TokenId = _scttGroup.Key.TokenId, ChainId = _scttGroup.Key.ChainId, BlockNumber = _scttGroup.Select(p => p.BlockNumber).Max() }
                        ) on new
                        {
                            _toyoTokenId = sctt.TokenId,
                            _toyoChain = sctt.ChainId,
                            _toyoBlockNumber = sctt.BlockNumber
                        } equals new
                        {
                            _toyoTokenId = owners.TokenId,
                            _toyoChain = owners.ChainId,
                            _toyoBlockNumber = owners.BlockNumber
                        }
                        join sctm in _context.Set<SmartContractToyoMint>()
                            on owners.TokenId equals sctm.TokenId
                        join sctty in _context.Set<SmartContractToyoType>()
                            on sctm.TypeId equals sctty.TypeId
                        join tt in _context.Set<TypeToken>()
                            on sctty.TypeId equals tt.Id
                        join box in _context.Set<BoxType>()
                            on tt.Id equals box.TypeId
                        where sctt.WalletAddress == walletAddress && sctt.ChainId == _chainId
                        select new BoxesViewModel(sctt.TokenId, sctm.TypeId, sctty.Name);


            return await query.ToListAsync();
        }

        public override async Task<ActionResult<List<BoxesViewModel>>> getParts(string walletAddress)
        {
            if (! _context.Set<SmartContractToyoTransfer>().Any(s => s.WalletAddress == walletAddress))
            {
                return NotFound(new ResponseStatusEntity(404, "wallet address not found"));
            }

            var query = from sctt in _context.Set<SmartContractToyoTransfer>()
                        join sctm in _context.Set<SmartContractToyoMint>()
                            on sctt.TokenId equals sctm.TokenId
                        join sctty in _context.Set<SmartContractToyoType>()
                            on sctm.TypeId equals sctty.TypeId
                        join tt in _context.Set<TypeToken>()
                            on sctty.TypeId equals tt.Id
                        where sctt.WalletAddress == walletAddress && sctt.ChainId == _chainId && tt.Type == "parts"
                        select new BoxesViewModel(sctt.TokenId, sctm.TypeId, sctty.Name);

            return await query.ToListAsync();
        }

        [HttpGet("getStatusParts")]
        public async Task<ActionResult<List<PartsStatsViewModel>>> getStatusParts(string walletAddress, int tokenId)
        {
            var query = from pp in _context.Set<PartPlayer>()
                        join s in _context.Set<Stat>()
                          on pp.StatId equals s.Id
                        join p in _context.Set<Parts>()
                          on pp.PartId equals p.Id
                        where pp.TokenId == tokenId && pp.WalletAddress == walletAddress && pp.ChainId == _chainId
                        select new PartsStatsViewModel(p.Part, s.Name, pp.BonusStat);

            return await query.ToListAsync();
        }

        [HttpGet("getStatusToyo")]
        public async Task<ActionResult<List<ToyoStatsViewModel>>> getStatusToyo(string walletAddress, int tokenId)
        {
            var query = from tp in _context.Set<ToyoPlayer>()
                        where tp.TokenId == tokenId && tp.WalletAddress == walletAddress && tp.ChainId == _chainId
                        select new ToyoStatsViewModel(tp.ToyoId, tp.Vitality, tp.Strength, tp.Resistance, tp.CyberForce, tp.Resilience, tp.Precision, tp.Technique, tp.Analysis, tp.Speed, tp.Agility, tp.Stamina, tp.Luck);

            return await query.ToListAsync();
        }

        [HttpGet("getToyos")]
        public async Task<ActionResult<List<ToyoViewModel>>> getToyos(string walletAddress)
        {
            var query = from sctt in _context.Set<SmartContractToyoTransfer>()
                        join owners in (
                            from _sctt in _context.Set<SmartContractToyoTransfer>()
                            where _sctt.ChainId == _chainId
                            select new { _sctt.TokenId, _sctt.ChainId, _sctt.BlockNumber } into _scttS
                            group _scttS by new { _scttS.TokenId, _scttS.ChainId } into _scttGroup
                            select new { TokenId = _scttGroup.Key.TokenId, ChainId = _scttGroup.Key.ChainId, BlockNumber = _scttGroup.Select(p => p.BlockNumber).Max() }
                        ) on new
                        {
                            _toyoTokenId = sctt.TokenId,
                            _toyoChain = sctt.ChainId,
                            _toyoBlockNumber = sctt.BlockNumber
                        } equals new
                        {
                            _toyoTokenId = owners.TokenId,
                            _toyoChain = owners.ChainId,
                            _toyoBlockNumber = owners.BlockNumber
                        }
                        join sctm in _context.Set<SmartContractToyoMint>()
                            on owners.TokenId equals sctm.TokenId
                        join sctty in _context.Set<SmartContractToyoType>()
                            on sctm.TypeId equals sctty.TypeId
                        join tt in _context.Set<TypeToken>()
                            on sctty.TypeId equals tt.Id
                        join tp in _context.Set<ToyoPlayer>()
                            on sctt.TokenId equals tp.TokenId
                        join t in _context.Set<Toyo>()
                            on tp.ToyoId equals t.Id
                        where sctt.WalletAddress == walletAddress && sctt.ChainId == _chainId && tt.Type == "toyo"
                        select new ToyoViewModel(sctt.TokenId, t.Name, t.Thumb, t.Video, tp.ChangeValue, t.Region);


            /* var query = from sctt in _context.Set<SmartContractToyoTransfer>()
                        join sctm in _context.Set<SmartContractToyoMint>()
                            on sctt.TokenId equals sctm.TokenId
                        join sctty in _context.Set<SmartContractToyoType>()
                            on sctm.TypeId equals sctty.TypeId        
                        join tt in _context.Set<TypeToken>()
                            on sctty.TypeId equals tt.TypeId
                        where sctt.WalletAddress == walletAddress && sctt.ChainId == chainId && tt.Type == "toyo"
                        select new BoxesViewModel(sctt.TokenId, sctm.TypeId, sctty.Name); */

            return await query.ToListAsync();
        }

        /* [HttpPost("postToyo")]
        public Task<ActionResult<bool>> postToyo(string walletAddress, string chainId) {

            return false;
        } */

        public override async Task<ActionResult<SortViewModel>> sortBox(
            int TokenId,
            string walletAddress
        )
        {
            // get the type of token by token id and throws not found if not find
            int? typeId = getTypeIdByTokenId(TokenId);
            if (typeId == null) return NotFound(new ResponseStatusEntity(404, "TokenId Not Found"));

            // get the box type by type id and throws bad reques if token type is not a closed box type
            BoxType box = _context.Set<BoxType>().AsNoTracking().SingleOrDefault(s => s.TypeId == typeId);
            if (box == null) return BadRequest(new ResponseStatusEntity(400, "TokenId is not a box"));

            //sorts the rarity of toyo
            var toyoRaffle = raffle.main(box.IsFortified, box.IsJakana);
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

            ToyoJson toyoJson = new ToyoJson()
            {
                name = toyoReturn[0].Name,
                description = toyoReturn[0].Desc,
                image = toyoReturn[0].Thumb,
                animation_url = toyoReturn[0].Video,
                attributes = attributes.ToArray()
            };

            List<SwapToyo> swapReturn = new List<SwapToyo>();

            swapReturn = await SwapFunction(TokenId, walletAddress);
            if (!swapReturn.Any()) return NotFound(new ResponseStatusEntity(404, "Smart Contract Not Found"));

            ToyoPlayer _toyoPlayer = new ToyoPlayer
            {
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
                ChainId = _chainId,
                ChangeValue = false
            };

            string json = JsonSerializer.Serialize(toyoJson);
            json = json.Replace("\"display_type\":\"string\",", "");
            for (int i = 0; i < 10; i++)
            {
                json = json.Replace($"\"{i}", $"{i}");
                json = json.Replace($"{i}\"", $"{i}");
            }
            json = json.Replace("mp4", "mp4\"");

            var jsonfolder = new DirectoryInfo(_jsonFolderPath);
            if (!jsonfolder.Exists) jsonfolder.Create();

            await System.IO.File.WriteAllTextAsync(Path.Combine(jsonfolder.FullName, $"{swapReturn[0].ToTokenId}.json"), json);
            //await System.IO.File.WriteAllTextAsync($"/tmp/toyoverse/1010.json", json);
            Console.WriteLine("Json Saved");
            try
            {
                await saveToyoPlayer(_toyoPlayer);

                for (int i = 1; i <= 10; i++)
                {
                    if (i != 2)
                    {
                        await savePartPlayer(toyoRaffle.toyoRaridade, i, swapReturn[0].ToTokenId, walletAddress, _chainId, toyoRaffle.qParts[i][0], toyoRaffle.qParts[i][1]);
                    }
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
            }
            toyoRaffle.toyoId = swapReturn[0].ToTokenId;

            return toyoRaffle;
        }

        private int? getTypeIdByTokenId(int tokenId)
        {
            var toyomint = _context.Set<SmartContractToyoMint>().AsNoTracking().SingleOrDefault(
                s => s.TokenId == tokenId
                && s.ChainId == _chainId);
            if (toyomint == null) return null;
            return toyomint.TypeId;
        }

        [HttpPost("postPercentageBonus")]
        public override async Task<ActionResult<ResponseStatusEntity>> postPorcentageBonus(PorcentageBonusView porcentageBonusView)
        {
            int _bonusCode;
            if (porcentageBonusView.bonus.Length > 0)
            {
                _bonusCode = Convert.ToInt32(EncodingUtils.Base64Decode(porcentageBonusView.bonus));
            }
            else
            {
                _bonusCode = 10;
            }

            string tokenId = EncodingUtils.Base64Decode(porcentageBonusView.tokenId);
            string chainId = porcentageBonusView.wallet.Split(";")[1];
            string walletAddress = porcentageBonusView.wallet.Split(";")[0];
            float[] porcentageBonus = new float[] { 1, 1.01f, 1.02f, 1.03f, 1.04f, 1.05f, 1.08f, 1.11f, 1.14f, 1.17f, 1.2f };

            //int[] numBase = new int[] { 0, 582, 49751, 67412, 714, 65852, 4414, 8857445, 5114514, 222541, 6367 };

            ToyoPlayer _toyoPlayer = new ToyoPlayer();
            Toyo _toyo = new Toyo();

            var queryToyoPlayer = from toyoPlayer in _context.Set<ToyoPlayer>()
                                  where toyoPlayer.TokenId == Convert.ToInt32(tokenId) && toyoPlayer.WalletAddress == walletAddress && toyoPlayer.ChainId == chainId
                                  select toyoPlayer;

            _toyoPlayer = await queryToyoPlayer.FirstOrDefaultAsync();

            if (_toyoPlayer.ChangeValue == false)
            {


                //int _bonus = Array.IndexOf(numBase, _bonusCode);
                int _bonus = _bonusCode;

                if (_bonus <= 10 && _bonus > 0)
                {

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
                        new AttributesJson { trait_type = "Type", value = "9" },
                        new AttributesJson { trait_type = "Toyo", value = _toyo.Name },
                        new AttributesJson { trait_type = "Region", value = _toyo.Region },
                        new AttributesJson { trait_type = "Rarity", value = (_toyo.Rarity == 1 ? "Common Edition" : (_toyo.Rarity == 2 ? "Uncommon Edition" : (_toyo.Rarity == 3 ? "Rare Edition" : (_toyo.Rarity == 4 ? "limited Edition" : (_toyo.Rarity == 5 ? "Collectors Edition" : "Prototype Edition" ))))) },
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

                    ToyoJson toyoJson = new ToyoJson()
                    {
                        name = _toyo.Name,
                        description = _toyo.Desc,
                        image = _toyo.Thumb,
                        animation_url = _toyo.Video,
                        attributes = attributes.ToArray()
                    };
                    var jsonOptions = new JsonSerializerOptions();
                    jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    string json = JsonSerializer.Serialize(toyoJson, jsonOptions);
                    for (int i = 0; i < 10; i++)
                    {
                        json = json.Replace($"\"{i}", $"{i}");
                        json = json.Replace($"{i}\"", $"{i}");
                    }
                    json = json.Replace("mp4", "mp4\"");

                    var jsonfolder = new DirectoryInfo(_jsonFolderPath);
                    if (!jsonfolder.Exists) jsonfolder.Create();
                    var targetFile = $"{jsonfolder.FullName}{Path.DirectorySeparatorChar}{tokenId}.json";

                    await System.IO.File.WriteAllTextAsync(targetFile, json);
                    await _context.SaveChangesAsync();
                }
            }

            return Ok(new ResponseStatusEntity(200, "updated toyo"));
        }

        [HttpGet("minigame")]
        public async Task<ActionResult<SortViewModel>> minigame(int TokenId, string walletAddress, string chainId)
        {
            var queryToyoPlayer = from toyoPlayer in _context.Set<ToyoPlayer>()
                                  where toyoPlayer.TokenId == TokenId && toyoPlayer.WalletAddress == walletAddress && toyoPlayer.ChainId == chainId
                                  select toyoPlayer;

            var toyoPlayerReturn = await queryToyoPlayer.ToListAsync();

            var queryPartsPlayer = from toyoParts in _context.Set<PartPlayer>()
                                   where toyoParts.TokenId == TokenId && toyoParts.WalletAddress == walletAddress && toyoParts.ChainId == chainId
                                   orderby toyoParts.Id
                                   select toyoParts;

            var toyoPartsReturn = await queryPartsPlayer.ToListAsync();

            var queryToyo = from toyo in _context.Set<Toyo>()
                            where toyo.Id == toyoPlayerReturn[0].ToyoId
                            select toyo;

            var toyoReturn = await queryToyo.ToListAsync();

            SortViewModel toyoRaffle = new SortViewModel();

            List<AttributesJson> attributes = new List<AttributesJson> {
                new AttributesJson { display_type = "string", trait_type = "Type", value = "9" },
                new AttributesJson { display_type = "string", trait_type = "Toyo", value = toyoReturn[0].Name },
                new AttributesJson { display_type = "string", trait_type = "Region", value = toyoReturn[0].Region },
                new AttributesJson { display_type = "string", trait_type = "Rarity", value = (toyoReturn[0].Rarity == 1 ? "Common Edition" : (toyoReturn[0].Rarity == 2 ? "Uncommon Edition" : (toyoReturn[0].Rarity == 3 ? "Rare Edition" : (toyoReturn[0].Rarity == 4 ? "limited Edition" : (toyoReturn[0].Rarity == 5 ? "Collectors Edition" : "Prototype Edition" ))))) },
                new AttributesJson { display_type = "number", trait_type = "Vitality", value = toyoPlayerReturn[0].Vitality.ToString() },
                new AttributesJson { display_type = "number", trait_type = "Strength", value =toyoPlayerReturn[0].Strength.ToString() },
                new AttributesJson { display_type = "number", trait_type = "Resistance", value =toyoPlayerReturn[0].Resistance.ToString() },
                new AttributesJson { display_type = "number", trait_type = "CyberForce", value =toyoPlayerReturn[0].CyberForce.ToString() },
                new AttributesJson { display_type = "number", trait_type = "Resilience", value =toyoPlayerReturn[0].Resilience.ToString() },
                new AttributesJson { display_type = "number", trait_type = "Precision", value =toyoPlayerReturn[0].Precision.ToString() },
                new AttributesJson { display_type = "number", trait_type = "Technique", value =toyoPlayerReturn[0].Technique.ToString() },
                new AttributesJson { display_type = "number", trait_type = "Analysis", value =toyoPlayerReturn[0].Analysis.ToString() },
                new AttributesJson { display_type = "number", trait_type = "Speed", value =toyoPlayerReturn[0].Speed.ToString() },
                new AttributesJson { display_type = "number", trait_type = "Agility", value =toyoPlayerReturn[0].Agility.ToString() },
                new AttributesJson { display_type = "number", trait_type = "Stamina", value =toyoPlayerReturn[0].Stamina.ToString() },
                new AttributesJson { display_type = "number", trait_type = "Luck", value =toyoPlayerReturn[0].Luck.ToString() }
            };

            ToyoJson toyoJson = new ToyoJson()
            {
                name = toyoReturn[0].Name,
                description = toyoReturn[0].Desc,
                image = toyoReturn[0].Thumb,
                animation_url = toyoReturn[0].Video,
                attributes = attributes.ToArray()
            };

            ToyoPlayer _toyoPlayer = new ToyoPlayer
            {
                ToyoId = toyoPlayerReturn[0].ToyoId,
                TokenId = toyoPlayerReturn[0].TokenId,
                Vitality = toyoPlayerReturn[0].Vitality,
                Strength = toyoPlayerReturn[0].Strength,
                Resistance = toyoPlayerReturn[0].Resistance,
                CyberForce = toyoPlayerReturn[0].CyberForce,
                Resilience = toyoPlayerReturn[0].Resilience,
                Precision = toyoPlayerReturn[0].Precision,
                Technique = toyoPlayerReturn[0].Technique,
                Analysis = toyoPlayerReturn[0].Analysis,
                Speed = toyoPlayerReturn[0].Speed,
                Agility = toyoPlayerReturn[0].Agility,
                Stamina = toyoPlayerReturn[0].Stamina,
                Luck = toyoPlayerReturn[0].Luck,
                WalletAddress = walletAddress,
                ChainId = chainId,
                ChangeValue = true
            };

            string json = JsonSerializer.Serialize(toyoJson);
            json = json.Replace("\"display_type\":\"string\",", "");
            for (int i = 0; i < 10; i++)
            {
                json = json.Replace($"\"{i}", $"{i}");
                json = json.Replace($"{i}\"", $"{i}");
            }
            json = json.Replace("mp4", "mp4\"");

            var jsonfolder = new DirectoryInfo(_jsonFolderPath);
            if (!jsonfolder.Exists) jsonfolder.Create();
            var targetFile = $"{jsonfolder.FullName}{Path.DirectorySeparatorChar}{toyoPlayerReturn[0].TokenId}.json";

            await System.IO.File.WriteAllTextAsync(targetFile, json);

            toyoRaffle.qParts = new int[][] {
                 new int[] { 0, 0 },
                 new int[] { toyoPartsReturn[0].StatId, toyoPartsReturn[0].BonusStat },
                 new int[] { 0, 0 },
                 new int[] { toyoPartsReturn[1].StatId, toyoPartsReturn[1].BonusStat },
                 new int[] { toyoPartsReturn[2].StatId, toyoPartsReturn[2].BonusStat },
                 new int[] { toyoPartsReturn[3].StatId, toyoPartsReturn[3].BonusStat },
                 new int[] { toyoPartsReturn[4].StatId, toyoPartsReturn[4].BonusStat },
                 new int[] { toyoPartsReturn[5].StatId, toyoPartsReturn[5].BonusStat },
                 new int[] { toyoPartsReturn[6].StatId, toyoPartsReturn[6].BonusStat },
                 new int[] { toyoPartsReturn[7].StatId, toyoPartsReturn[7].BonusStat },
                 new int[] { toyoPartsReturn[8].StatId, toyoPartsReturn[8].BonusStat },
             };

            try
            {
                await saveToyoPlayer(_toyoPlayer);

                for (int i = 1; i <= 10; i++)
                {
                    if (i != 2)
                    {
                        await savePartPlayer(toyoPlayerReturn[0].ToyoId, i, toyoPlayerReturn[0].TokenId, walletAddress, chainId, toyoRaffle.qParts[i][0], toyoRaffle.qParts[i][1]);
                    }
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
            }

            toyoRaffle.qStats = new int[] {
                0,
                toyoPlayerReturn[0].Vitality,
                toyoPlayerReturn[0].Strength,
                toyoPlayerReturn[0].Resistance,
                toyoPlayerReturn[0].CyberForce,
                toyoPlayerReturn[0].Resilience,
                toyoPlayerReturn[0].Precision,
                toyoPlayerReturn[0].Technique,
                toyoPlayerReturn[0].Analysis,
                toyoPlayerReturn[0].Speed,
                toyoPlayerReturn[0].Agility,
                toyoPlayerReturn[0].Stamina,
                toyoPlayerReturn[0].Luck
            };

            toyoRaffle.toyoRaridade = toyoReturn[0].Id;
            toyoRaffle.raridade = toyoReturn[0].Rarity;
            toyoRaffle.toyoId = toyoPlayerReturn[0].TokenId;

            return toyoRaffle;
        }

        private async Task<List<SwapToyo>> SwapFunction(int TokenId, string walletAddress)
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var result = new List<SwapToyo>();
            do
            {
                var query = from scts in _context.Set<SmartContractToyoSwap>()
                            join sctt in _context.Set<SmartContractToyoTransfer>()
                                on new
                                {
                                    _toyoTransaction = scts.TransactionHash,
                                    _toyoChain = scts.ChainId,
                                    _toyoTokenId = scts.ToTokenId
                                } equals new
                                {
                                    _toyoTransaction = sctt.TransactionHash,
                                    _toyoChain = sctt.ChainId,
                                    _toyoTokenId = sctt.TokenId
                                }
                            join sctty in _context.Set<SmartContractToyoType>()
                                on scts.ToTypeId equals sctty.TypeId
                            join tt in _context.Set<TypeToken>()
                                on sctty.TypeId equals tt.Id
                            where scts.FromTokenId == TokenId && sctt.WalletAddress == walletAddress && sctt.ChainId == _chainId && tt.Type == "toyo"
                            select new SwapToyo { TransactionHash = scts.TransactionHash, ChainId = scts.ChainId, ToTokenId = scts.ToTokenId, TypeToken = tt.Id, Name = sctty.Name };
                result = await query.ToListAsync();
            } while (continueSwapFunction(result, stopWatch));

            return result;
        }

        private bool continueSwapFunction(List<SwapToyo> result, Stopwatch stopWatch)
        {
            if (result.Any()) return false;
            if (stopWatch.ElapsedMilliseconds > _timeoutSwapFunction) return false;
            Thread.Sleep(_intervalSwapFunctionQuery);
            return true;
        }

        private async Task<bool> saveToyoPlayer(ToyoPlayer _toyoPlayer)
        {
            var toyoPlayer = await _context.ToyosPlayer.FirstOrDefaultAsync(p => p.WalletAddress == _toyoPlayer.WalletAddress && p.ChainId == _toyoPlayer.ChainId && p.TokenId == _toyoPlayer.TokenId && p.ToyoId == _toyoPlayer.ToyoId);

            if (toyoPlayer == null)
            {
                _context.ToyosPlayer.Add(_toyoPlayer);
            }
            else
            {
                toyoPlayer = _toyoPlayer;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> savePartPlayer(int toyoId, int part, int tokenId, string walletAddress, string chainId, int statId, int bonusStat)
        {
            var partFind = await _context.Parts.FirstOrDefaultAsync(p => p.TorsoId == toyoId && p.Part == part);
            var partPlayer = await _context.PartsPlayer.FirstOrDefaultAsync(p => p.WalletAddress == walletAddress && p.ChainId == chainId && p.TokenId == tokenId && p.PartId == partFind.Id);

            var newPart = new PartPlayer
            {
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
            }
            else
            {
                partPlayer = newPart;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        [HttpGet("onlyRaffle")]
        public async Task<ActionResult<SortViewModel>> onlyRaffle(int TypeId, int TokenId, string walletAddress, string chainId, int newTokenId, bool Fortified = false, bool Jakana = false)
        {
            Console.WriteLine("TokenId - Sorteio: {0}", TokenId);
            Console.WriteLine("WalletAddress - Sorteio: {0}", walletAddress);

            SortViewModel toyoRaffle = new SortViewModel();
            toyoRaffle = raffle.main(Fortified, Jakana);

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

            ToyoJson toyoJson = new ToyoJson()
            {
                name = toyoReturn[0].Name,
                description = toyoReturn[0].Desc,
                image = toyoReturn[0].Thumb,
                animation_url = toyoReturn[0].Video,
                attributes = attributes.ToArray()
            };

            ToyoPlayer _toyoPlayer = new ToyoPlayer
            {
                ToyoId = toyoRaffle.toyoRaridade,
                TokenId = newTokenId,
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

            string json = JsonSerializer.Serialize(toyoJson);
            json = json.Replace("\"display_type\":\"string\",", "");
            for (int i = 0; i < 10; i++)
            {
                json = json.Replace($"\"{i}", $"{i}");
                json = json.Replace($"{i}\"", $"{i}");
            }
            json = json.Replace("mp4", "mp4\"");

            var jsonfolder = new DirectoryInfo(_jsonFolderPath);
            if (!jsonfolder.Exists) jsonfolder.Create();
            var targetFile = $"{jsonfolder.FullName}{Path.DirectorySeparatorChar}{newTokenId}.json";

            await System.IO.File.WriteAllTextAsync(targetFile, json);
            Console.WriteLine("Json Saved");
            try
            {
                await saveToyoPlayer(_toyoPlayer);

                for (int i = 1; i <= 10; i++)
                {
                    if (i != 2)
                    {
                        await savePartPlayer(toyoRaffle.toyoRaridade, i, newTokenId, walletAddress, chainId, toyoRaffle.qParts[i][0], toyoRaffle.qParts[i][1]);
                    }
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
            }

            toyoRaffle.toyoId = newTokenId;

            return toyoRaffle;
        }
    }
}