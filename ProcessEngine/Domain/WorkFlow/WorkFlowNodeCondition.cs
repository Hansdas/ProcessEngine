using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ProcessEngine.Domain.WorkFlow
{
  public  class WorkFlowNodeCondition
    {
        public WorkFlowNodeCondition()
        {

        }
        public WorkFlowNodeCondition(WorkFlowNodeCondition workFlowNodeCondition)
        {
            WorkFlowId = workFlowNodeCondition.WorkFlowId;
            NodeNameContain = workFlowNodeCondition.NodeNameContain;
        }
        public string WorkFlowId { get; set; }
        public string NodeNameContain { get; set; }
        public Expression<Func<WorkFlowNode,bool>> CreateCondition()
        {
            Expression<Func<WorkFlowNode, bool>> expressLeft = null;
            if (!string.IsNullOrEmpty(WorkFlowId))
            {
                 Expression<Func<WorkFlowNode, bool>> expressRight = s => s.WorkFlowId == WorkFlowId;
                Expression.Add(expressLeft, expressRight);
            }
            if (!string.IsNullOrEmpty(NodeNameContain))
            {
                Expression<Func<WorkFlowNode, bool>> expressRight = s => s.NodeName.Contains(NodeNameContain);
                Expression.Add(expressLeft, expressRight);
            }
            return expressLeft;



        }
    }
}
