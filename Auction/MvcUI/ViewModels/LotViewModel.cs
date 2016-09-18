using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BLL.Interface.Models;

namespace MvcUI.ViewModels
{
    public class LotViewModel : BLLLot
    {
        public LotViewModel()
        {
        }

        public LotViewModel(BLLLot lot)
        {
            Id = lot.Id;
            Photos = lot.Photos;
            Author = lot.Author;
            UserOwnerId = lot.UserOwnerId;
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
        }

        public int CurrentUserId { get; set; }
        public bool CanRate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanSeeUser { get; set; }
        public bool CanUpdate { get; set; }

        [Required]
        [Display(Name = "Your price of rate")]
        public decimal PriceRate { get; set; }
    }
}