namespace BLL.Interface.Services
{
    public interface ILotService
    {
        void MakeRate(int lotId, int userId, decimal newPrice);
    }
}
