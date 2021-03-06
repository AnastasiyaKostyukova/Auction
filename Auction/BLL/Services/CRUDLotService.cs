﻿using System.Collections.Generic;
using System.Linq;
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
            _repositoryFactory.LotRepository.CreateLot(lot.ToLot());
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
            var allLotsOfUser = _repositoryFactory.LotRepository.GetAllLotsOfUser(sellerId);
            return allLotsOfUser.Select(l => l.ToBllLot());
        }

        public IEnumerable<BLLLot> GetAllLotsOfUser(string email)
        {
            var user = _repositoryFactory.UserRepository.GetUserByEmail(email);

            if (user == null)
            {
                return new List<BLLLot>();
            }

            return GetAllLotsOfUser(user.Id);
        }

        public void UpdateLot(BLLLot lot)
        {
            _repositoryFactory.LotRepository.UpdateDescriptionDateStepOfLot(lot.ToLot());
        }

        public void DeleteLot(int id)
        {
            _repositoryFactory.LotRepository.DeleteLot(id);
        }
    }
}
