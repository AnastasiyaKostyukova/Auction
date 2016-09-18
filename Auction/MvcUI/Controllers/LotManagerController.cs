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

            var lots = _lotManagerService.GetLotsByTabName(lotsRequest.Tab, User.Identity.Name);

            var bllSearchModel = new BLLSearch
            {
                SearchByArtworkName = lotsRequest.ArtworkName,
                SearchByPictureAuthor = lotsRequest.PictureAuthor,
                SearchByMinPrice = lotsRequest.MinPrice,
                SearchByMaxPrice = lotsRequest.MaxPrice,
                OrderByAuctionDate = lotsRequest.OrderByAuctionDate
            };
            lots = _lotService.Search(bllSearchModel, lots).ToList();
            var model = _lotManagerService.BuildPagingModel(lots, lotsRequest, User.Identity.Name);

            return PartialView("_AllLots", model);
        }

        public ActionResult Lot(int id)
        {
            var lot = _crudLotService.GetLotById(id);
            var lotView = new LotViewModel(lot);
            var emailOfCurrentUser = User.Identity.Name;
            var currentUserId = _crudUserService.GetUserByEmail(emailOfCurrentUser).Id;
            lotView.CurrentUserId = currentUserId;

            ViewBag.CanUpdate = lot.UserOwnerId == currentUserId && lot.LotIsFinishedAuction == false;
            ViewBag.CanRate = currentUserId != lot.UserOwnerId && lot.LotIsFinishedAuction == false;
            ViewBag.CanDelete = (currentUserId == lot.UserOwnerId && lot.LotIsFinishedAuction 
                && lot.CurrentBuyerId == 0) || (currentUserId == lot.UserOwnerId 
                && !lot.LotIsFinishedAuction);
            return View(lotView);
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

            _lotService.MakeRate(lotView.Id, lotView.CurrentUserId, price);

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

        public ActionResult UpdateLot(int id)
        {
            var bllLot = _crudLotService.GetLotById(id);
            var lot = new LotUpdateModel()
            {
                Id = bllLot.Id,
                ArtworkName = bllLot.ArtworkName,
                Author = bllLot.Author,
                Description = bllLot.Description,
                Photos = bllLot.Photos,
                StartingPrice = bllLot.StartingPrice,
                YearOfCreation = bllLot.YearOfCreation,
                MinimalStepRate = bllLot.MinimalStepRate,
                DateOfAuction = bllLot.DateOfAuction,
                ArtworkFormat = bllLot.ArtworkFormat,
                UpdatingDateOfAuction = bllLot.DateOfAuction
            };

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