using BLL.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace BLL.Mappers
{
    public static class LotMapper
    {
        public static BLLLot ToBllLot(this Lot lot)
        {
            return new BLLLot
            {
                Id = lot.Id,
                Photos = lot.Photos,
                Author = lot.Author,
                UsersLotsOwnerId = lot.UsersLotsOwnerId.GetValueOrDefault(),
                ArtworkFormat = lot.ArtworkFormat,
                CurrentBuyerId = lot.CurrentBuyerId,
                CurrentPrice = lot.CurrentPrice,
                DateOfAuction = lot.DateOfAuction,
                Description = lot.Description,
                MinimalStepRate = lot.MinimalStepRate,
                RatesCount = lot.RatesCount,
                StartingPrice = lot.StartingPrice,
                YearOfCreation = lot.YearOfCreation,
                ArtworkName = lot.ArtworkName,
                UsersLotsRates = lot.UsersLotsRates.ToList(),
                UsersLotsOwner = lot.UsersLotsOwner
            };
        }

        public static Lot ToLot(this BLLLot lot)
        {
            return new Lot
            {
                Id = lot.Id,
                Photos = lot.Photos,
                Author = lot.Author,
                UsersLotsOwnerId = lot.UsersLotsOwnerId,
                ArtworkFormat = lot.ArtworkFormat,
                CurrentBuyerId = lot.CurrentBuyerId,
                CurrentPrice = lot.CurrentPrice,
                DateOfAuction = lot.DateOfAuction,
                Description = lot.Description,
                MinimalStepRate = lot.MinimalStepRate,
                RatesCount = lot.RatesCount,
                StartingPrice = lot.StartingPrice,
                YearOfCreation = lot.YearOfCreation,
                ArtworkName = lot.ArtworkName
            };
        }
    }
}
