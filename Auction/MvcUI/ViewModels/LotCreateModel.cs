using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace MvcUI.ViewModels
{
    public class LotCreateModel
    {
        [Required]
        [Display(Name = "Name of Artwork")]
        public string ArtworkName { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MaxLength(100, ErrorMessage = "Your description should not contain more than 100 characters.")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Photos")]
        public string Photos { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Artwork Format")]
        public string ArtworkFormat { get; set; }

        [Required]
        [Display(Name = "Year Of Creation")]
        public int YearOfCreation { get; set; }

        [Required]
        [Display(Name = "Starting Price")]
        public decimal StartingPrice { get; set; }

        [Required]
        [Display(Name = "Minimal Step Rate")]
        public decimal MinimalStepRate { get; set; }

        [Required]
        [Display(Name = "Date of the end of the auction")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfAuction { get; set; }

        //public int Id { get; set; }
        //public int SellerId { get; set; }
        //public decimal CurrentPrice { get; set; }
        //public int CurrentBuyerId { get; set; }
        //public int RatesCount { get; set; }


    }
}