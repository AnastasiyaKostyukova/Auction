using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Models;

namespace MvcUI.ViewModels
{
    public class LotViewModel : BLLLot
    {
        public LotViewModel(BLLLot lot)
        {
            Id = lot.Id;
            Photos = lot.Photos;
            Author = lot.Author;
            UsersLotsOwnerId = lot.UsersLotsOwnerId;
            ArtworkFormat = lot.ArtworkFormat;
            CurrentBuyerId = lot.CurrentBuyerId;
            CurrentPrice = lot.CurrentPrice;
            DateOfAuction = lot.DateOfAuction;
            Description = lot.Description;
            MinimalStepRate = lot.MinimalStepRate;
            RatesCount = lot.RatesCount;
            StartingPrice = lot.StartingPrice;
            YearOfCreation = lot.YearOfCreation;
            ArtworkName = lot.ArtworkName;
            UsersLotsRates = lot.UsersLotsRates;
            UsersLotsOwner = lot.UsersLotsOwner;
        }

        public int CurrentUserId { get; set; }
    }
}