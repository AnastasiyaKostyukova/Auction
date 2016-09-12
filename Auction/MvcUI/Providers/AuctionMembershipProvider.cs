using DAL.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DAL.Interface.Entities;
using System.Web.Helpers;

namespace MvcUI.Providers
{
    public class AuctionMembershipProvider : MembershipProvider
    {
        public IRepositoryFactory RepositoryFactory
            => (IRepositoryFactory) DependencyResolver.Current.GetService(typeof (IRepositoryFactory));

        public bool CreateUser(string userName, string password, string email)
        {
            var membershipUser = GetUser(email, false);

            if (membershipUser != null)
            {
                return false;
            }

            var user = new User
            {
                UserName = userName,
                Password = Crypto.HashPassword(password),
                Email = email,
                CreationDate = DateTime.Now
            };

            var role = RepositoryFactory.RoleRepository.GetByRoleName("user");

            if (role == null)
            {
                throw new ArgumentException("Role 'user' doesn't exist");
            }

            user.RoleId = role.Id;
            RepositoryFactory.UserRepository.CreateUser(user);

            return true;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            var user = RepositoryFactory.UserRepository.GetUserByEmail(email);

            if (user == null)
            {
                return null;
            }

            var memberUser = new MembershipUser("AuctionMembershipProvider",
                user.UserName, null, user.Email,
                null, null, false, false, user.CreationDate,
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }

        public override bool ValidateUser(string email, string password)
        {
            var user = RepositoryFactory.UserRepository.GetUserByEmail(email);

            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
            {
                return true;
            }

            return false;
        }





        public override MembershipUser CreateUser(string username, string password, string email,
            string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
            string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }



        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval { get; }
        public override bool EnablePasswordReset { get; }
        public override bool RequiresQuestionAndAnswer { get; }
        public override string ApplicationName { get; set; }
        public override int MaxInvalidPasswordAttempts { get; }
        public override int PasswordAttemptWindow { get; }
        public override bool RequiresUniqueEmail { get; }
        public override MembershipPasswordFormat PasswordFormat { get; }
        public override int MinRequiredPasswordLength { get; }
        public override int MinRequiredNonAlphanumericCharacters { get; }
        public override string PasswordStrengthRegularExpression { get; }
    }
}