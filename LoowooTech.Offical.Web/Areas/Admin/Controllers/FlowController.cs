using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Admin.Controllers
{
    public class FlowController : AdminControllerBase
    {
        // GET: Admin/Flow
        public ActionResult Index()
        {
            var list = Core.FlowManager.GetList();
            ViewBag.List = list;
            return View();
        }


        public ActionResult Create(int id = 0)
        {
            var flow = Core.FlowManager.Get(id);
            ViewBag.Flow = flow;
            ViewBag.Systems = Core.LWSystemManager.Get();
            return View();
        }

        [HttpPost]
        public ActionResult Save(Models.Admin.Flow flow)
        {
            if (flow.ID > 0)
            {
                if (!Core.FlowManager.Edit(flow))
                {
                    return ErrorJsonResult("编辑流程信息失败！未找到相关流程信息！");
                }
            }
            else
            {
                var id = Core.FlowManager.Add(flow);
                if (id <= 0)
                {
                    return ErrorJsonResult("添加流程信息失败！");
                }
            }

            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.FlowManager.Delete(id))
            {
                return ErrorJsonResult("删除流程信息失败！未找到相关流程信息！");
            }
            return SuccessJsonResult();
        }
    }
}