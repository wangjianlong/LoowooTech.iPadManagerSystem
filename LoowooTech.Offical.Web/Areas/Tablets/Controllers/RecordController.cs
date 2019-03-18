using LoowooTech.Models.Tablets;
using LoowooTech.Offical.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Tablets.Controllers
{
    public class RecordController : TabletsControllerBase
    {
        // GET: Tablets/Record
        public ActionResult Index(int? projectId=null,int? cityId=null, int page=1,int rows=20)
        {
            var parameter = new RecordParameter
            {
                ProjectId = projectId,
                CityId = cityId,
                Page = new Models.PageParameter(page, rows)
            };
            var list = Core.RecordManager.Search(parameter);
            ViewBag.List = list;
            ViewBag.Parameter = parameter;
            ViewBag.Citys = Core.CityManager.GetTree();
            ViewBag.Projects = Core.ProjectManager.Search(new Models.Project.ProjectParameter { IsDone = false, Delete = false });
            return View();
        }


        public ActionResult Create(int id=0)
        {
            var record = Core.RecordManager.Get(id);
            ViewBag.Model = record;
            var tablets = Core.TabletManager.GetUse();
            ViewBag.Tablets = tablets;
            ViewBag.Citys = Core.CityManager.GetTree();
            ViewBag.Projects = Core.ProjectManager.Search(new Models.Project.ProjectParameter { IsDone = false, Delete = false });
            return View();
        }

        [HttpPost]
        public ActionResult Save(Record record,int[] tabletIds,string projectName)
        {
            if (record.CityId == 0)
            {
                return ErrorJsonResult("请选择地区！");
            }
            if (record.ProjectId == 0)
            {
                if (string.IsNullOrEmpty(projectName))
                {
                    return ErrorJsonResult("请选择项目或者输入项目名称！");
                }
                if (Core.ProjectManager.Exist(projectName))
                {
                    return ErrorJsonResult("当前输入的项目名称已存在，请核对！");
                }
                var projectId = Core.ProjectManager.Add(new Models.Project.Project { Name = projectName, UserId = record.UserId });
                if (projectId <= 0)
                {
                    return ErrorJsonResult("录入输入项目名称失败！");
                }
                else
                {
                    record.ProjectId = projectId;
                }
            }
            if (record.ID > 0)
            {
                if (!Core.RecordManager.Edit(record))
                {
                    return ErrorJsonResult("编辑使用登记失败！");
                }
                Core.RecordManager.UpdateTablets(tabletIds.Select(e => new TabletRecord { TabletId = e, RecordId = record.ID }).ToList(), record.ID);
            }
            else
            {
                if (tabletIds == null)
                {
                    return ErrorJsonResult("请勾选平板！");
                }
                record.TabletRecords = tabletIds.Select(e => new TabletRecord { TabletId = e }).ToList();
                var id = Core.RecordManager.Add(record);
                if (id <= 0)
                {
                    return ErrorJsonResult("添加使用登记失败！");
                }
            }

            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.RecordManager.Delete(id))
            {
                return ErrorJsonResult("删除使用登记失败！");
            }
            return SuccessJsonResult();
        }

        public ActionResult Detail(int id)
        {
            var record = Core.RecordManager.Get(id);
            ViewBag.Model = record;
            return View();
        }

        public ActionResult File(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveFile(int recordId)
        {
            if (Request.Files.Count == 0)
            {
                throw new ArgumentException("请指定上传文件！");
            }
            var file = HttpContext.Request.Files[0];
            var fileName = file.FileName;
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("请上传有效的文件");
            }
            var info = FilePost.Upload(file, "Records", Identity.UserId, true);
            if (info == null)
            {
                throw new ArgumentException("上传文件失败！");
            }
            var fileId = Core.FileInfoManager.Add(info);
            if (fileId <= 0)
            {
                throw new Exception("保存文件记录失败");
            }
            if (!Core.RecordManager.UpdateFile(recordId, fileId))
            {
                throw new Exception("使用登记文件更新失败！");
            }
            return RedirectToAction("Detail", new { id = recordId });
        }

        public ActionResult Revert(int id)
        {
            var record = Core.RecordManager.Get(id);
            ViewBag.Model = record;
            ViewBag.Leaves = record.TabletRecords == null ?null: record.TabletRecords.Where(e => e.Tablet.Delete == false && e.RevertId == null).ToList();

            return View();
        }

       

       
    }
}