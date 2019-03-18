using LoowooTech.Models.Versions;
using LoowooTech.Offical.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Version.Controllers
{
    public class ProgramController : VersionsControllerBase
    {
        // GET: Version/Program
        public ActionResult Index(string name=null,int? UserId=null,int page=1,int rows=20)
        {
            var parameter = new ProgramParameter
            {
                Name = name,
                UserId = UserId,
                Delete = false,
                Page = new Models.PageParameter(page, rows)
            };
            var list = Core.ProgramManager.Search(parameter);
            ViewBag.List = list;
            ViewBag.Parameter = parameter;

            return View();
        }

        [ChildActionOnly]
        public ActionResult Widget()
        {
            var parameter = new ProgramParameter
            {
                Delete = false,
                Page = new Models.PageParameter(1, 10)
            };
            var list = Core.ProgramManager.Search(parameter);
            ViewBag.List = list;
            ViewBag.Parameter = parameter;
            return View();
        }

        public ActionResult Create(int id=0)
        {
            var program = Core.ProgramManager.Get(id);
            ViewBag.Program = program;
            return View();
        }

        [HttpPost]
        public ActionResult Save(Program program)
        {
            if (program.ID > 0)
            {
                if (Core.ProgramManager.Edit(program) == false)
                {
                    return ErrorJsonResult("编辑程序信息失败！");
                }
            }
            else
            {
                var id = Core.ProgramManager.Add(program);
                if (id <= 0)
                {
                    return ErrorJsonResult("添加程序失败！");
                }
            }
            return SuccessJsonResult(program.ID);
        }

        public ActionResult Delete(int id)
        {
            if (Core.ProgramManager.Delete(id) == false)
            {
                return ErrorJsonResult("删除失败!");
            }
            return SuccessJsonResult();
        }

        public ActionResult Detail(int id)
        {
            var program = Core.ProgramManager.Get(id);
            ViewBag.Program = program;
            return View();
        }

        public ActionResult File(int id)
        {
            var program = Core.ProgramManager.Get(id);
            ViewBag.Program = program;
            return View();
        }

        [HttpPost]
        public ActionResult SaveFile(int programId)
        {
            if (Request.Files.Count > 0)
            {
                var list = new List<ProgramFile>();
                for(var i = 0; i < HttpContext.Request.Files.Count; i++)
                {
                    var file = HttpContext.Request.Files[i];
                    var fileName = file.FileName;
                    if (string.IsNullOrEmpty(fileName) == false)
                    {
                        var info = FilePost.Upload(file, "ProgramFiles", Identity.UserId);
                        var fileId = Core.FileInfoManager.Add(info);
                        if (fileId > 0)
                        {
                            list.Add(new ProgramFile
                            {
                                ProgramId = programId,
                                FileId = fileId,
                                Name = fileName,
                                UserId=Identity.UserId
                            });
                        }
                    }
                }
                Core.ProgramManager.AddFiles(list);

            }
            return RedirectToAction("Detail", new { Id = programId });
        }

        public ActionResult DeleteFile(int id)
        {
            if (!Core.ProgramManager.DeleteFile(id))
            {
                return ErrorJsonResult("删除附件失败！");
            }

            return SuccessJsonResult();
        }
    }
}