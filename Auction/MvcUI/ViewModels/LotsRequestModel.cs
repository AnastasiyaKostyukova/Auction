using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcUI.ViewModels
{
    public class LotsRequestModel
    {
        public string Tab { get; set; }
        public int PageNumber { get; set; }
        public int LotsCountOnPage { get; set; }
    }
}