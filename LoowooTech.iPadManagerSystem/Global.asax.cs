using LoowooTech.iPadManagerSystem.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LoowooTech.iPadManagerSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected virtual void Application_BeginRequest()
        {
            HttpDbContextContainer.OnBeginRequest(Context, new DataContext());
        }
        protected virtual void Application_EndRequest()
        {
            HttpDbContextContainer.OnEndRequest(Context);
        }
    }
}
