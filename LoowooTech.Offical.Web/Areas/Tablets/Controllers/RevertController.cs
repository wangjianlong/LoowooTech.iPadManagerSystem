using LoowooTech.Models.Tablets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Tablets.Controllers
{
    public class RevertController : TabletsControllerBase
    {
        // GET: Tablets/Revert
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Save(Revert revert, int[] TabletIds, int recordId)
        {
            if (TabletIds == null || TabletIds.Length <= 0)
            {
                return ErrorJsonResult("请选择归还平板");
            }
            if (string.IsNullOrEmpty(revert.Operator))
            {
                return ErrorJsonResult("经办人不能为空");
            }
            var id = Core.RecordManager.Add(revert);
            if (id <= 0)
            {
                return ErrorJsonResult("保存归还信息失败！");
            }
            Core.RecordManager.Revert(recordId, TabletIds, id);
            return SuccessJsonResult();
        }

        public ActionResult Detail(int id)
        {
            var revert = Core.RecordManager.GetRevert(id);
            ViewBag.Revert = revert;
            return View();
        }
    }
}