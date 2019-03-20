using LoowooTech.Models.Tablets;
using LoowooTech.Offical.Web.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Tablets.Controllers
{
    public class AuthorityController : TabletsControllerBase
    {
        // GET: Tablets/Authority
        public ActionResult Index(DateTime? startTime=null,DateTime? endTime=null,string name=null,int? UserId=null, int page=1,int rows=20)
        {
            var parameter = new AuthorityParameter
            {
                StartTime = startTime,
                EndTime = endTime,
                Name = name,
                UserId=UserId,
                Delete = false,
                Page = new Models.PageParameter(page, rows)
            };
            var list = Core.AuthorityManager.Search(parameter);
            var users = Core.UserManager.GetList();
            ViewBag.Users = users;
            ViewBag.List = list;
            ViewBag.Parameter = parameter;
            return View();
        }

        [ChildActionOnly]
        public ActionResult Widget()
        {
            var parameter = new AuthorityParameter
            {
                Delete = false,
                Page = new Models.PageParameter(1, 5)
            };
            var list = Core.AuthorityManager.Search(parameter);
            ViewBag.List = list;
            ViewBag.Parameter = parameter;
            return View();
        }


        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Save()
        {
            if (Request.Files.Count == 0)
            {
                throw new ArgumentException("请选择上传文件！");
            }
            var list = new List<Authority>();
            for(var i = 0; i < HttpContext.Request.Files.Count; i++)
            {
                var file = HttpContext.Request.Files[i];
                var fileName = file.FileName;
                if (string.IsNullOrEmpty(fileName))
                {
                    continue;
                }
                var info = FilePost.Upload(file, "Authority", Identity.UserId, true);
                var fileId = Core.FileInfoManager.Add(info);
                if (fileId > 0)
                {
                    list.Add(new Authority {

                        FileId=fileId,
                        UserId=Identity.UserId,
                        Name=info.FileName
                    });
                }
            }
            Core.AuthorityManager.AddRange(list);
            return RedirectToAction("Index");
        }

       
    }
}