using System.Collections.Generic;
using BLL.Interface.Models;

namespace BLL.Interface.Services
{
    public interface ILotService
    {
        void MakeRate(int lotId, int userId, decimal newPrice);
        IEnumerable<BLLLot> Search(BLLSearch searchModel, List<BLLLot> lots);
    }
}
