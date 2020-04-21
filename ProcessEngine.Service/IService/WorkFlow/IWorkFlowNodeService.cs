
using ProcessEngine.Core;
using ProcessEngine.Domain.WorkFlow;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Application.IService.WorkFlow
{
   public interface IWorkFlowNodeService
    {
        /// <summary>
        /// 创建步骤返回流程图节点
        /// </summary>
        /// <param name="workFlowNode"></param>
        /// <returns></returns>
        IList<WorkFlowNodeDto> AddOrUpdateStep(WorkFlowNode workFlowNode,bool update);
        /// <summary>
        /// 生成前置节点（前端控件使用）
        /// </summary>
        /// <param name="workFlowId"></param>
        /// <returns></returns>

        IList<ListItemControl> GetPreviousControl(string workFlowId);
        IList<WorkFlowNodeDto> GetFlow(string workFlowId);
    }
}
