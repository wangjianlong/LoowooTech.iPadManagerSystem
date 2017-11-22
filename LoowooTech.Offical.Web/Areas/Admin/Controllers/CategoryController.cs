using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Admin.Controllers
{
    public class CategoryController : AdminControllerBase
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            var tree = Core.CategoryManager.GetTree();
            ViewBag.Tree = tree;
            return View();
        }

        public ActionResult Create(int id = 0,int categoryId=0)
        {
            var category = Core.CategoryManager.Get(id);
            ViewBag.Category = category;
            ViewBag.Prev = Core.CategoryManager.Get(categoryId);
            return View();
        }

        [HttpPost]
        public ActionResult Save(Category category)
        {
            if (category.ID>0)
            {
                if (!Core.CategoryManager.Edit(category))
                {
                    return ErrorJsonResult("编辑类别信息失败！");
                }
            }
            else
            {
                var id = Core.CategoryManager.Add(category);
                if (id <= 0)
                {
                    return ErrorJsonResult("添加类别失败！");
                }
            }
            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.CategoryManager.Delete(id))
            {
                return ErrorJsonResult("删除类别失败！");
            }
            return SuccessJsonResult();
        }
    }
}