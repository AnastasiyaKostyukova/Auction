using BLL.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using BLL.Interface.Models;
using DAL.Interface.Entities;
using MvcUI.ViewModels;

namespace MvcUI.Services
{
    public class LotManagerService
    {
        public const string TabNameMyRates = "my_rates_lots";
        public const string TabNameMyLots = "my_lots";
        public const string TabNameMyWinsLots = "my_wins_lots";
        public const string TabAllLots = "all_lots";

        private readonly ICRUDLotService _crudLotService;
        private readonly ICRUDUserService _crudUserService;
        private readonly ILotService _lotService;

        public LotManagerService(
            ICRUDLotService crudLotService,
            ICRUDUserService crudUserService, 
            ILotService lotService)
        {
            _crudLotService = crudLotService;
            _crudUserService = crudUserService;
            _lotService = lotService;
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

            var model = new LotsViewModel(lotsRequest);
            model.Lots = lotsAfterSkip.ToList();
            model.CurrentUserId = userId;
            model.MaxPageNumber = maxPageNumber;

            //var model = new LotsViewModel
            //{
            //    Lots = lotsAfterSkip.ToList(),
            //    PageNumber = lotsRequest.PageNumber,
            //    MaxPageNumber = maxPageNumber,
            //    Tab = lotsRequest.Tab,
            //    CurrentUserId = userId
            //};

            return model;
        }

        public BLLLot BuildBllLot(LotCreateModel newLot, int userId)
        {
            var bllLot = new BLLLot
            {
                ArtworkName = newLot.ArtworkName,
                Author = newLot.Author,
                Photos = newLot.Photos,
                ArtworkFormat = newLot.ArtworkFormat,
                Description = newLot.Description,
                YearOfCreation = newLot.YearOfCreation,
                StartingPrice = newLot.StartingPrice,
                MinimalStepRate = newLot.MinimalStepRate,
                DateOfAuction = newLot.DateOfAuction,
                CurrentPrice = newLot.StartingPrice,
                UserOwnerId = userId
            };
            return bllLot;
        }

        public LotsViewModel BuildLotsViewModelByRequestModel(LotsRequestModel lotsRequest, string userEmail)
        {
            if (lotsRequest.PageNumber == 0)
            {
                lotsRequest.PageNumber = 1;
            }

            var errorMessage = ValidateSearchRequestModel(lotsRequest);
            lotsRequest.SearchErrors = errorMessage;

            var lots = new List<BLLLot>();

            if (string.IsNullOrEmpty(lotsRequest.SearchErrors))
            {   
                // Get all lots for current tab
                lots = GetLotsByTabName(lotsRequest.Tab, userEmail);

                var bllSearchModel = new BLLSearch
                {
                    SearchByArtworkName = lotsRequest.SearchArtworkName,
                    SearchByPictureAuthor = lotsRequest.SearchPictureAuthor,
                    SearchByMinPrice = lotsRequest.SearchMinPrice.GetValueOrDefault(),
                    SearchByMaxPrice = lotsRequest.SearchMaxPrice.GetValueOrDefault(),
                    OrderByAuctionDate = lotsRequest.OrderByAuctionDate
                };

                // Get lots by searchModel
                lots = _lotService.Search(bllSearchModel, lots).ToList();
            }

            var model = BuildPagingModel(lots, lotsRequest, userEmail);

            return model;
        }

        public string ValidateSearchRequestModel(LotsRequestModel lotsRequest)
        {
            var errors = new StringBuilder();

            if (lotsRequest.SearchArtworkName != null && lotsRequest.SearchArtworkName.Length > 50)
            {
                errors.Append("Artwork Name should be less then 50 symbols; \n");
            }

            if (lotsRequest.SearchPictureAuthor != null && lotsRequest.SearchPictureAuthor.Length > 50)
            {
                errors.Append("Picture Author should be less then 50 symbols; \n");
            }

            if (lotsRequest.SearchMinPrice.HasValue && lotsRequest.SearchMinPrice.Value < 0)
            {
                errors.Append("Min price should be positive; \n");
            }

            if (lotsRequest.SearchMaxPrice.HasValue)
            {
                if (lotsRequest.SearchMaxPrice.Value < 0)
                {
                    errors.Append("Max price should be positive; \n");
                }
                else if (lotsRequest.SearchMinPrice.HasValue &&
                         lotsRequest.SearchMaxPrice.Value < lotsRequest.SearchMinPrice.Value)
                {
                    errors.Append("Max price should be more the Min Price; \n");
                }
            }

            return errors.ToString();
        }
    }
}