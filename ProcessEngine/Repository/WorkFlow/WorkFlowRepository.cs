using ProcessEngine.Domain.WokrFlow;
using ProcessEngine.Maps;
using ProcessEngine.Repository.Interface.WorkFlow;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Repository.WorkFlow
{
   public class WorkFlowRepository: BaseRepository<Domain.WokrFlow.WorkFlow,string>, IWorkFlowRepository
    {
        public WorkFlowRepository(DBContext dBContext) : base(dBContext)
        {

        }

      
    }
}
