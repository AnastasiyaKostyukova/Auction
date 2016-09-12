using BLL.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface ICRUDUserService
    {
        void CreateUser(BLLUser user);
        IEnumerable<BLLUser> GetAllUsers();
        BLLUser GetUserByEmail(string email);
        BLLUser GetUserById(int id);
        void UpdateUser(BLLUser user);
        void RemoveUser(int id);
    }
}
