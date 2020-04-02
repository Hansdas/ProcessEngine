using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Domain
{
    /// <summary>
    /// 流程步骤操作类型
    /// </summary>
   public enum WorkFlowNodeType
    {
        开始=1,
        判断=2,
        单人处理=3,
        多人并行处理=4,
        分发=5,
        结束=6
    }
}
