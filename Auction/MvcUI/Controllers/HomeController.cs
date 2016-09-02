using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Interface.Repositories;
using BLL.Interface;

namespace MvcUI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ITestWriter _testWriter;

		public HomeController(ITestWriter testWriter)
		{
			this._testWriter = testWriter;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Index(string str)
		{
			_testWriter.GetSomeMessage(str);
			return RedirectToAction("Index");
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}