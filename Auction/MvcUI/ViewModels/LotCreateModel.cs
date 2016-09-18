using System;
using System.ComponentModel.DataAnnotations;

namespace MvcUI.ViewModels
{
    public class LotCreateModel
    {
        [Required]
        [Display(Name = "Name of Artwork")]
        [MaxLength(30, ErrorMessage = "Your name's field should not contain more than 30 characters")]
        public string ArtworkName { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MaxLength(500, ErrorMessage = "Your description should not contain more than 100 characters.")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Photos")]
        [MaxLength(300, ErrorMessage = "Path to the photo should not contain more than 100 characters.")]
        public string Photos { get; set; }

        [Required]
        [Display(Name = "Author")]
        [MaxLength(50, ErrorMessage = "Path to the photo should not contain more than 50 characters.")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Artwork Format, [mm]")]
        [RegularExpression(@"\d+[*]\d+", ErrorMessage = "Please enter correct artwork format like a [140*220]")]
        [MaxLength(50, ErrorMessage = "Path to the photo should not contain more than 50 characters.")]
        public string ArtworkFormat { get; set; }

        [Required]
        [Display(Name = "Year Of Creation")]
        public int YearOfCreation { get; set; }

        [Required]
        [Display(Name = "Starting Price, $")]
        public decimal StartingPrice { get; set; }

        [Required]
        [Display(Name = "Minimal Step of Rate, $")]
        public decimal MinimalStepRate { get; set; }

        [Required]
        [Display(Name = "Date of the end of the auction")]
        public DateTime DateOfAuction { get; set; }

        public int Id { get; set; }
        public DateTime UpdatingDateOfAuction { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}