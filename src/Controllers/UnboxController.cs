using System.Threading.Tasks;
using BackendToyo.Controllers.Api;
using BackendToyo.Models;
using BackendToyo.Services;
using Microsoft.AspNetCore.Mvc;
using BackendToyo.Middleware;

namespace BackendToyo.Controllers
{
    public class UnboxController : UnboxApi
    {
        private readonly IUnboxService _unboxService;

        public UnboxController(IUnboxService unboxService)
        {
            _unboxService = unboxService;
        }

        public override async Task<ActionResult<SortViewModel>> sortUnbox(int tokenId, string walletAddress)
        {
            var swap = await _unboxService.verifyCondition(tokenId, walletAddress);
            if (swap != null) return await _unboxService.SortUnbox(swap);
            return null;
        }
    }
}