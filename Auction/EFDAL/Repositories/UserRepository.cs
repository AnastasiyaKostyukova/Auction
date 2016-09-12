﻿using System;
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

        public bool RemoveUser(int id)
        {
            var user = GetUserById(id);
            if (user == null)
            {
                return false;
            }

            _auctionContext.Users.Remove(user);
            return true;
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
