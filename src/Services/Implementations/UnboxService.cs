using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendToyo.Repositories;
using BackendToyo.Middleware;
using BackendToyo.Models.DataEntities;
using Microsoft.AspNetCore.Mvc;
using BackendToyo.Models;
using BackendToyo.Exceptions;
using BackendToyo.Models.ResponseEntities;
using BackendToyo.Data;
using System.Text.Json;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace BackendToyo.Services.Implementations
{
    public class UnboxService : IUnboxService
    {
        private readonly IUnboxRepository _unboxRepository;
        private readonly string _jsonFolderPath;

        public UnboxService (IUnboxRepository unboxRepository, IConfiguration configuration)
        {
            _unboxRepository = unboxRepository;
            _jsonFolderPath = configuration["Json_Folder"];
        }
        public async Task<SmartContractToyoSwap> verifyCondition(int tokenId, string walletAddress)
        {
            var swaps = await _unboxRepository.swapList(tokenId);
            if (swaps ==null || swaps.Count() != 1) throw new InvalidTokenException("Error: return in 24 hours");//return false; //error("Error: return in 24 hours");
            var transfer = await _unboxRepository.transferList(tokenId);
            if (transfer == null || transfer.Count() != 1) throw new InvalidTokenException("Error: return in 24 hours");//return false;
            var mint = await _unboxRepository.mintList(tokenId);
            if (mint == null || mint.Count() != 1) throw new InvalidTokenException("Error: return in 24 hours");//return false;
            var toyoPlayer = await _unboxRepository.toyoListByToken(tokenId, walletAddress);
            if (toyoPlayer.Count() >= 1) throw new InvalidTokenException("Unbox is not applicable");

            return swaps[0];
        }
        //em desenvolvimento
         public async Task<ActionResult<SortViewModel>> SortUnbox(SmartContractToyoSwap swap)
        {
            // get the type of token by token id and throws not found if not find
            int typeId = await _unboxRepository.getTypeIdByTokenId(swap.FromTokenId);
            if (typeId == 0) throw new NotFoundException("TokenId Not Found");
            // get the box type by type id and throws bad reques if token type is not a closed box type
            BoxType box = await _unboxRepository.typeBoxType(typeId);
            if (box == null) throw new InvalidTokenException("TokenId is not a box");

            //sorts the rarity of toyo
            SortViewModel toyoRaffle = raffle.main(box.IsFortified, box.IsJakana);

            var toyoReturn = await _unboxRepository.returnToyo(toyoRaffle.toyoRaridade);

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
            swapReturn = await _unboxRepository.SwapFunction(swap.FromTokenId, swap.Sender);
            if (!swapReturn.Any()) throw new NotFoundException ("Smart Contract Not Found");

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
                WalletAddress = swap.Sender,
                ChainId = swap.ChainId,
                ChangeValue = false
            };

            string json = JsonSerializer.Serialize(toyoJson);
            json = json.Replace("\"display_type\":\"string\",", "");
            var jsonfolder = new DirectoryInfo(_jsonFolderPath);
            if (!jsonfolder.Exists) jsonfolder.Create();
            await System.IO.File.WriteAllTextAsync(Path.Combine(jsonfolder.FullName, $"{swapReturn[0].ToTokenId}.json"), json);
            Console.WriteLine("Json Saved");

            try
            {
                await _unboxRepository.saveToyoPlayer(_toyoPlayer);

                for (int i = 1; i <= 10; i++)
                {
                    if (i != 2)
                    {
                        await _unboxRepository.savePartPlayer(toyoRaffle.toyoRaridade, i, swapReturn[0].ToTokenId, swap.Sender, swap.ChainId, toyoRaffle.qParts[i][0], toyoRaffle.qParts[i][1]);
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
    }
}