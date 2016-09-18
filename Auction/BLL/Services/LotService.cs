using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Models;
using BLL.Interface.Services;
using DAL.Interface.Repositories;

namespace BLL.Services
{
    public class LotService : ILotService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public LotService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public void MakeRate(int lotId, int userId, decimal newPrice)
        {
            _repositoryFactory.LotRepository.MakeRate(lotId, userId, newPrice);
        }

        public IEnumerable<BLLLot> Search(BLLSearch searchModel, List<BLLLot> lots)
        {
            var searchingListLots = lots;
            if (!string.IsNullOrEmpty(searchModel.SearchByPictureAuthor))
            {
                searchingListLots = lots
                    .Where(l => l.Author.Contains(searchModel.SearchByPictureAuthor, 
                    StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(searchModel.SearchByArtworkName))
            {
                searchingListLots = searchingListLots.Where(l => l.ArtworkName.
                Contains(searchModel.SearchByArtworkName, 
                StringComparison.OrdinalIgnoreCase)).ToList();
            }
            
            if (searchModel.SearchByMinPrice > 0)
            {
                searchingListLots =
                    searchingListLots.Where(l => l.CurrentPrice >= searchModel.SearchByMinPrice).ToList();
            }

            if (searchModel.SearchByMaxPrice > 0 && searchModel.SearchByMaxPrice >= searchModel.SearchByMinPrice)
            {
                searchingListLots =
                    searchingListLots.Where(l => l.CurrentPrice <= searchModel.SearchByMaxPrice).ToList();
            }

            if (searchModel.OrderByAuctionDate)
            {
                searchingListLots = searchingListLots.OrderBy(l => l.DateOfAuction).ToList();
            }

            return searchingListLots;
        }

    }
}
