using ProcessEngine.Domain.WorkFlow;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Repository.Interface.WorkFlow
{
   public interface IWorkFlowStepRepository: IBaseRepository<WorkFlowStep, string>
    {
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="list"></param>
        void BatchInsert(IEnumerable<WorkFlowStep> list);
    }
}
