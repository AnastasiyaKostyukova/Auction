using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;
using DAL.Interface.Repositories;

namespace EFDAL.Repositories
{
	public class WrongLotRepo : ILotRepository
	{
		public bool AddLot(Lot lot)
		{
			var lot124 = lot;
			throw new Exception("AAAAAAAAAAAAAAA WRONG!!!");
		}
	}
}
