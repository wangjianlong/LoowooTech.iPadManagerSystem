using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Admin.Controllers
{
    public class FlowNodeController : AdminControllerBase
    {
        
        public ActionResult Create(int id=0,int flowId = 0)
        {
            var flowNode = Core.FlowNodeManager.Get(id);
            ViewBag.FlowNode = flowNode;
            if (flowNode != null)
            {
                flowId = flowNode.FlowId;
            }
            ViewBag.Flow = Core.FlowManager.Get(flowId);
            ViewBag.Users = Core.UserManager.GetList();
            return View();
        }

        [HttpPost]
        public ActionResult Save(FlowNode node)
        {
            if (node.ID > 0)
            {
                if (!Core.FlowNodeManager.Edit(node))
                {
                    return ErrorJsonResult("编辑流程节点失败！未找到相关节点信息！");
                }
            }
            else
            {
                var id = Core.FlowNodeManager.Add(node);
                if (id <= 0)
                {
                    return ErrorJsonResult("添加流程节点失败！");
                }
            }
            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.FlowNodeManager.Delete(id))
            {
                return ErrorJsonResult("删除节点失败！");
            }
            return SuccessJsonResult();
        }

        public ActionResult Prev(int id)
        {
            if (!Core.FlowNodeManager.Prev(id))
            {
                return ErrorJsonResult("上移失败！");
            }
            return SuccessJsonResult();
        }

        public ActionResult Later(int id)
        {
            if (!Core.FlowNodeManager.Later(id))
            {
                return ErrorJsonResult("下移失败！");
            }
            return SuccessJsonResult();
        }
    }
}