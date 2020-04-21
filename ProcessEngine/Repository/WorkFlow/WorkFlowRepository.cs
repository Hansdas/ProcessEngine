
using ProcessEngine.EF;
using ProcessEngine.Maps;
using ProcessEngine.Repository.Interface.WorkFlow;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Repository.WorkFlow
{
   public class WorkFlowRepository: BaseRepository<Domain.WorkFlow.WorkFlow,string>, IWorkFlowRepository
    {
        public WorkFlowRepository(DBContext dBContext) : base(dBContext)
        {

        }

      
    }
}
