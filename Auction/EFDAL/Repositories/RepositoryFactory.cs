using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Repositories;
using EFDAL.Data;

namespace EFDAL.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly AuctionContext _auctionContext;
        private IRoleRepository _roleRepository;
        private ILotRepository _lotRepository;
        private IUserRepository _userRepository;

        public RepositoryFactory()
        {
            _auctionContext = new AuctionContext();
        }

        public IRoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new RolesRepository(_auctionContext);
                }

                return _roleRepository;
            }
        }

        public ILotRepository LotRepository
        {
            get
            {
                if (_lotRepository == null)
                {
                    _lotRepository = new LotRepository(_auctionContext);
                }

                return _lotRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_auctionContext);
                }

                return _userRepository;
            }
        }
    }
}
