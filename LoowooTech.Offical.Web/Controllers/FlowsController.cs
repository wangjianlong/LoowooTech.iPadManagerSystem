using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Controllers
{
    public class FlowsController : ControllerBase
    {
        // GET: Flows
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int flowId, int infoId)
        {
            var data = Core.FlowDataManager.Get(flowId, infoId);
            ViewBag.Data = data;
            return View();
        }

        /// <summary>
        /// 作用：用户自己提交审核
        /// </summary>
        /// <param name="flowDataId"></param>
        /// <returns></returns>
        public ActionResult Commit(int flowDataId)
        {
            var data = Core.FlowDataManager.Get(flowDataId);
            if (data == null)
            {
                return ErrorJsonResult("表单审核信息缺失，请告知网站负责人！");
            }
            var node = data.Flow.GetFirstNode();
            if (node == null)
            {
                return ErrorJsonResult("当前表单流程未配置！无法进行审核！");
            }
            var id = Core.FlowNodeDataManager.Add(new Models.Admin.FlowNodeData { FlowNodeId = node.ID, FlowDataId = data.ID, UserIdValues=node.UserIdValues });
            if (id <= 0)
            {
                return ErrorJsonResult("创建流程信息节点失败！");
            }
            if (!Core.FlowDataManager.Change(flowDataId, FlowDataState.Checking))
            {
                return ErrorJsonResult("更新审核单状态失败");
            }
            return SuccessJsonResult();
        }

        /// <summary>
        /// 作用：审核人员提交审核
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(FlowNodeData data)
        {
            if (!Core.FlowNodeDataManager.Verification(data,Identity.UserId))
            {
                return ErrorJsonResult("录入审核意见信息失败！");
            }
            var next = data.state==VerificationState.Success?
                Core.FlowNodeManager.GetNext(data.FlowNodeId) ://审核通过 查询下一个流程节点
                Core.FlowNodeManager.GetPrev(data.FlowNodeId);//审核不通过 获取上一个节点;
            if (next == null)
            {
                if (!Core.FlowDataManager.Change(data.FlowDataId,data.state==VerificationState.Success?FlowDataState.Done:FlowDataState.None))
                {
                    return ErrorJsonResult("设置审批流程完成失败，请重试！");
                }
            }
            else
            {
                var id = Core.FlowNodeDataManager.Add(new FlowNodeData { FlowNodeId = next.ID, FlowDataId = data.FlowDataId, UserIdValues=next.UserIdValues });
                if (id <= 0)
                {
                    return ErrorJsonResult("创建下一审批环节信息失败！");
                }
            }

            return SuccessJsonResult();
        }
        /// <summary>
        /// 作用：用户撤销提交审核
        /// </summary>
        /// <param name="flowDataId"></param>
        /// <param name="OwnId"></param>
        /// <returns></returns>
        public ActionResult Cancel(int flowDataId,int OwnId)
        {
            var flowData = Core.FlowDataManager.Get(flowDataId);
            if (flowData == null)
            {
                return ErrorJsonResult("参数错误！");
            }
            if (flowData.Flow.CanBack == false)
            {
                return ErrorJsonResult("当前流程不能撤回操作！");
            }
            var lastTwo = flowData.GetSkipNode(1);//
            if (lastTwo == null)
            {
                if (Identity.UserId == OwnId)
                {
                    if (!Core.FlowNodeDataManager.Cancel(flowData, Identity.UserId))
                    {
                        return ErrorJsonResult("撤销失败");
                    }
                    return SuccessJsonResult();
                }
                return ErrorJsonResult("未获取相关撤回消息");
            }
            if (lastTwo.UserId != Identity.UserId)
            {
                return ErrorJsonResult("当前您不能进行撤销操作！");
            }
            if (!Core.FlowNodeDataManager.Cancel(flowData, lastTwo))
            {
                return ErrorJsonResult("撤销失败！");
            }
            return SuccessJsonResult();
        }
    }
}