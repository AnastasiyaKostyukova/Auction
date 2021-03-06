﻿using BLL.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Models;
using DAL.Interface.Entities;
using MvcUI.ViewModels;

namespace MvcUI.Services
{
    public class LotManagerService
    {
        private const string TabNameMyRates = "my_rates_lots";
        public const string TabNameMyLots = "my_lots";
        private const string TabNameMyWinsLots = "my_wins_lots";

        private readonly ICRUDLotService _crudLotService;
        private readonly ICRUDUserService _crudUserService;

        public LotManagerService(ICRUDLotService crudLotService, ICRUDUserService crudUserService)
        {
            _crudLotService = crudLotService;
            _crudUserService = crudUserService;
        }

        public List<BLLLot> GetLotsByTabName(string tabName, string currentUserEmail)
        {
            List<BLLLot> lots;
            if (tabName == TabNameMyLots)
            {
                lots = _crudLotService.GetAllLotsOfUser(currentUserEmail).ToList();
            }
            else if (tabName == TabNameMyRates)
            {
                var userId = _crudUserService.GetUserByEmail(currentUserEmail).Id;
                lots = _crudLotService.GetAllLots()
                    .Where(l => l.UsersLotsRates.Any(ulr => ulr.UserId == userId) 
                    && l.LotIsFinishedAuction == false).ToList();
            }
            else if(tabName == TabNameMyWinsLots)
            {
                var userId = _crudUserService.GetUserByEmail(currentUserEmail).Id;
                lots = _crudLotService.GetAllLots().Where(l => l.CurrentBuyerId == userId 
                && l.LotIsFinishedAuction).ToList();
            }
            else
            {
                lots = _crudLotService.GetAllLots().Where(l => l.LotIsFinishedAuction == false).ToList();
            }

            return lots;
        }

        public LotsViewModel BuildPagingModel(List<BLLLot> lots, LotsRequestModel lotsRequest, string currentUserEmail)
        {
            if (lotsRequest.LotsCountOnPage == 0)
            {
                lotsRequest.LotsCountOnPage = 5;
            }

            var maxPageNumber = 1;
            if (lots.Count % lotsRequest.LotsCountOnPage != 0)
            {
                maxPageNumber = lots.Count / lotsRequest.LotsCountOnPage + 1;
            }
            else
            {
                maxPageNumber = lots.Count / lotsRequest.LotsCountOnPage;
            }

            var lotsAfterSkip = lots.Skip(lotsRequest.LotsCountOnPage * 
                (lotsRequest.PageNumber - 1)).Take(lotsRequest.LotsCountOnPage);

            var userId = _crudUserService.GetUserByEmail(currentUserEmail).Id;

            var model = new LotsViewModel
            {
                Lots = lotsAfterSkip.ToList(),
                PageNumber = lotsRequest.PageNumber,
                MaxPageNumber = maxPageNumber,
                Tab = lotsRequest.Tab,
                CurrentUserId = userId
            };

            return model;
        }
    }
}