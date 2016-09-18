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

            lotView.CanDelete = (currentUserId == lot.UserOwnerId
                                 && ((lot.LotIsFinishedAuction && lot.CurrentBuyerId == 0) || !lot.LotIsFinishedAuction))
                                || User.IsInRole("admin");

            lotView.CanRate = currentUserId != lot.UserOwnerId && lot.LotIsFinishedAuction == false;
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

            var createdLot = BuildBllLot(newLot, user.Id);

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

        private BLLLot BuildBllLot(LotCreateModel newLot, int userId)
        {
            var bllLot = new BLLLot
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
                UserOwnerId = userId
            };
            return bllLot;
        }
    }
}