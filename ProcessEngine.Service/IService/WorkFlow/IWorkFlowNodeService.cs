using ProcessEngine.Domain.WokrFlow;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Application.IService.WorkFlow
{
   public interface IWorkFlowNodeService
    {
        /// <summary>
        /// 创建步骤
        /// </summary>
        /// <param name="workFlowNode"></param>
        /// <returns></returns>
        ProcessFlow AddFlowStep(WorkFlowNode workFlowNode);
    }
}
