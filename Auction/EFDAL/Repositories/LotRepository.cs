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

	    public void CreateLot(Lot lot, int userId)
	    {
	        var ulo = new UsersLotsOwner()
	        {
                UserId = userId
            };

            // Save because we need a ulo ID
	        var savedUlo = _auctionContext.UsersLotsOwners.Add(ulo);
            _auctionContext.SaveChanges();

            // Save because we need lot ID
            lot.UsersLotsOwnerId = savedUlo.Id;
	        lot = _auctionContext.Lots.Add(lot);
	        _auctionContext.SaveChanges();

            // Update lot Id 
	        savedUlo.LotId = lot.Id;
            _auctionContext.SaveChanges();
        }

	    public void DeleteLot(Lot lot)
	    {
	        //var ulo = _auctionContext.UsersLotsOwners.Where(l => l.LotId == lot.Id);
	        //_auctionContext.UsersLotsOwners.Remove(ulo);

	        _auctionContext.Lots.Remove(lot);
	        _auctionContext.SaveChanges();
	    }

	    public IEnumerable<Lot> GetAllLots()
	    {
	        var lots = _auctionContext.Lots;
            return lots.ToList();
	    }

	    public IEnumerable<Lot> GetAllLotsOfUser(int sellerId)
	    {
	        return GetAllLots().Where(l => l.UsersLotsOwnerId == sellerId);
	    }

        public Lot GetLotById(int id)
        {
            return _auctionContext.Lots.FirstOrDefault(l => l.Id == id);
        }
    }
}
