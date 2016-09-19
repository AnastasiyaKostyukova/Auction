namespace MvcUI.ViewModels
{
    public class LotsRequestModel
    {
        public string Tab { get; set; }
        public int PageNumber { get; set; }
        public int LotsCountOnPage { get; set; }

        public string PictureAuthor { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string ArtworkName { get; set; }
        public bool OrderByAuctionDate { get; set; }
    }
}