using LoowooTech.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Project.Controllers
{
    public class InvoiceController : ProjectControllerBase
    {
        // GET: Project/Invoice
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int projectId)
        {
            var project = Core.ProjectManager.Get(projectId);
            ViewBag.Project = project;
            return View();
        }

        public ActionResult Save(Invoice invoice)
        {
            return SuccessJsonResult();
        }
    }
}