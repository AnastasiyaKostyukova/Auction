using BLL.Interface.Models;
using System;
using System.Collections.Generic;

namespace BLL.Interface.Services
{
    public interface ICRUDLotService
    {
        // create
        void CreateLot(BLLLot lot);
        
        // read
        IEnumerable<BLLLot> GetAllLots();
        IEnumerable<BLLLot> GetAllLotsOfUser(int sellerId);
        IEnumerable<BLLLot> GetAllLotsOfUser(string email);
        BLLLot GetLotById(int id);

        // update
        void UpdateLot(BLLLot bllLot);

        // delete
        void DeleteLot(int id);
    }
}
