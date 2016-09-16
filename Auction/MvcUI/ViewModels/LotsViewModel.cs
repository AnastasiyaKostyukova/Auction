using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Models;

namespace MvcUI.ViewModels
{
    public class LotsViewModel
    {
        public LotsViewModel()
        {
            Lots = new List<BLLLot>();
        }

        public string Tab { get; set; }
        public List<BLLLot> Lots { get; set; }
        public int PageNumber { get; set; }

        public int MaxPageNumber { get; set; }
        public int CurrentUserId { get; set; }
    }
}