using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Customer
{
    public class CustomerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Customer";//客户管理系统
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Customer_default",
                "Customer/{controller}/{action}/{id}",
                new { Controller="Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}