using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Admin.Controllers
{
    public class CompanyController : AdminControllerBase
    {
        // GET: Admin/Company
        public ActionResult Index()
        {
            var list = Core.CompanyManager.GetList();
            ViewBag.List = list;
            var projects = Core.CompanyManager.GetProjects();
            ViewBag.Projects = projects;
            return View();
        }

        public ActionResult Create(int id = 0)
        {
            var company = Core.CompanyManager.Get(id);
            ViewBag.Company = company;
            return View();
        }

        [HttpPost]
        public ActionResult Save(Company company)
        {
            if (company.ID > 0)
            {
                if (!Core.CompanyManager.Edit(company))
                {
                    return ErrorJsonResult("编辑单位信息失败！");
                }
            }
            else
            {
                var id = Core.CompanyManager.Add(company);
                if (id <= 0)
                {
                    return ErrorJsonResult("新建单位信息失败！");
                }
            }
            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.CompanyManager.Delete(id))
            {
                return ErrorJsonResult("删除单位信息失败!未找到相关单位信息！");
            }
            return SuccessJsonResult();
        }
    }
}