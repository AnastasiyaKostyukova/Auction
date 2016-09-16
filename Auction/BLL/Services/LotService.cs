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
    }
}
