using DAL.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;
using EFDAL.Data;

namespace EFDAL.Repositories
{
	public class LotRepository: ILotRepository
	{
        private AuctionContext _auctionContext;

        internal LotRepository(AuctionContext auctionContext)
        {
            _auctionContext = auctionContext;
        }

	    public void CreateLot(Lot lot)
	    {
	        _auctionContext.Lots.Add(lot);
	        _auctionContext.SaveChanges();
	    }

	    public void DeleteLot(Lot lot)
	    {
	        _auctionContext.Lots.Remove(lot);
	        _auctionContext.SaveChanges();
	    }

	    public IEnumerable<Lot> GetAllLots()
	    {
	        return _auctionContext.Lots.ToList();
	    }

	    public IEnumerable<Lot> GetAllLotsOfUser(int sellerId)
	    {
	        return GetAllLots().Where(l => l.SellerId == sellerId);
	    }

        public Lot GetLotById(int id)
        {
            return _auctionContext.Lots.FirstOrDefault(l => l.Id == id);
        }
    }
}
