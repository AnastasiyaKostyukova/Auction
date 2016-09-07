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
        bool UpdateUser(User user);
        bool RemoveUser(int id);
    }
}
