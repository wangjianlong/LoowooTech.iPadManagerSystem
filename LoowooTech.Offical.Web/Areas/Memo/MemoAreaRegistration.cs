using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Memo
{
    public class MemoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Memo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Memo_default",
                "Memo/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "LoowooTech.Offical.Web.Areas.Memo.Controllers" }
            );
        }
    }
}