using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Models;

namespace MvcUI.ViewModels
{
    public class UserEditModel
    {
        public int Id { get; set; }
        public int CurrentUserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }

        public int CountLotsSold { get; set; }
        public int CountOfRatesOnLots { get; set; }

        public List<BLLLot> Lots { get; set; }
        public bool IsBanned { get; set; }
    }
}