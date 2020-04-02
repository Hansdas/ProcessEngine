using ProcessEngine.Application.IService;
using ProcessEngine.Repository.Interface.WorkFlow;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Application.Service.WorkFlow
{
    public class WorkFlowService : IWorkFlowService
    {
        private IWorkFlowRepository _workFlowRepository;
        public WorkFlowService(IWorkFlowRepository workFlowRepository)
        {
            _workFlowRepository = workFlowRepository;
        }
        public Domain.WokrFlow.WorkFlow Create(Domain.WokrFlow.WorkFlow workFlow)
        {
            int version= _workFlowRepository.SelectCount(s => s.Name == workFlow.Name)+1;
            workFlow.Id = Guid.NewGuid().ToString();
            workFlow.CreateTime = DateTime.Now;
            workFlow.Version = version;
            workFlow = _workFlowRepository.Insert(workFlow);
            return workFlow;
        }
    }
}
