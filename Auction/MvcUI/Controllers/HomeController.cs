using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using MvcUI.ViewModels;

namespace MvcUI.Controllers
{
	public class HomeController : Controller
	{
        private readonly ICRUDLotService _crudLotService;
        private readonly ICRUDUserService _crudUserService;

        public HomeController(
            ICRUDLotService crudLotService,
            ICRUDUserService crudUserService)
        {
            _crudLotService = crudLotService;
            _crudUserService = crudUserService;
        }

        public ActionResult About()
        {
            var aboutModel = new AboutModel()
            {
                CountUsers = _crudUserService.GetAllUsers().Count(),
                CountSoldLots = _crudLotService.GetAllLots().Count(l => l.LotIsFinishedAuction && l.CurrentBuyerId != 0),
                CountActiveLots = _crudLotService.GetAllLots().Count(l => l.LotIsFinishedAuction == false)
            };

            if (User.Identity.IsAuthenticated && User.IsInRole("banned"))
            {
                ViewBag.Message = "You was banned";
            }

			return View(aboutModel);
		}

	    public ActionResult BannedUserInfo()
	    {
	        var user = _crudUserService.GetUserByEmail(User.Identity.Name);
            var ratedLots =  _crudLotService.GetAllLots()
                    .Where(l => l.UsersLotsRates.Any(ulr => ulr.UserId == user.Id)).ToList();

            var viewModel = new LotsViewModel
            {
                Lots = ratedLots,
                CurrentUserId = user.Id
            };

            return View(viewModel);
	    }
    }
}