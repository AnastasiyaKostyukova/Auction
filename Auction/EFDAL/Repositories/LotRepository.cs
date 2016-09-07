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
        public bool AddLot(Lot lot)
		{
			var myLot = lot;

			return true;
		}
	}
}
