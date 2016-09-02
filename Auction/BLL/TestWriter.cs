using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;
using DAL.Interface.Repositories;

namespace BLL
{
	public class TestWriter: ITestWriter
	{
		private readonly ILotRepository _lotRepository;

		public TestWriter(ILotRepository lotRepository)
		{
			_lotRepository = lotRepository;
		}

		public bool GetSomeMessage(string mess)
		{
			var gettingMess = mess;
			_lotRepository.AddLot(new Lot {Description = mess});
			return true;
		}
	}
}
