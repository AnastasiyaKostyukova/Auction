﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace DAL.Interface.Repositories
{
	public interface ILotRepository
	{
        // #todo CRUD function
		bool AddLot(Lot lot);
	}
}
