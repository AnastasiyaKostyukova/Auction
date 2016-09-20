namespace MvcUI.ViewModels
{
    public class LotsRequestModel
    {
        public string Tab { get; set; }
        public int PageNumber { get; set; }
        public int LotsCountOnPage { get; set; }
        public bool OrderByAuctionDate { get; set; }
        public string SearchPictureAuthor { get; set; }
        public decimal? SearchMinPrice { get; set; }
        public decimal? SearchMaxPrice { get; set; }
        public string SearchArtworkName { get; set; }

        public string SearchErrors { get; set; }
    }
}