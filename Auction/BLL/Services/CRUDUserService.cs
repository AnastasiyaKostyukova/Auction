using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Models;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repositories;

namespace BLL.Services
{
    public class CRUDUserService : ICRUDUserService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public CRUDUserService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public void CreateUser(BLLUser user)
        {
            var creatingUser = user.ToUser();
            _repositoryFactory.UserRepository.CreateUser(creatingUser);
        }

        public IEnumerable<BLLUser> GetAllUsers()
        {
            var allUsers = _repositoryFactory.UserRepository.GetAllUsers();
            return allUsers.Select(u => u.ToBllUser());
        }

        public void ChangeBanStatusUser(int id, bool isBan)
        {
            _repositoryFactory.UserRepository.ChangeBanStatusUser(id, isBan);
        }

        public IEnumerable<BLLUser> GetBannedUsers()
        {
            var banned = _repositoryFactory.UserRepository.GetBannedUsers();
            return banned.Select(r => r.ToBllUser());
        }

        public BLLUser GetUserByEmail(string email)
        {
            var userByEmail = _repositoryFactory.UserRepository.GetUserByEmail(email);
            return userByEmail.ToBllUser();
        }

        public BLLUser GetUserById(int id)
        {
            var userById = _repositoryFactory.UserRepository.GetUserById(id);
            return userById.ToBllUser();
        }

        

        public void UpdateUser(BLLUser user)
        {
            throw new NotImplementedException();
        }
    }
}
