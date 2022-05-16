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

namespace BackendToyo.Services.Implementations
{
    public class UnboxService : IUnboxService
    {
        private readonly IUnboxRepository _unboxRepository;

        public UnboxService (IUnboxRepository unboxRepository)
        {
            _unboxRepository = unboxRepository;
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
            if (toyoPlayer.Count() >= 1) throw new InvalidTokenException("Error: return in 24 hours");

            return swaps[0];
        }
         public async Task<ActionResult<SortViewModel>> SortBox(SmartContractToyoSwap swap)
        {
            
            // get the box type by type id and throws bad reques if token type is not a closed box type
            BoxType box = await _unboxRepository.typeBoxType(swap.ToTypeId);
            if (box == null) throw new InvalidTokenException("TokenId is not a box");
            throw new NotImplementedException();
        }
    }
}