using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Models;
using BLL.Interface.Services;
using DAL.Interface.Repositories;
using MvcUI.Services;
using MvcUI.ViewModels;

namespace MvcUI.Controllers
{
    [Authorize]
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
            _lotManagerService = new LotManagerService(_crudLotService, _crudUserService);
        }


        public ActionResult Index(LotsRequestModel lotsRequest)
        {
            //for (var i = 0; i < 57; i++)
            //{
            //    var bllLot = new BLLLot
            //    {
            //        ArtworkName = "Name_" + i,
            //        DateOfAuction = DateTime.Now,
            //        UserOwnerId = 1,
            //        Author = "podlec"
            //    };

            //    _crudLotService.CreateLot(bllLot);
            //}

            return View(new LotsViewModel());
        }

        public ActionResult Lots(LotsRequestModel lotsRequest)
        {
            if (lotsRequest.PageNumber == 0)
            {
                lotsRequest.PageNumber = 1;
            }

            lotsRequest.LotsCountOnPage = 10;

            var lots = _lotManagerService.GetLotsByTabName(lotsRequest.Tab, User.Identity.Name);

            var model = _lotManagerService.BuildPagingModel(lots, lotsRequest, User.Identity.Name);

            return PartialView("_AllLots", model);
        }

        public ActionResult Lot(int id)
        {
            var lot = _crudLotService.GetLotById(id);
            var lotView = new LotViewModel(lot);
            var emailOfCurUser = User.Identity.Name;
            var userId = _crudUserService.GetUserByEmail(emailOfCurUser).Id;
            lotView.CurrentUserId = userId;

            ViewBag.CanRate = userId != lot.UserOwnerId;
            return View(lotView);
        }

        public ActionResult CreateLot()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateLot(LotCreateModel newLot)
        {
            if (ModelState.IsValid)
            {
                var user = _crudUserService.GetUserByEmail(User.Identity.Name);

                var createdLot = new BLLLot
                {
                    ArtworkName = newLot.ArtworkName,
                    Author = newLot.Author,
                    Photos = newLot.Photos,
                    ArtworkFormat = newLot.ArtworkFormat,
                    Description = newLot.Description,
                    YearOfCreation = newLot.YearOfCreation,
                    StartingPrice = newLot.StartingPrice,
                    MinimalStepRate = newLot.MinimalStepRate,
                    DateOfAuction = newLot.DateOfAuction,
                    CurrentPrice = newLot.StartingPrice,
                    UserOwnerId = user.Id
                };

                _crudLotService.CreateLot(createdLot);
                return RedirectToAction("Index", "LotManager");
            }

            return View(); 
        }

        [HttpPost]
        public ActionResult Lot(LotViewModel lotView)
        {
            ViewBag.CanRate = true;
            if (ModelState.IsValid == false)
            {
                return View("Lot", lotView);
            }

            decimal price;
            if (decimal.TryParse(lotView.PriceRate, out price) == false)
            {
                ModelState.AddModelError("", "Price should be decimal");
                return View("Lot", lotView);
            }

            //todo save
            _lotService.MakeRate(lotView.Id, lotView.CurrentUserId, price);

            return RedirectToAction("Index", "LotManager");
        }
    }
}