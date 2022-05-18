using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackendToyo.Data;
using BackendToyo.Models;
using BackendToyo.Models.DataEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BackendToyo.Repositories.Implementations
{
    public class UnboxRepository : IUnboxRepository
    {
        private readonly AppDbContext _context;
         private readonly int _timeoutSwapFunction;
        private readonly int _intervalSwapFunctionQuery;
        private readonly string _chainId;

        public UnboxRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _intervalSwapFunctionQuery = int.Parse(configuration["Swap_Interval_Milliseconds"]);
            _chainId = configuration["Chain_Id"];
            _timeoutSwapFunction = int.Parse(configuration["Timeout_Swap_Milliseconds"]);
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
            if (type != 0) return await _context.BoxTypes.AsNoTracking().FirstOrDefaultAsync(s => s.TypeId == type);
            return null;
        }
         public async Task<List<Toyo>> returnToyo(int toyoRaridade)
        {
            var queryToyo = from toyo in _context.Toyos
                            where toyo.Id == toyoRaridade
                            select toyo;
            return await queryToyo.ToListAsync();
        }

        public async Task<List<SwapToyo>> SwapFunction(int tokenId, string walletAddress)
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
                            where scts.FromTokenId == tokenId && sctt.WalletAddress == walletAddress && sctt.ChainId == _chainId && tt.Type == "toyo"
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

        public async Task<bool> saveToyoPlayer(ToyoPlayer _toyoPlayer)
        {
            var toyoPlayer = await _context.ToyosPlayer.FirstOrDefaultAsync(
                p => p.WalletAddress == _toyoPlayer.WalletAddress 
                && p.ChainId == _toyoPlayer.ChainId 
                && p.TokenId == _toyoPlayer.TokenId 
                && p.ToyoId == _toyoPlayer.ToyoId
            );

            if (toyoPlayer == null)
            {
                await _context.ToyosPlayer.AddAsync(_toyoPlayer);
            }
            else
            {
                toyoPlayer = _toyoPlayer;
            }

            await _context.SaveChangesAsync();

            return true;
        }

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
                await _context.PartsPlayer.AddAsync(newPart);
            }
            else
            {
                partPlayer = newPart;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> getTypeIdByTokenId(int fromTokenId)
        {
            var toyomint = await _context.SmartContractToyoMints.AsNoTracking().SingleOrDefaultAsync(
                s => s.TokenId == fromTokenId
                && s.ChainId == _chainId);
            if (toyomint == null) return 0;
            return toyomint.TypeId;
        }
    }
}