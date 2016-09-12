using BLL.Interface.Models;
using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class UserMapper
    {
        public static BLLUser ToBllUser(this User user)
        {
            return new BLLUser
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password,
                CreationDate = user.CreationDate,
                RoleId = user.RoleId
            };
        }

        public static User ToUser(this BLLUser user)
        {
            return new User
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password,
                CreationDate = user.CreationDate,
                RoleId = user.RoleId
            };
        }
    }
}
