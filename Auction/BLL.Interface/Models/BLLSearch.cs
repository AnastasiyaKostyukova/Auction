using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Models
{
    public class BLLSearch
    {
        public string SearchByPictureAuthor { get; set; }
        public decimal SearchByMinPrice { get; set; }
        public decimal SearchByMaxPrice { get; set; }
        public string SearchByArtworkName { get; set; }
        public bool OrderByAuctionDate { get; set; }
    }
}
