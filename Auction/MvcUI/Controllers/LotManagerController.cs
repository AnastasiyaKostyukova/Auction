using System;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Models;
using BLL.Interface.Services;
using MvcUI.Services;
using MvcUI.ViewModels;

namespace MvcUI.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class LotManagerController : Controller
    {
        private readonly LotManagerService _lotManagerService;
        private readonly ICRUDLotService _crudLotService;
        private readonly ICRUDUserService _crudUserService;
        private readonly ILotService _lotService;

        public LotManagerController(
            ICRUDLotService crudLotService,
            ICRUDUserService crudUserService,
            ILotService lotService)
        {
            _crudLotService = crudLotService;
            _crudUserService = crudUserService;
            _lotService = lotService;
            _lotManagerService = new LotManagerService(_crudLotService, _crudUserService, _lotService);
        }

        public ActionResult Index(LotsRequestModel lotsRequest)
        {
            //for (var i = 0; i < 57; i++)
            //{
            //    var bllLot = new BLLLot
            //    {
            //        ArtworkName = "Name_" + i,
            //        DateOfAuction = new DateTime(2016, 12, 31),
            //        UserOwnerId = 1,
            //        Author = "cat in",
            //        Photos = "http://d39kbiy71leyho.cloudfront.net/wp-content/uploads/2016/05/09170020/cats-politics-TN.jpg"
            //    };

            //    _crudLotService.CreateLot(bllLot);
            //}

            if (lotsRequest.LotsCountOnPage == 0)
            {
                lotsRequest.LotsCountOnPage = 5;
            }

            if (lotsRequest.Tab == null)
            {
                lotsRequest.Tab = LotManagerService.TabAllLots;
            }

            var model = _lotManagerService.BuildLotsViewModelByRequestModel(lotsRequest, User.Identity.Name);

            return View(model);
        }

        public ActionResult Lots(LotsRequestModel lotsRequest)
        {   
            var model = _lotManagerService.BuildLotsViewModelByRequestModel(lotsRequest, User.Identity.Name);
            return PartialView("_LotsList", model);
        }

        public ActionResult Lot(int id)
        {
            var lot = _crudLotService.GetLotById(id);
            var lotView = new LotViewModel(lot);
            var emailOfCurrentUser = User.Identity.Name;
            var currentUserId = _crudUserService.GetUserByEmail(emailOfCurrentUser).Id;
            lotView.CurrentUserId = currentUserId;

            lotView.CanDelete = (currentUserId == lot.UserOwnerId
                                 && ((lot.LotIsFinishedAuction && lot.CurrentBuyerId == 0) || !lot.LotIsFinishedAuction))
                                || User.IsInRole("admin");

            lotView.CanRate = currentUserId != lot.UserOwnerId && lot.LotIsFinishedAuction == false && lot.CurrentBuyerId != currentUserId;
            lotView.CanUpdate = lot.UserOwnerId == currentUserId && lot.LotIsFinishedAuction == false;
            lotView.CanSeeUser = User.IsInRole("admin");

            lotView.PriceRate = lotView.CurrentPrice + lotView.MinimalStepRate;
            return View(lotView);
        }

        [HttpPost]
        public ActionResult Lot(LotViewModel lotView)
        {
            if (ModelState.IsValid == false)
            {
                return View("Lot", lotView);
            }

            var actualLot = _crudLotService.GetLotById(lotView.Id);
            if (actualLot != null)
            {
                lotView.CurrentPrice = actualLot.CurrentPrice;
                lotView.MinimalStepRate = actualLot.MinimalStepRate;
            }

            if (lotView.PriceRate < lotView.CurrentPrice + lotView.MinimalStepRate)
            {
                ModelState.AddModelError("", "Price your rate should be more that minimal step");
                return View("Lot", lotView);
            }

            _lotService.MakeRate(lotView.Id, lotView.CurrentUserId, lotView.PriceRate);

            return RedirectToAction("Index", "LotManager");
        }

        public ActionResult CreateLot()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateLot(LotCreateModel newLot)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            if (newLot.DateOfAuction < DateTime.Now)
            {
                ModelState.AddModelError("", "Date Of auction should be in future");
                return View();
            }

            if (newLot.YearOfCreation > DateTime.Now.Year || newLot.YearOfCreation < 0)
            {
                ModelState.AddModelError("", "Year of creation is not correct");
                return View();
            }

            var user = _crudUserService.GetUserByEmail(User.Identity.Name);

            var createdLot = _lotManagerService.BuildBllLot(newLot, user.Id);

            _crudLotService.CreateLot(createdLot);
            return RedirectToAction("Index", "LotManager");
        }

        public ActionResult UpdateLot(int id)
        {
            var bllLot = _crudLotService.GetLotById(id);
            var lot = new LotUpdateModel(bllLot);

            return View(lot);
        }

        [HttpPost]
        public ActionResult UpdateLot(LotUpdateModel updatingLot)
        {
            if (ModelState.IsValid == false)
            {
                return View(updatingLot);
            }

            if (updatingLot.UpdatingDateOfAuction < updatingLot.DateOfAuction)
            {
                ModelState.AddModelError("", "Updating date Of auction should be no earlier than already declared");
                return View(updatingLot);
            }

            var lotForUpdate = new BLLLot()
            {
                Id = updatingLot.Id,
                DateOfAuction = updatingLot.UpdatingDateOfAuction,
                Description = updatingLot.Description,
                MinimalStepRate = updatingLot.MinimalStepRate
            };

            _crudLotService.UpdateLot(lotForUpdate);

            return RedirectToAction("Lot", "LotManager", new {id = updatingLot.Id});
        }

        [HttpPost]
        public bool DeleteLot(int id)
        {
            _crudLotService.DeleteLot(id);
            return true;
        }
    }
}