using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendToyo.Models;
using BackendToyo.Models.DataEntities;
using Microsoft.AspNetCore.Mvc;

namespace BackendToyo.Repositories
{
    public interface IUnboxRepository
    {
        public Task<List<SmartContractToyoSwap>> swapList(int tokenId);
        public Task<List<SmartContractToyoMint>> mintList(int tokenId);
        public Task<List<SmartContractToyoTransfer>> transferList(int tokenId);
        public Task<List<ToyoPlayer>> toyoListByToken(int tokenId, string walletAddress);
        public Task<BoxType> typeBoxType(int type);
    }
}