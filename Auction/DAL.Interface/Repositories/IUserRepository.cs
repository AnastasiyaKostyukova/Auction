using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        bool CreateUser(User user);
        User GetUserByEmail(string email);
        User GetUserById(int id);
        bool UpdateUser(User user);
        bool ChangeBanStatusUser(int id, bool isBan);
        IEnumerable<User> GetBannedUsers();
    }
}
