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

	    public void DeleteLot(int id)
	    {
	        var allRatesOnLotForDelete = _auctionContext.UsersLotsRates.Where(l => l.LotId == id);
	        foreach (var rate in allRatesOnLotForDelete)
	        {
	            _auctionContext.UsersLotsRates.Remove(rate);
	        }

            var lotForDeleting = _auctionContext.Lots.FirstOrDefault(l => l.Id == id);
	        _auctionContext.Lots.Remove(lotForDeleting);
            _auctionContext.SaveChanges();
	    }

	    public IEnumerable<Lot> GetAllLots()
	    {
	        var lots = _auctionContext.Lots;
            return lots.ToList();
	    }

	    public IEnumerable<Lot> GetAllLotsOfUser(int sellerId)
	    {
	        return GetAllLots().Where(l => l.UserId == sellerId);
	    }

        public Lot GetLotById(int id)
        {
            return _auctionContext.Lots.FirstOrDefault(l => l.Id == id);
        }

	    public void MakeRate(int lotId, int userId, decimal newPrice)
	    {
	        if (_auctionContext.UsersLotsRates.Any(r => r.UserId == userId && r.LotId == lotId) == false)
	        {
                var ulr = new UsersLotsRate()
                {
                    LotId = lotId,
                    UserId = userId
                };

                _auctionContext.UsersLotsRates.Add(ulr);
            }

	        var lot = _auctionContext.Lots.FirstOrDefault(l => l.Id == lotId);

	        lot.CurrentBuyerId = userId;
	        lot.CurrentPrice = newPrice;
	        lot.RatesCount++;
	        _auctionContext.SaveChanges();
	    }

	    public void UpdateDescriptionDateStepOfLot(Lot lot)
	    {
	        var lotForUpdate = _auctionContext.Lots.FirstOrDefault(l => l.Id == lot.Id);

            lotForUpdate.Description = lot.Description;
	        lotForUpdate.DateOfAuction = lot.DateOfAuction;
	        lotForUpdate.MinimalStepRate = lot.MinimalStepRate;
	        _auctionContext.SaveChanges();
	    }
    }
}
