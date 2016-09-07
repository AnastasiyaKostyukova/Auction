using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace EFDAL.Data
{
	internal class AuctionContext: DbContext
	{
	    public AuctionContext() : base("AuctionContext")
	    {
	    }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
