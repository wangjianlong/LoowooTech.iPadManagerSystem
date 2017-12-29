using System.Web.Mvc;
using LoowooTech.Models.Admin;

namespace LoowooTech.Offical.Web.Areas.Expense
{
    public class ExpenseControllerBase : Web.Controllers.ControllerBase
    {
        private const string _Name = "报销单";
        private static LoowooTech.Models.Admin.Flow _flow { get; set; }
        public LoowooTech.Models.Admin.Flow Flow { get { return _flow == null ? _flow = Core.FlowManager.Get(_Name) : _flow; } }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Flow = Flow;
            base.OnActionExecuting(filterContext);
        }
    }
}