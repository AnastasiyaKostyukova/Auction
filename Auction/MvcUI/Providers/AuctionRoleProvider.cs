using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using DAL.Interface.Repositories;
using System.Web.Mvc;

namespace MvcUI.Providers
{
    public class AuctionRoleProvider: RoleProvider
    {
        public IRepositoryFactory RepositoryFactory
            => (IRepositoryFactory)DependencyResolver.Current.GetService(typeof(IRepositoryFactory));

        public override bool IsUserInRole(string userEmail, string roleName)
        {
            var user = RepositoryFactory.UserRepository.GetUserByEmail(userEmail);
            if (user != null && user.Role.Name == roleName)
            {
                return true;
            }

            return false;
        }

        public override string[] GetRolesForUser(string userEmail)
        {
            var user = RepositoryFactory.UserRepository.GetUserByEmail(userEmail);
            if (user != null)
            {

                return new string[] { user.Role.Name };
            }
            return new string[0];
        }

        public override bool RoleExists(string roleName)
        {
            return RepositoryFactory.RoleRepository.GetByRoleName(roleName) != null;
        }

        //возможно этот не нужен метод
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}