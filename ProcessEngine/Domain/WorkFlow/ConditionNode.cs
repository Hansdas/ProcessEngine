using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Domain.WorkFlow
{
    /// <summary>
    /// 条件节点
    /// </summary>
   public class ConditionNode
    {
        /// <summary>
        /// 判断条件结果
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        ///  条件后续节点
        /// </summary>
        public string NextNodeId { get; set; }
    }
}
