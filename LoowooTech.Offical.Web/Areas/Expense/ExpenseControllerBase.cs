using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LoowooTech.Models.Admin;

namespace LoowooTech.Offical.Web.Areas.Expense
{
    public class ExpenseControllerBase : Web.Controllers.ControllerBase
    {
        private const string _Name = "报销单";
        private static LoowooTech.Models.Admin.Flow _flow { get; set; }
        public LoowooTech.Models.Admin.Flow Flow { get { return _flow == null ? _flow = Core.FlowManager.Get(_Name) : _flow; } }

        //private static List<Flow> _flows { get; set; }
        //public List<Flow> Flows { get { return _flows == null ? _flows = Core.FlowManager.GetList(_Name) : _flows; } }
        //private static List<FlowNode> _flowNodes { get; set; }
        //public List<FlowNode> FlowNodes { get { return _flowNodes == null ? _flowNodes = GetFlowNodes(Identity.UserId) : _flowNodes; } }
        //private List<FlowNode> GetFlowNodes(int userId)
        //{
        //    var list = new List<FlowNode>();
        //    foreach(var flow in Flows)
        //    {
        //        if (flow.FlowNodes != null)
        //        {
        //            var temp = flow.FlowNodes.Where(e => e.UserIds.Contains(userId)).ToList();
        //            list.AddRange(temp);
        //        }
        //    }
        //    return list;
        //}
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Flow = Flow;
            base.OnActionExecuting(filterContext);
        }
    }
}