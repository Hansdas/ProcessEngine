using ProcessEngine.Application.IService.WorkFlow;
using ProcessEngine.Core;
using ProcessEngine.Core.AOP;
using ProcessEngine.Core.AOP.Transaction;
using ProcessEngine.Domain;
using ProcessEngine.Domain.WorkFlow;
using ProcessEngine.Repository.Interface.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessEngine.Application.Service.WorkFlow
{
    public class WorkFlowNodeService : IWorkFlowNodeService, IInterceptorHandler
    {

        private IWorkFlowNodeRepository _workFlowNodeRepository;
        public WorkFlowNodeService(IWorkFlowNodeRepository workFlowNodeRepository)
        {
            _workFlowNodeRepository = workFlowNodeRepository;
        }
        public IList<WorkFlowNodeDto> AddOrUpdateStep(WorkFlowNode workFlowNode,bool update)
        {
            if (!update)
            {
                workFlowNode.Id = Guid.NewGuid().ToString();
                if (workFlowNode.Type == WorkFlowNodeType.开始)
                    workFlowNode.NodeName = "开始";
                else if (workFlowNode.Type == WorkFlowNodeType.结束)
                    workFlowNode.NodeName = "结束";
                _workFlowNodeRepository.Insert(workFlowNode);
            }
            else
                _workFlowNodeRepository.Update(workFlowNode);
            IList<WorkFlowNodeDto> workFlowNodes = GetFlow(workFlowNode.WorkFlowId);
            return workFlowNodes;
        }
       
        public IList<WorkFlowNodeDto> GetFlow(string workFlowId)
        {
            IList<WorkFlowNode> workFlowNodes = _workFlowNodeRepository.Select(s => s.WorkFlowId ==workFlowId).ToList();
            IList<WorkFlowNodeDto> workFlowNodeDtos = new List<WorkFlowNodeDto>();
            string defaultStyle = "color:#0e76a8;left:12px;top:60px;";
            foreach (var item in workFlowNodes)
            {
                WorkFlowNodeDto dto = new WorkFlowNodeDto();
                dto.Id = item.Id;
                dto.Flow_id = item.WorkFlowId;
                dto.Process_name = item.NodeName;
                dto.Process_to =string.Join(',', workFlowNodes.Where(s => s.PreviousNodeList.Contains(item.Id)).Select(s => s.Id).ToList());
                switch (item.Type)
                {
                    case WorkFlowNodeType.开始:
                        dto.Icon = "icon-ok";
                        dto.Style = string.IsNullOrEmpty(item.Style) ? defaultStyle : item.Style ;
                        break;
                    case WorkFlowNodeType.单人处理:
                        dto.Style = string.IsNullOrEmpty(item.Style) ? defaultStyle : item.Style;
                        break;
                    case WorkFlowNodeType.判断:
                        dto.Style = string.IsNullOrEmpty(item.Style) ? defaultStyle : item.Style;
                        break;
                    case WorkFlowNodeType.结束:
                        dto.Style = string.IsNullOrEmpty(item.Style) ? defaultStyle : item.Style;
                        break;
                }
                workFlowNodeDtos.Add(dto);
            }                
            return workFlowNodeDtos;

        }
        /// <summary>
        /// 生成前置节点（前端控件使用）
        /// </summary>
        /// <param name="workFlowId"></param>
        /// <returns></returns>
        public IList<ListItemControl> GetPreviousControl(string workFlowId)
        {
            IList<WorkFlowNode> workFlowNodes = _workFlowNodeRepository.
                SelectWithCol(s => s.WorkFlowId == workFlowId
                , p => new WorkFlowNode() { Id = p.Id,Type=p.Type, NodeName = p.NodeName }).ToList();
            IList<ListItemControl> listItems = new List<ListItemControl>();
            foreach(var item in workFlowNodes)
            {
                ListItemControl listItem = null;
                if (item.Type == WorkFlowNodeType.判断)
                {
                    foreach(var condition in item.conditionNodeList)
                    {
                        listItem = new ListItemControl();
                        listItem.Text = condition.Result == true ? "是" : "否";
                        listItem.Value = item.Id;
                        listItems.Add(listItem);
                    }
                }
                else
                {
                    listItem = new ListItemControl();
                    listItem.Text = item.NodeName;
                    listItem.Value = item.Id;
                    listItems.Add(listItem);
                }    
            }
            return listItems;
        }
    }
}
