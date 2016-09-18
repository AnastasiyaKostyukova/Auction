using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcUI.ViewModels
{
    public class AboutModel
    {
        public int CountUsers { get; set; }
        public int CountSoldLots { get; set; }
        public int CountActiveLots { get; set; }
    }
}