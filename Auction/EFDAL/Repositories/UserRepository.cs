using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;
using DAL.Interface.Repositories;
using EFDAL.Data;

namespace EFDAL.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private AuctionContext _auctionContext;

        public UserRepository(AuctionContext _auctionContext)
        {
            this._auctionContext = _auctionContext;
        }

        public bool CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUser(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
