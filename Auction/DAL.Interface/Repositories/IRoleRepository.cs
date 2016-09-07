using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Repositories
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAllRoles();
        bool CreateNewRole(Role role);
        Role GetById(int? roleId);
        Role GetByRoleName(string roleName);
    }
}
