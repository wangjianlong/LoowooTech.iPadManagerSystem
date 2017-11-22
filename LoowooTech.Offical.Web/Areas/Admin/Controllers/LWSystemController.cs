using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Admin.Controllers
{
    public class LWSystemController : AdminControllerBase
    {
        public ActionResult Index()
        {
            var list = Core.LWSystemManager.Get();
            ViewBag.List = list;
            return View();
        }

        public ActionResult Create(int id = 0)
        {
            var model = Core.LWSystemManager.Get(id);
            ViewBag.Model = model;
            return View();
        }
        [HttpPost]
        public ActionResult Save(LWSystem lw)
        {
            if (lw.ID > 0)
            {
                if (!Core.LWSystemManager.Edit(lw))
                {
                    return ErrorJsonResult("编辑子系统失败");
                }
            }
            else
            {
                var id = Core.LWSystemManager.Add(lw);
                if (id <= 0)
                {
                    return ErrorJsonResult("添加子系统失败");
                }
            }
            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.LWSystemManager.Delete(id))
            {
                return ErrorJsonResult("删除失败，未找到相关系统信息，可能已经被删除了！");
            }
            return SuccessJsonResult();
        }
    }
}