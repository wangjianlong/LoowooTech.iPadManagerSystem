using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Address
{
    public class AddressAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Address";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Address_default",
                "Address/{controller}/{action}/{id}",
                new { Controller="Home", action = "Index", id = UrlParameter.Optional },
                new[] { "LoowooTech.Offical.Web.Areas.Address.Controllers" }
            );
        }
    }
}