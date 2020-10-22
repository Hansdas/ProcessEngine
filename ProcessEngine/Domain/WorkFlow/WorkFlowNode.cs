using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ProcessEngine.Domain.WorkFlow
{
    /// <summary>
    /// 流程活动基类
    /// </summary>
    public class WorkFlowNode : Entity<string>
    {
        public WorkFlowNode()
        {
            Id = Guid.NewGuid().ToString();
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 所属流程Id
        /// </summary>
        public string WorkFlowId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 流程处理属性
        /// </summary>
        public string NodeProperty { get; set; }

        /// <summary>
        /// 流程活动名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 流程操作步骤名称
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// 流程活动类型
        /// </summary>
        public WorkFlowNodeType Type { get; set; }
        /// <summary>
        /// 流程活动前置处理操作Id集合（数据库持久化）
        /// </summary>
        public string PreviousNodeIds
        {
            get
            {
                return PreviousNodeList == null ? "" : string.Join(',', PreviousNodeList);
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    PreviousNodeList = new List<string>();
                else
                    PreviousNodeList = value.Split(',');
            }
        }
        /// <summary>
        /// 流程活动前置处理操作Id集合
        /// </summary>
        [NotMapped]
        public IList<string> PreviousNodeList { get; set; }
        /// <summary>
        /// 前置节点是否需要全部结束
        /// </summary>
        public bool PreviousAllFinish { get; set; }
        /// <summary>
        /// 退回节点集合（数据库持久化）
        /// </summary>
        public string ReturnIds { get; set; }
        /// <summary>
        /// 退回节点集合
        /// </summary>
        [NotMapped]
        public IList<string> ReturnList
        {
            get
            {
                if (string.IsNullOrEmpty(ReturnIds))
                    return new List<string>();
                return ReturnIds.Split(',');
            }
            set
            {
                ReturnIds = string.Join(',', value);
            }
        }
        /// <summary>
        /// 节点样式
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        ///	判断条件后续节点
        /// </summary>
        [NotMapped]
        public IList<ConditionNode> conditionNodeList
        {
            get
            {
                IList<ConditionNode> nodeList = new List<ConditionNode>();
                XElement xml = XElement.Parse(ConditionXml);
                IList<XElement> xElements = xml.Descendants("Action").ToList();
                foreach(var item in xElements)
                {
                    ConditionNode conditionNode = new ConditionNode();
                    conditionNode.Result = Convert.ToBoolean(item.Attribute("result").Value);
                    conditionNode.NextNodeId = item.Attribute("next").Value;
                    nodeList.Add(conditionNode);
                }
                return nodeList;
            }
            set
            {
                XElement x = new XElement("Condition");
                foreach(var item in conditionNodeList)
                {
                    XElement xAction = new XElement("Action");
                    xAction.SetAttributeValue("result", item.Result);
                    xAction.SetAttributeValue("next", item.NextNodeId);
                    x.Add(xAction);
                }
                ConditionXml = x.ToString();
            }
        }
        public string ConditionXml { get; set; }
    }
}
