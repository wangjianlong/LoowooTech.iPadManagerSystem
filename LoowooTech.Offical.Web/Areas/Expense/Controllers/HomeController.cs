﻿using LoowooTech.Models;
using LoowooTech.Models.Admin;
using LoowooTech.Models.Expense;
using LoowooTech.Offical.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Expense.Controllers
{
    public class HomeController : ExpenseControllerBase
    {
        // GET: Expense/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int id = 0)
        {
            ViewBag.Sheet = Core.SheetManager.GetSingle(id);

            var companys = Core.UserCompanyManager.GetCompanys(Identity.UserId);
            ViewBag.Companys = companys;
            ViewBag.Projects = Core.ProjectManager.Search(new Models.Project.ProjectParameter { IsDone = false, Delete = false });
            return View();
        }

        [HttpPost]
        public ActionResult Save(Sheet sheet, string projectName)
        {
            if (sheet.UserId == 0 || sheet.CompanyId == 0)
            {
                return ErrorJsonResult("未获取报销人信息或者报销单位信息");
            }
            if (sheet.ProjectId == 0)
            {
                if (string.IsNullOrEmpty(projectName))
                {
                    return ErrorJsonResult("请选择相关项目");
                }
                else
                {
                    if (Core.ProjectManager.Exist(projectName))
                    {
                        return ErrorJsonResult("当前输入项目名称已经存在，请核对！");
                    }
                    var projectId = Core.ProjectManager.Add(new Models.Project.Project { Name = projectName, UserId = sheet.UserId });
                    if (projectId <= 0)
                    {
                        return ErrorJsonResult("录入手工输入项目失败！");
                    }
                    else
                    {
                        sheet.ProjectId = projectId;
                    }
                }
            }
            if (sheet.ID > 0)
            {
                if (!Core.SheetManager.Edit(sheet))
                {
                    return ErrorJsonResult("编辑报销基础信息失败！");
                }
            }
            else
            {
                var flowDataId = Core.FlowDataManager.Add(new FlowData { FlowId = Flow.ID });
                if (flowDataId <= 0)
                {
                    return ErrorJsonResult("创建审批流程记录失败！");
                }
                sheet.FlowDataId = flowDataId;
                var id = Core.SheetManager.Add(sheet);
                if (id <= 0)
                {
                    return ErrorJsonResult("创建报销基础信息失败");
                }
            }
            return SuccessJsonResult(sheet.ID);
        }

        public ActionResult Delete(int id)
        {
            if (!Core.SheetManager.Delete(id))
            {
                return ErrorJsonResult("删除失败！未找到相关基础报销信息！");
            }
            return SuccessJsonResult();
        }

        public ActionResult Navigation(int id)
        {
            var sheet = Core.SheetManager.Get(id);
            if (sheet != null)
            {
                switch (sheet.SheetType)
                {
                    case SheetType.Evection:
                        return Redirect("/Expense/Evection/Create?sheetId=" + id);
                    case SheetType.Daily:
                        return Redirect("/Expense/Daily/Create?sheetId=" + id);
                    case SheetType.Reception:
                        return Redirect("/Expense/Reception/Create?sheetId=" + id);
                }
            }
            return View("Empty");
        }
        public ActionResult NavigationDetial(int id)
        {
            var sheet = Core.SheetManager.Get(id);
            if (sheet != null)
            {
                switch (sheet.SheetType)
                {
                    case SheetType.Evection:
                        return Redirect("/Expense/Evection/Detail?sheetId=" + id);
                    case SheetType.Daily:
                        return Redirect("/Expense/Daily/Detail?sheetId=" + id);
                    case SheetType.Reception:
                        return Redirect("/Expense/Reception/Detail?sheetId=" + id);
                }
            }
            return View("Empty");
        }

        public ActionResult Examination(
            int? companyId = null, int? sheetUserId = null, int? flowNodeId = null, int page = 1, int rows = 20)
        {

            var companys = Core.UserCompanyManager.GetCompanys(Identity.UserId);
            var parameter = new SheetViewParameter
            {
                CheckUserId = Identity.UserId,
                CompanyId = companyId,
                SheetUserId = sheetUserId,
                FLowNodeId = flowNodeId,
                State = VerificationState.None,
                Page = new PageParameter(page, rows)
            };
            if (companys != null)
            {
                parameter.CompanyIds = companys.Select(e => e.ID).ToArray();
            }
            #region 历史
            //var sheets = Core.SheetViewManager.Search(parameter);
            //ViewBag.Sheets = sheets;
            #endregion

            var sheets = Core.SheetViewManager.Search2(parameter);
            ViewBag.Sheets = sheets;

            ViewBag.Parameter = parameter;
            
            ViewBag.Companys = companys;
            var users = Core.UserManager.GetList();
            ViewBag.Users = users;
            ViewBag.FlowNodes = Core.FlowNodeManager.GetList2(Flow.ID);
            return View();
        }




        /// <summary>
        /// 报销确认款项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Check(int id, bool flag)
        {
            if (!Core.SheetManager.IsCheck(id, flag))
            {
                return ErrorJsonResult("确认款项失败！");
            }
            return SuccessJsonResult();
        }

        public ActionResult Head(SheetType type)
        {
            var parameter = new SheetViewParameter
            {
                SheetUserId = Identity.UserId,
                SheetType = type,
                Page = new PageParameter(1, 20)
            };
            var list = Core.SheetViewManager.Search(parameter);
            ViewBag.List = list;
            ViewBag.Parameter = parameter;
            return View();
        }

        public ActionResult State(int userId, FlowDataState state, bool? isCheck, bool? Delete, int? page = null, int rows = 20)
        {
            #region 历史
            //var parameter = new SheetFlowDataParameter
            //{
            //    UserId = userId,
            //    State = state,
            //    IsCheck = isCheck,
            //    Delete=Delete
            //};
            //if (page.HasValue)
            //{
            //    parameter.Page = new PageParameter(page.Value, rows);
            //}
            //var list = Core.SheetFlowDataViewManager.Search(parameter);
            #endregion

            var parameter = new SheetParameter
            {
                UserId = userId,
                State = state,
                IsCheck = isCheck,
                Delete = Delete
            };
            if (page.HasValue)
            {
                parameter.Page = new PageParameter(page.Value, rows);
            }
            var list = Core.SheetManager.Search(parameter);

            ViewBag.List = list;
            return View();
        }


        public ActionResult Files(int sheetId)
        {
            var sheet = Core.SheetManager.Get(sheetId);
            ViewBag.Sheet = sheet;
            var files = Core.SheetManager.GetFiles(sheetId);
            ViewBag.List = files;
            return View();
        }

        public ActionResult Upload(int sheetId)
        {

            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(int sheetId)
        {
            if (Request.Files.Count == 0)
            {
                throw new ArgumentException("请选择上传电子发票文件");
            }
            var file = HttpContext.Request.Files[0];
            var fileName = file.FileName;
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("请选择上传电子发票文件");
            }
            var info = FilePost.Upload(file, "Invoice", Identity.UserId);
            if (info == null)
            {
                throw new ArgumentException("上传文件失败！");
            }
            var fileId = Core.FileInfoManager.Add(info);
            if (fileId <= 0)
            {
                throw new ArgumentException("保存文件信息失败！");
            }
            Core.SheetManager.Add(new SheetFile
            {
                SheetId = sheetId,
                FileId = fileId
            });
            return RedirectToAction("NavigationDetial", new { Id = sheetId });
        }

        public ActionResult DeleteFile(int id)
        {
            if (!Core.SheetManager.DeleteFile(id))
            {
                return ErrorJsonResult("删除失败！");
            }
            return SuccessJsonResult();
        }


        public ActionResult StatisticHead()
        {
            var times = Core.SheetViewManager.GetTimes2();
            ViewBag.Times2 = times;
            var lwtimes = Core.SheetViewManager.GetTimes();
            ViewBag.Times = lwtimes;
            return View();
        }

        public ActionResult MySelfStatistic()
        {

            return View();
        }

        public ActionResult StatisticDay()
        {
            var dict = Core.SheetViewManager.GetTimeDict();
            ViewBag.Dict = dict;
            return View();
        }


        public ActionResult Statistic(int year, int month, int? userId = null, int? day = null)
        {

            var parameter = new SheetViewParameter
            {
                Year = year,
                Month = month,
                Day = day,
                FinialUserId = userId,
                State = VerificationState.Success,
                FlowDataState = FlowDataState.Done
            };
            var list = Core.SheetViewManager.Search(parameter);
            list = list.GroupBy(e => e.SheetId).Select(e => e.FirstOrDefault()).ToList();
            ViewBag.List = list;

            return View();
        }

        public ActionResult Statistic2(int year, int month, int? userId, int? day = null)
        {
            var parameter = new SheetViewParameter
            {
                Year = year,
                Month = month,
                Day = day,
                FinialUserId = userId,
                State = VerificationState.Success,
                FlowDataState = FlowDataState.Done
            };

            var list = Core.SheetViewManager.Search2(parameter);
            list = list.GroupBy(e => e.SheetId).Select(e => e.FirstOrDefault()).ToList();
            ViewBag.List = list;
            return View();
        }


        public ActionResult DetailList(int projectId)
        {
            List<Sheet> list = null;
            if (Identity.UserRole >= UserRole.Advance)
            {
                list = Core.SheetManager.Getlist(projectId, null);
            }
            else
            {
                list = Core.SheetManager.Getlist(projectId, Identity.UserId);
            }
            ViewBag.List = list;                                                                                                                                                                  
            return View();
        }

    }
}