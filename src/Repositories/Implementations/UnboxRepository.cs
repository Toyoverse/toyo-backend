using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendToyo.Data;
using BackendToyo.Models;
using BackendToyo.Models.DataEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendToyo.Repositories.Implementations
{
    public class UnboxRepository : IUnboxRepository
    {
        private readonly AppDbContext _context;

        public UnboxRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<SmartContractToyoMint>> mintList(int tokenId)
        {
            return await _context.SmartContractToyoMints.AsNoTracking().Where(p=> p.TokenId == tokenId).ToListAsync();
        }

        public async Task<List<SmartContractToyoSwap>> swapList(int tokenId)
        {
            return await _context.SmartContractToyoSwaps.AsNoTracking().Where(p=> p.ToTokenId == tokenId).ToListAsync();
        }

        public async Task<List<ToyoPlayer>> toyoListByToken(int tokenId, string walletAddress)
        {
            return await _context.ToyosPlayer.AsNoTracking().Where(p=> p.WalletAddress == walletAddress && p.TokenId == tokenId).ToListAsync();
        }

        public async Task<List<SmartContractToyoTransfer>> transferList(int tokenId)
        {
            return await _context.SmartContractToyoTransfers.AsNoTracking().Where(p => p.TokenId == tokenId).ToListAsync();
        }
          public async Task<BoxType> typeBoxType(int type)
        {
            return await _context.BoxTypes.AsNoTracking().FirstOrDefaultAsync(s => s.TypeId == type);
        }
    }
}