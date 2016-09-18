using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MvcUI.Infrastructure;
using NLog;

namespace MvcUI
{
	public class MvcApplication : System.Web.HttpApplication
	{
	    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			DependencyResolver.SetResolver(new NinjectDependencyResolver());

            Logger.Debug("Application started");
		}

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            if (exception == null)
            {
                return;
            }

            var errorString = $"{exception.GetType()}: {exception.Message}";
            Logger.Error(errorString);

            //Response.Write("<center><h1>Global Page Error</h1>\n");
            //Response.Write("<p>" + errorString + "</p></center>");

            Server.ClearError();
            HttpContext.Current.Response.Redirect("~/Error.html");
        }

        protected void Application_BeginRequest()
        {
            //NOTE: Stopping IE from being a caching whore
            HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.Now);
            Response.Cache.SetValidUntilExpires(true);
        }
    }
}
