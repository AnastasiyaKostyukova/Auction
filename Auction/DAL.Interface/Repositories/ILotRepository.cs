using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace DAL.Interface.Repositories
{
	public interface ILotRepository
	{
		void CreateLot(Lot lot);
	    void DeleteLot(Lot lot);

	    IEnumerable<Lot> GetAllLots();
	    IEnumerable<Lot> GetAllLotsOfUser(int sellerId);
        Lot GetLotById(int id);
	    void MakeRate(int lotId, int userId, decimal newPrice);
	}
}
