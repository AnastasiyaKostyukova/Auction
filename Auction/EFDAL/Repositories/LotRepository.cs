using DAL.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace EFDAL.Repositories
{
	public class LotRepository: ILotRepository

	{
		public bool AddLot(Lot lot)
		{
			var myLot = lot;

			return true;
		}
	}
}
