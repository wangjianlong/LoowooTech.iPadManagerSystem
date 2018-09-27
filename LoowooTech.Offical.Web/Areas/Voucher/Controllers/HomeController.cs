using LoowooTech.Models.Voucher;
using LoowooTech.Offical.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Voucher.Controllers
{
    public class HomeController : VoucherControllerBase
    {
        // GET: Voucher/Home
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(EInvoice invoice)
        {
            if (Request.Files.Count == 0)
            {
                throw new ArgumentException("请上传电子发票文件!");
            }
            var file = HttpContext.Request.Files[0];
            var fileName = file.FileName;
            var info = FilePost.Upload(file, "EInvoices", Identity.UserId);
            if (info == null)
            {
                throw new ArgumentException("保存文件信息失败！");
            }
            var fileId = Core.FileInfoManager.Add(info);
            return View();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.EInvoiceManager.Delete(id))
            {
                return ErrorJsonResult("删除失败！");
            }
            return SuccessJsonResult();
        }
    }
}