using MvcUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUI.Providers;
using System.Web.Security;

namespace MvcUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignUp(SignUpModel signUp)
        {
            if (ModelState.IsValid)
            {
                var isCreated = ((AuctionMembershipProvider) Membership.Provider)
                    .CreateUser(signUp.UserName, signUp.Password, signUp.Email);

                if (isCreated == false)
                {
                    ModelState.AddModelError("", "User with that email already exists.");
                    return View();
                }

                FormsAuthentication.SetAuthCookie(signUp.Email, false);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult SignIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIn(SignInModel signIn, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var isValidated = Membership.Provider.ValidateUser(signIn.Email, signIn.Password);

                if (isValidated == false)
                {
                    ModelState.AddModelError("", "Email or password is incorrect.");
                    return View();
                }

                FormsAuthentication.SetAuthCookie(signIn.Email, false);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult SignOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn", "Account");
        }

        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }
    }
}