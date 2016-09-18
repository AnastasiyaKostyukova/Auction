using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace BLL.Interface.Models
{
    public class BLLLot
    {
        public BLLLot()
        {
            UsersLotsRates = new List<UsersLotsRate>();
        }

        public int Id { get; set; }
        public int UserOwnerId { get; set; }
        public string ArtworkName { get; set; }
        public string Photos { get; set; }
        public string Author { get; set; }
        public string ArtworkFormat { get; set; }
        public int YearOfCreation { get; set; }
        public string Description { get; set; }
        public DateTime DateOfAuction { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal MinimalStepRate { get; set; }
        public decimal CurrentPrice { get; set; }
        public int CurrentBuyerId { get; set; }
        public int RatesCount { get; set; }

        public bool LotIsFinishedAuction => DateOfAuction < DateTime.Now;

        public List<UsersLotsRate> UsersLotsRates { get; set; }
    }
}
