using System.Collections.Generic;
using System.Data.Entity;
using DAL.Interface.Entities;

namespace EFDAL.Data
{
    internal class AuctionContextInitializer
        : DropCreateDatabaseIfModelChanges<AuctionContext>
    {
        protected override void Seed(AuctionContext context)
        {
            context.Roles.Add(new Role {Name = "admin"});
            context.Roles.Add(new Role { Name = "user" });
            context.Roles.Add(new Role { Name = "banned" });

            context.SaveChanges();
        }
    }
}
