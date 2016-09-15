using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DAL.Interface.Repositories;
using BLL.Interface;

namespace MvcUI.Controllers
{
	public class HomeController : Controller
	{

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Index(string str)
		{
			return RedirectToAction("Index");
		}

		public ActionResult About()
		{
			ViewBag.Message = "ART Auction is a resource where contemporary artists can exhibit their own work.";

			return View();
		}

        [Authorize(Roles = "admin")]
        public ActionResult UsersEdit()
        {
            return View();
        }
    }
}