using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Tablets
{
    public class TabletsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Tablets";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Tablets_default",
                "Tablets/{controller}/{action}/{id}",
                new { Controller="Home", action = "Index", id = UrlParameter.Optional },
                new[] { "LoowooTech.Offical.Web.Areas.Tablets.Controllers" }
            );
        }
    }
}