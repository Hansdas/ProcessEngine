using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Application.IService
{
   public interface IWorkFlowService
    {
        Domain.WokrFlow.WorkFlow Create(Domain.WokrFlow.WorkFlow workFlow);
    }
}
