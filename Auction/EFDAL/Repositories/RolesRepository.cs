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
    internal class RolesRepository : IRoleRepository
    {
        private AuctionContext _auctionContext;

        internal RolesRepository(AuctionContext auctionContext)
        {
            _auctionContext = auctionContext;
        }
        public bool CreateNewRole(Role role)
        {
            if (_auctionContext.Roles.Any(m => m.Name == role.Name))
            {
                return false;
            }

            _auctionContext.Roles.Add(role);
            _auctionContext.SaveChanges();

            return true;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _auctionContext.Roles;
        }

        public Role GetById(int? roleId)
        {
            if (!roleId.HasValue)
            {
                return null;
            }

            return _auctionContext.Roles.FirstOrDefault(r => r.Id == roleId.Value);
        }

        public Role GetByRoleName(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return null;
            }

            return _auctionContext.Roles
                .FirstOrDefault(r => r.Name.ToLower() == roleName.ToLower());
        }
    }
}
