using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Application.IService
{
   public interface IWorkFlowService
    {
        Domain.WorkFlow.WorkFlow  Create(Domain.WorkFlow.WorkFlow workFlow);
        void Delete(string id);
        void SaveDiagram(string diagramJson, string workFlowId);
    }
}
