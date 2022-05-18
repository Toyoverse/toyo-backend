using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendToyo.Enums;
using BackendToyo.Models;
using BackendToyo.Models.DataEntities;
using Microsoft.AspNetCore.Mvc;

namespace BackendToyo.Services
{
    public interface IUnboxService
    {
        public Task<SmartContractToyoSwap> verifyCondition(int tokenId, string walletAdress);
        public Task<ActionResult<SortViewModel>> SortUnbox(SmartContractToyoSwap swap);
    }
}