using BLL.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUI.ViewModels;

namespace MvcUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ICRUDLotService _crudLotService;
        private readonly ICRUDUserService _crudUserService;

        public AdminController(
            ICRUDLotService crudLotService,
            ICRUDUserService crudUserService)
        {
            _crudLotService = crudLotService;
            _crudUserService = crudUserService;
        }

        public ActionResult UsersEdit(int id)
        {
            var currentUserId = _crudUserService.GetUserByEmail(User.Identity.Name).Id;
            var user = _crudUserService.GetUserById(id);
            var allHisLots = _crudLotService.GetAllLotsOfUser(id).ToList();
            var countOfLotsSold = allHisLots.Where(l => l.LotIsFinishedAuction && l.CurrentBuyerId != 0).ToList().Count;
            var countOfLotsWithRates = allHisLots.Where(l => l.LotIsFinishedAuction == false && l.CurrentBuyerId != 0).ToList().Count;

            var isBanned = _crudUserService.GetBannedUsers().Any(r => r.Id == id);
            var userEdit = new UserEditModel
            {
                Id = id,
                CurrentUserId = currentUserId,
                UserName = user.UserName,
                Email = user.Email,
                CreationDate = user.CreationDate,
                Lots = allHisLots,
                CountLotsSold = countOfLotsSold,
                CountOfRatesOnLots = countOfLotsWithRates,
                IsBanned = isBanned
            };
            return View(userEdit);
        }

        [HttpPost]
        public ActionResult BanUser(int id)
        {
            _crudUserService.ChangeBanStatusUser(id, true);
            return RedirectToAction("BannedUsers");
        }

        [HttpPost]
        public ActionResult UnBanUser(int id)
        {
            _crudUserService.ChangeBanStatusUser(id, false);
            return RedirectToAction("BannedUsers");
        }

        public ActionResult BannedUsers()
        {
            var bannedUsers = _crudUserService.GetBannedUsers();
            return View(bannedUsers);
        }
    }
}