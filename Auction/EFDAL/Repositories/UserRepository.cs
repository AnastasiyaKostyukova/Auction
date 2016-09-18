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
            _auctionContext.Users.Add(user);
            _auctionContext.SaveChanges();
            return true;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _auctionContext.Users.ToList();
        }

        public User GetUserByEmail(string email)
        {
            return _auctionContext.Users
                .FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        }

        public User GetUserById(int id)
        {
            return _auctionContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public bool ChangeBanStatusUser(int id, bool isBan)
        {
            var user = GetUserById(id);
            var roleName = isBan ? "banned" : "user";
            var role = _auctionContext.Roles.FirstOrDefault(r => r.Name == roleName);
            if (user == null || role == null)
            {
                return false;
            }

            user.RoleId = role.Id;
            _auctionContext.SaveChanges();
            return true;
        }

        public IEnumerable<User> GetBannedUsers()
        {
            var bannedRole = _auctionContext.Roles.FirstOrDefault(r => r.Name == "banned");

            if (bannedRole == null)
            {
                return new List<User>();
            }

            var users = _auctionContext.Users.Where(r => r.RoleId == bannedRole.Id);
            return users;
        }

        public bool UpdateUser(User updatedUser)
        {
            var user = GetUserById(updatedUser.Id);

            if (user == null)
            {
                return false;
            }

            user.UserName = updatedUser.UserName;
            user.Password = updatedUser.Password;
            user.Email = updatedUser.Email;
            user.CreationDate = updatedUser.CreationDate;
            user.RoleId = updatedUser.RoleId;

            return true;
        }
    }
}
