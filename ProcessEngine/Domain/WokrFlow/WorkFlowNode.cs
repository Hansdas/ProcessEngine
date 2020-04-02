using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Domain.WokrFlow
{
    /// <summary>
    /// 流程步骤
    /// </summary>
   public class WorkFlowNode:Entity<string>
    {
        /// <summary>
        ///   WorkFlow实体id
        /// </summary>
        public string WorkFlowId { get; set; }
        /// <summary>
        /// 流程节点名字
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 流程节点类型
        /// </summary>
        public WorkFlowNodeType WorkFlowNodeType { get; set; } 
        /// <summary>
        /// 前置节点
        /// </summary>
        public string PreviousNode { get; set; }
        /// <summary>
        /// 节点属性
        /// </summary>
        public string NodeProperty { get; set; }
        /// <summary>
        /// 后一节点
        /// </summary>
        public string NextNode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
