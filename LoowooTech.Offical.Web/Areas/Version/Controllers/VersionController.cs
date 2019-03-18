using LoowooTech.Offical.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Version.Controllers
{
    public class VersionController : VersionsControllerBase
    {
        // GET: Version/Version
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Widget()
        {
            var parameter = new Models.Versions.VersionParameter
            {
                Delete = false,
                Page = new Models.PageParameter(1, 10)
            };
            var list = Core.VersionManager.Search(parameter);
            ViewBag.List = list;
            ViewBag.Parameter = parameter;
            return View();
        }

        public ActionResult Create(int id)
        {
            var program = Core.ProgramManager.Get(id);
            ViewBag.Program = program;
            return View();
        }

        [HttpPost]
        public ActionResult Save(Models.Versions.Version version)
        {
            if (Request.Files.Count == 0)
            {
                throw new ArgumentException("请上传程序版本文件!");
            }
            var file = HttpContext.Request.Files[0];
            var fileName = file.FileName;
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("请上传有效的程序文件！");
            }
            var info = FilePost.Upload(file, "Versions", Identity.UserId);
            if (info == null)
            {
                throw new ArgumentException("上传文件失败！");
            }
            var fileId = Core.FileInfoManager.Add(info);
            if (fileId <= 0)
            {
                throw new ArgumentException("保存文件记录信息失败！");
            }
            version.FileId = fileId;
            var id = Core.VersionManager.Add(version);
            if (id <= 0)
            {
                throw new Exception("保存版本信息失败!");
            }

            return RedirectToAction("Detail", "Program", new { id = version.ProgramId });
        }

        public ActionResult Delete(int id)
        {
            if (!Core.VersionManager.Delete(id))
            {
                return ErrorJsonResult("删除失败！");
            }

            return SuccessJsonResult();
        }

        public ActionResult Search(int? programId=null,int? userId=null,int page=1,int rows=20)
        {
            var parameter = new Models.Versions.VersionParameter
            {
                ProgramId = programId,
                UserId = userId,
                Page = new Models.PageParameter(page, rows)
            };
            var list = Core.VersionManager.Search(parameter);
            ViewBag.List = list;
            return View();
        }





    }
}