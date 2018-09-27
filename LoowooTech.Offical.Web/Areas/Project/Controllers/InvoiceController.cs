using LoowooTech.Models.Project;
using LoowooTech.Offical.Web.Common;
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
            ViewBag.Contracts = Core.ContractManager.GetList(projectId);
            return View();
        }

        [HttpPost]
        public ActionResult Save(Models.Project.Invoice invoice)
        {
            if (HttpContext.Request.Files.Count == 0)
            {
                throw new ArgumentException("请上传发票扫描件！");
            }
            var file = HttpContext.Request.Files[0];
            var fileName = file.FileName;
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("请上传有效扫描件");
            }
            var info = FilePost.Upload(file, "Invoices", Identity.UserId);
            if (info == null)
            {
                throw new Exception("保存发票扫描件失败");
            }
            var fileId = Core.FileInfoManager.Add(info);
            if (fileId <= 0)
            {
                throw new Exception("保存发票扫描件信息到数据库失败");
            }
            invoice.FileInfoId = fileId;
            if (invoice.ID > 0)
            {
                if (!Core.InvoiceManager.Edit(invoice))
                {
                    throw new ArgumentException("编辑发票信息失败！");
                }
            }
            else
            {
                var id = Core.InvoiceManager.Add(invoice);
                if (id <= 0)
                {
                    throw new ArgumentException("添加发票信息失败！");
                }
            }
            return RedirectToAction("Detail","Home",new { id=invoice.ProjectId,activeLabel="Invoice"});
        }

        public ActionResult DetailList(int projectId)
        {
            var list = Core.InvoiceManager.GetList(projectId);
            ViewBag.List = list;
            ViewBag.ProjectId = projectId;

         
            return View();
        }
    }
}