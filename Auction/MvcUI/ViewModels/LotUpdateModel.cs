using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcUI.ViewModels
{
    public class LotUpdateModel
    {
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