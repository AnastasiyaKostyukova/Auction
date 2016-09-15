namespace DAL.Interface.Entities
{
    public class UsersLotsRate
    {
        public int Id { get; set; }

        public int? UserId { get; set; }
        public int? LotId { get; set; }
    }
}
