using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessEngine.Domain.WokrFlow
{
   public class ProcessFlow
    {
        /// <summary>
        /// 创建连接线js 
        /// </summary>
        public string JsText { get; set; }
        /// <summary>
        /// 流程图html
        /// </summary>
        public string DivHtml { get; set; }
        public static Tuple<int,int> GetCoordinate(IEnumerable<WorkFlowNode> workFlowNodes,string id,int left,int top)
        {
            string previousId = workFlowNodes.First(s => s.Id == id).PreviousNode;
            IList<string> idList = workFlowNodes.Where(s => s.PreviousNode == previousId).Select(s=>s.Id).ToList();
            int index = idList.IndexOf(id);
            left = left + 50;
            top = index * 50;
            return new Tuple<int, int>(left,top);
        }
    }
}
