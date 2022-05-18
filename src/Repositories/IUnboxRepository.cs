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
        public Task<List<Toyo>> returnToyo (int toyoRaridade);
        public Task<List<SwapToyo>> SwapFunction(int tokenId, string walletAddress);
        public Task<bool> saveToyoPlayer(ToyoPlayer _toyoPlayer);
        public Task<bool> savePartPlayer(int toyoId, int part, int tokenId, string walletAddress, string chainId, int statId, int bonusStat);
        public Task<int> getTypeIdByTokenId(int fromTokenId);
    }
}