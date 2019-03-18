using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Tablets.Controllers
{
    public class TypeController : TabletsControllerBase
    {
        // GET: Tablets/Type
        public ActionResult Index()
        {
            var list = Core.TabletTypeManager.GetList();
            ViewBag.List = list;
            return View();
        }

        public ActionResult Create(int id=0)
        {
            var model = Core.TabletTypeManager.Get(id);
            ViewBag.Model = model;
            return View();
        }

        [HttpPost]
        public ActionResult Save(TabletType type)
        {
            if (type.ID > 0)
            {
                if (!Core.TabletTypeManager.Edit(type))
                {
                    return ErrorJsonResult("编辑平板信息失败!");
                }
            }
            else
            {
                var id = Core.TabletTypeManager.Add(type);
                if (id <= 0)
                {
                    return ErrorJsonResult("添加平板类型失败");
                }
            }
            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.TabletTypeManager.Delete(id))
            {
                return ErrorJsonResult("删除失败");
            }
            return SuccessJsonResult();
        }
    }
}