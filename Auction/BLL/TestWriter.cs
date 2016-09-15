//using BLL.Interface;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DAL.Interface.Entities;
//using DAL.Interface.Repositories;

//namespace BLL
//{
//    public class TestWriter : ITestWriter
//    {
//        private readonly IRepositoryFactory _repositoryFactory;

//        public TestWriter(IRepositoryFactory repositoryFactory)
//        {
//            _repositoryFactory = repositoryFactory;
//        }

//        public bool GetSomeMessage(string mess)
//        {
//            var gettingMess = mess;
//            _repositoryFactory.LotRepository.CreateLot(new Lot {Description = mess ,DateOfAuction = DateTime.Now});
//            return true;
//        }
//    }
//}