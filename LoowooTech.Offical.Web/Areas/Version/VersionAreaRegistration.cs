using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Version
{
    public class VersionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Version";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Version_default",
                "Version/{controller}/{action}/{id}",
                new { Controller="Home", action = "Index", id = UrlParameter.Optional },
                new[] { "LoowooTech.Offical.Web.Areas.Version.Controllers" }
            );
        }
    }
}