using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Admin.Controllers
{
    public class GroupController : AdminControllerBase
    {
        // GET: Admin/Group
        public ActionResult Index()
        {
            var list = Core.GroupManager.GetList();
            ViewBag.List = list;
            return View();
        }

        public ActionResult Create(int id = 0)
        {
            var group = Core.GroupManager.Get(id);
            ViewBag.Group = group;
            return View();
        }

        [HttpPost]
        public ActionResult Save(Group group)
        {
            if (group.ID > 0)
            {
                if (!Core.GroupManager.Edit(group))
                {
                    return ErrorJsonResult("编辑部门组信息失败！");
                }
            }
            else
            {
                var id = Core.GroupManager.Add(group);
                if (id <= 0)
                {
                    return ErrorJsonResult("新建部门组信息失败！");
                }
            }
            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.GroupManager.Delete(id))
            {
                return ErrorJsonResult("删除部门/组信息失败！");
            }
            return SuccessJsonResult();
        }
    }
}