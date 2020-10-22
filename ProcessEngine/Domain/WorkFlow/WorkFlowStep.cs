using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Domain.WorkFlow
{
   public class WorkFlowStep:Entity<string>
    {
        public WorkFlowStep()
        {
            Id = Guid.NewGuid().ToString();
            CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 流程id
        /// </summary>
        public string WorkFlowId { get; set; }
        /// <summary>
        /// 流程节点id
        /// </summary>
        public string WorkFlowNodeId { get; set; }
        /// <summary>
        /// 关联实体
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 关联id
        /// </summary>
        public string DomainId { get; set; }
        /// <summary>
        /// 当前步骤处理人
        /// </summary>
        public string Actor { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? ActorTime { get; set; }
        /// <summary>
        /// 是否已处理
        /// </summary>
        public bool IsActor { get
            {
                return ActorTime.HasValue;
            }
        }
        /// <summary>
        /// 处理意见
        /// </summary>
        public string ActorComment { get; set; }
        /// <summary>
        /// 步骤名称
        /// </summary>
        public string StepName { get; set; }
    }
}
