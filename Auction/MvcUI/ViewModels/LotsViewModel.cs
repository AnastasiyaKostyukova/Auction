using System;
using System.Collections.Generic;
using BLL.Interface.Models;

namespace MvcUI.ViewModels
{
    public class LotsViewModel : LotsRequestModel
    {
        public LotsViewModel()
        {
            Lots = new List<BLLLot>();
        }

        public LotsViewModel(LotsRequestModel requestModel)
        {
            Lots = new List<BLLLot>();
            Tab = requestModel.Tab;
            PageNumber = requestModel.PageNumber;
            LotsCountOnPage = requestModel.LotsCountOnPage;
            SearchPictureAuthor = requestModel.SearchPictureAuthor;
            SearchMinPrice = requestModel.SearchMinPrice;
            SearchMaxPrice = requestModel.SearchMaxPrice;
            SearchArtworkName = requestModel.SearchArtworkName;
            OrderByAuctionDate = requestModel.OrderByAuctionDate;
            SearchErrors = requestModel.SearchErrors;
        }

        // public string Tab { get; set; }
        public List<BLLLot> Lots { get; set; }
        //public int PageNumber { get; set; }

        public int MaxPageNumber { get; set; }
        public int CurrentUserId { get; set; }
    }
}