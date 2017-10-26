using LoowooTech.iPadManagerSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.iPadManagerSystem.Controllers
{
    public class ColorController : ControllerBase
    {
        // GET: Color
        public ActionResult Index()
        {
            var list = Core.ColorManager.GetList();
            ViewBag.List = list;
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(Color color)
        {
            if (color.ID > 0)
            {
                if (Core.ColorManager.Edit(color))
                {
                    return SuccessJsonResult();
                }
            }
            else
            {
                var id = Core.ColorManager.Add(color);
                if (id > 0)
                {
                    return SuccessJsonResult();
                }
            }
            return ErrorJsonResult("保存颜色失败");
        }

        public ActionResult Delete(int id)
        {
            if (!Core.ColorManager.Delete(id))
            {
                return ErrorJsonResult("删除颜色失败，未找到相关数据库信息");
            }
            return SuccessJsonResult();
        }
    }
}