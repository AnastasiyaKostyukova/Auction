using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Models;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repositories;

namespace BLL.Services
{
    public class CRUDLotService : ICRUDLotService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public CRUDLotService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public void CreateLot(BLLLot lot)
        {
            var creatingLot = lot.ToLot();
            _repositoryFactory.LotRepository.CreateLot(creatingLot);
        }

        public void DeleteLot(BLLLot lot)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BLLLot> GetAllLots()
        {
            var allLots = _repositoryFactory.LotRepository.GetAllLots();
            return allLots.Select(l => l.ToBllLot());
        }

        public BLLLot GetLotById(int id)
        {
            return _repositoryFactory.LotRepository.GetLotById(id).ToBllLot();
        }

        public IEnumerable<BLLLot> GetAllLotsOfUser(int sellerId)
        {
            throw new NotImplementedException();
        }
    }
}
