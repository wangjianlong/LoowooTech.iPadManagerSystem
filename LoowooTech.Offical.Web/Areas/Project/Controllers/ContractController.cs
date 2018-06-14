using LoowooTech.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Project.Controllers
{
    public class ContractController : ProjectControllerBase
    {
        // GET: Project/Contract
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create(int projectId)
        {
            var companys = Core.CompanyManager.GetAll();
            ViewBag.Companys = companys;
            ViewBag.ProjectId = projectId;
            return View();
        }

        [HttpPost]
        public ActionResult Save(Contract contract,string[] content,double[] money2)
        {
            if (content == null || money2 == null || content.Length != money2.Length || content.Length == 0)
            {
                return ErrorJsonResult("合同支付方式未填写，请填写");
            }
            if (Math.Abs(money2.Sum() - contract.Money) > 0.01)
            {
                return ErrorJsonResult("支付方式合计金额与合同金额不一致，请核对！");
            }
            
            if (contract.ID > 0)
            {
                if (!Core.ContractManager.Edit(contract))
                {
                    return ErrorJsonResult("编辑合同信息失败！");
                }
            }
            else
            {
                var id = Core.ContractManager.Add(contract);
                if (id <= 0)
                {
                    return ErrorJsonResult("保存合同信息失败");
                }
            }
            var pays = PayInfo.Tranlate(contract.ID, content, money2);
            if (pays.Count > 0)
            {
                Core.ContractManager.AddPayInfo(pays, contract.ID);
            }
            return SuccessJsonResult(contract.ProjectId);
        }

        public ActionResult Detail(int projectId)
        {

            return View();
        }

        public ActionResult DetailList(int projectId)
        {
            var list = Core.ContractManager.GetList(projectId);
            ViewBag.List = list;
            ViewBag.ProjectId = projectId;
            return View();
        }
    }
}