using Newtonsoft.Json;
using ProcessEngine.Application.IService;
using ProcessEngine.Core.AOP;
using ProcessEngine.Core.AOP.Transaction;
using ProcessEngine.Domain.WorkFlow;
using ProcessEngine.Repository.Interface.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessEngine.Application.Service
{
    public class WorkFlowService : IWorkFlowService, IInterceptorHandler
    {
        private IWorkFlowRepository _workFlowRepository;
        private IWorkFlowNodeRepository _workFlowNodeRepository;
        public WorkFlowService(IWorkFlowRepository workFlowRepository, IWorkFlowNodeRepository workFlowNodeRepository)
        {
            _workFlowRepository = workFlowRepository;
            _workFlowNodeRepository = workFlowNodeRepository;
        }
        public Domain.WorkFlow.WorkFlow Create(Domain.WorkFlow.WorkFlow workFlow)
        {
            int version= _workFlowRepository.SelectCount(s => s.Name == workFlow.Name)+1;
            workFlow.Id = Guid.NewGuid().ToString();
            workFlow.CreateTime = DateTime.Now;
            workFlow.Version = version;
            workFlow = _workFlowRepository.Insert(workFlow);
            return workFlow;
        }
        [Transaction(TransactionLevel.ReadCommitted)]
        public void Delete(string id)
        {
            _workFlowRepository.DeleteById(id);
            _workFlowNodeRepository.Delete(s => s.WorkFlowId == id);
        }
		private IList<WorkFlowNode> workFlowNodeList = new List<WorkFlowNode>();
		public IList<WorkFlowNode> GetByWorkFlow(Domain.WorkFlow.WorkFlow workFlow)
		{
			return _workFlowNodeRepository.Select(s => s.WorkFlowId == workFlow.Id).ToList();
		}

        public void SaveDiagram(string diagramJson,string workFlowId)
        {

            IList<WorkFlowNode> workFlowNodes= _workFlowNodeRepository.Select(s => s.WorkFlowId == workFlowId).ToList();
            IEnumerable<string> ids = workFlowNodes.Select(s => s.Id);
            dynamic d = JsonConvert.DeserializeObject(diagramJson);
            foreach (var item in ids)
            {
                dynamic connection = d[item];
                string style = string.Format("color:#0e76a8;left:{0}px;top:{1}px", connection["left"], connection["top"]);
                var array = connection["process_to"];
                WorkFlowNode workFlowNode = workFlowNodes.First(s => s.Id == item);
                workFlowNode.Style = style;
                foreach(var toId in array)
                {
                    WorkFlowNode toNode = workFlowNodes.First(s => s.Id == (string)toId);
                    //如果直接使用 toNode.previousNodeList.add()会出现Collection was of a fixed size异常
                    List<string> previousIds = new List<string>();
                    previousIds.AddRange(toNode.PreviousNodeList);
                    if(!toNode.PreviousNodeList.Contains(item))
                        previousIds.Add(item);
                    toNode.PreviousNodeList = previousIds.Distinct().ToList();
                }

            }
            _workFlowNodeRepository.BatchUpdate(workFlowNodes);
        }
    }
}
