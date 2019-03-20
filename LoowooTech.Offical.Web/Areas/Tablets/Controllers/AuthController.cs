using LoowooTech.Offical.Web.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Tablets.Controllers
{
    public class AuthController : Controller
    {
        // GET: Tablets/Auth
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Get(string code)
        {
            var folder = System.IO.Path.Combine(FilePost.SaveFolder, "Authority");
            var files = new DirectoryInfo(folder).GetFiles(string.Format("{0}.sn", code), SearchOption.AllDirectories);
            if (files.Length == 1)
            {
                var file = files[0];
                return File(new FileStream(file.FullName, FileMode.Open), "application/octet-stream", file.Name);
            }
            return null;
        }
    }
}