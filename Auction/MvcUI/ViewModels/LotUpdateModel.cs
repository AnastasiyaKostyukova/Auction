using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BLL.Interface.Models;

namespace MvcUI.ViewModels
{
    public class LotUpdateModel
    {
        public LotUpdateModel()
        {
            
        }

        public LotUpdateModel(BLLLot bllLot)
        {
            Id = bllLot.Id;
            ArtworkName = bllLot.ArtworkName;
            Author = bllLot.Author;
            Description = bllLot.Description;
            Photos = bllLot.Photos;
            StartingPrice = bllLot.StartingPrice;
            YearOfCreation = bllLot.YearOfCreation;
            MinimalStepRate = bllLot.MinimalStepRate;
            DateOfAuction = bllLot.DateOfAuction;
            ArtworkFormat = bllLot.ArtworkFormat;
            UpdatingDateOfAuction = bllLot.DateOfAuction;
        }

        public string ArtworkName { get; set; }
        public string Photos { get; set; }
        public string Author { get; set; }
        public string ArtworkFormat { get; set; }
        public int YearOfCreation { get; set; }
        public decimal StartingPrice { get; set; }

        [Required]
        [Display(Name = "New minimal step rate")]
        public decimal MinimalStepRate { get; set; }

        [Required]
        [Display(Name = "New date of the end of the auction")]
        public DateTime UpdatingDateOfAuction { get; set; }

        [Required]
        [Display(Name = "New description")]
        [MaxLength(500, ErrorMessage = "Your description should not contain more than 100 characters.")]
        public string Description { get; set; }

        public int Id { get; set; }
        public DateTime DateOfAuction { get; set; }
    }
}