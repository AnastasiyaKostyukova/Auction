using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MvcUI.Infrastructure;

namespace MvcUI
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			DependencyResolver.SetResolver(new NinjectDependencyResolver());
		}

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            if (exception == null)
            {
                return;
            }

            var errorString = $"{exception.GetType()}: {exception.Message}";

            //Logger.Error(errorString);

            //Response.Write("<center><h1>Global Page Error</h1>\n");
            //Response.Write("<p>" + errorString + "</p></center>");

            Server.ClearError();
            HttpContext.Current.Response.Redirect("~/Error.html");
        }
    }
}
