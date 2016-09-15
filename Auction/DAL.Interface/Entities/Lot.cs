using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Entities
{
	public class Lot
	{
        public int Id { get; set; }
	    public int? UsersLotsOwnerId { get; set; }
        public string ArtworkName { get; set; }
	    public string Photos { get; set; }
        public string Author { get; set; }
        public string ArtworkFormat { get; set; }
        public uint YearOfCreation { get; set; }
        public string Description { get; set; }
        public DateTime DateOfAuction { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal MinimalStepRate { get; set; }
        public decimal CurrentPrice { get; set; }
        public int CurrentBuyerId { get; set; }
        public uint RatesCount { get; set; }

        public virtual UsersLotsOwner UsersLotsOwner { get; set; } 
        public virtual ICollection<UsersLotsRate> UsersLotsRates { get; set; }
    }
}
