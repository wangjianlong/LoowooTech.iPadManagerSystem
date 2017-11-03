﻿using LoowooTech.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LoowooTech.Offical.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected virtual void Application_BeginRequest()
        {
            OneContext.Begin();
        }

        protected virtual void Application_EndRequest()
        {
            OneContext.End();
        }
    }
}
