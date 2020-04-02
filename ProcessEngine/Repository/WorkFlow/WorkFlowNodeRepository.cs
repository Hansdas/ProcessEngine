using ProcessEngine.Maps;
using ProcessEngine.Repository.Interface.WorkFlow;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Repository.WorkFlow
{
   public class WorkFlowNodeRepository: BaseRepository<Domain.WokrFlow.WorkFlowNode, string>, IWorkFlowNodeRepository
    {
        public WorkFlowNodeRepository(DBContext dBContext) : base(dBContext)
        {

        }
    }
}
