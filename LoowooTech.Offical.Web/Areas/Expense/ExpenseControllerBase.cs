using LoowooTech.Models.Admin;

namespace LoowooTech.Offical.Web.Areas.Expense
{
    public class ExpenseControllerBase : Web.Controllers.ControllerBase
    {
        private const string _Name = "报销单";
        private static Flow _flow { get; set; }
        public Flow Flow { get { return _flow == null ? _flow = Core.FlowManager.Get(_Name) : _flow; } }
    }
}