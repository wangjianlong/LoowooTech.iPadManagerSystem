using LoowooTech.Common;
using LoowooTech.Models.Tablets;
using LoowooTech.Offical.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Tablets.Controllers
{
    public class AtlasController : TabletsControllerBase
    {
  
        // GET: Tablets/Atlas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int accountId)
        {
            var account = Core.AccountManager.Get(accountId);
            ViewBag.Account = account;
            return View();
        }

        [HttpPost]
        public ActionResult Save(Atlas atlas,string accountName)
        {
            if (Request.Files.Count == 0)
            {
                throw new ArgumentException("请上传程序文件");
            }
            var file = HttpContext.Request.Files[0];
            var fileName = file.FileName;
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("请上传有效的程序文件！");
            }
            var info = FilePost.Upload(file, accountName+"atlas", Identity.UserId,true);
            if (info == null)
            {
                throw new ArgumentException("上传文件失败！");
            }
            var fileId = Core.FileInfoManager.Add(info);
            if (fileId <= 0)
            {
                throw new Exception("保存文件记录信息失败！");
            }
            atlas.FileId = fileId;
            var id = Core.AtlasManager.Add(atlas);
            if (id <= 0)
            {
                throw new Exception("保存发布程序记录失败！");
            }
            try
            {
                AtlasExtension.Save(System.Configuration.ConfigurationManager.AppSettings["AtlasPath"].ToString(), atlas.AccountId.ToString(), atlas.Number, System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, info.Path));

            }catch(Exception ex)
            {
                throw new Exception("生成配置文件发生错误，错误信息:" + ex.Message);
            }
            return RedirectToAction("Detail", "Account", new { id = atlas.AccountId });
        }
    }
}