using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Models;
using BLL.Interface.Services;
using DAL.Interface.Repositories;
using MvcUI.ViewModels;

namespace MvcUI.Controllers
{
    [Authorize]
    public class LotManagerController : Controller
    {
        private readonly ICRUDLotService _crudLotService;
        private readonly ICRUDUserService _crudUserService;

        public LotManagerController(
            ICRUDLotService crudLotService,
            ICRUDUserService crudUserService)
        {
            _crudLotService = crudLotService;
            _crudUserService = crudUserService;
        }

        public ActionResult Index()
        {
            var allLots = _crudLotService.GetAllLots().ToList();
            return View(allLots);
        }

        public ActionResult Lot(int id)
        {
            var lot = _crudLotService.GetLotById(id);
            return View(lot);
        }

        public ActionResult CreateLot()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateLot(LotViewModel newLot)
        {
            if (ModelState.IsValid)
            {
                var sellerOfLot = _crudUserService.GetUserByEmail(User.Identity.Name);
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
                    SellerId = sellerOfLot.Id,
                    CurrentPrice = newLot.StartingPrice
                };
                _crudLotService.CreateLot(createdLot);
                return RedirectToAction("Index", "Home");
            }

            return View(); 
        }
    }
}