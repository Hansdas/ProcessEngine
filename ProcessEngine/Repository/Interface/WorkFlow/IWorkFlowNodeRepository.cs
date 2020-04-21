
using ProcessEngine.Domain.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ProcessEngine.Repository.Interface.WorkFlow
{
  public  interface IWorkFlowNodeRepository: IBaseRepository<WorkFlowNode, string>
    {
        /// <summary>
        /// 查询部分列
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="cols">结果列</param>
        /// <returns></returns>
        IList<WorkFlowNode> SelectWithCol(Expression<Func<WorkFlowNode, bool>> where = null, Expression<Func<WorkFlowNode, WorkFlowNode>> selector = null);
        void BatchUpdate(IList<WorkFlowNode> workFlowNodes);
    }
}
