using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Voucher
{
    public class VoucherAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Voucher";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Voucher_default",
                "Voucher/{controller}/{action}/{id}",
                new { Controller="Home", action = "Index", id = UrlParameter.Optional },
                new [] { "LoowooTech.Offical.Web.Areas.Voucher.Controllers" }
            );
        }
    }
}