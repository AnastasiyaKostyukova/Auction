using BLL.Interface.Models;
using System;
using System.Collections.Generic;

namespace BLL.Interface.Services
{
    public interface ICRUDLotService
    {
        void CreateLot(BLLLot lot);
        void DeleteLot(BLLLot lot);

        IEnumerable<BLLLot> GetAllLots();
        IEnumerable<BLLLot> GetAllLotsOfUser(int sellerId);
        IEnumerable<BLLLot> GetAllLotsOfUser(string email);
        BLLLot GetLotById(int id);
    }
}
