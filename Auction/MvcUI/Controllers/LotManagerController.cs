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

        public LotManagerController(
            ICRUDLotService crudLotService,
            ICRUDUserService crudUserService)
        {
            _crudLotService = crudLotService;
            _crudUserService = crudUserService;
            _lotManagerService = new LotManagerService(_crudLotService, _crudUserService);
        }


        public ActionResult Index(LotsRequestModel lotsRequest)
        {
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

            var model = _lotManagerService.BuildPagingModel(lots, lotsRequest);

            return PartialView("_AllLots", model);
        }

        public ActionResult Lot(int id)
        {
            var lot = _crudLotService.GetLotById(id);
            var lotView = new LotViewModel(lot);
            var emailOfCurUser = User.Identity.Name;
            lotView.CurrentUserId = _crudUserService.GetUserByEmail(emailOfCurUser).Id;
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
                    CurrentPrice = newLot.StartingPrice
                };

                _crudLotService.CreateLot(createdLot, user.Id);
                return RedirectToAction("Index", "Home");
            }

            return View(); 
        }
    }
}