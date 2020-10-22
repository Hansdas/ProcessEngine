using ProcessEngine.Domain.WorkFlow;
using ProcessEngine.EF;
using ProcessEngine.Repository.Interface.WorkFlow;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Repository.WorkFlow
{
   public class WorkFlowStepRepository:BaseRepository<WorkFlowStep,string>,IWorkFlowStepRepository
    {
        public WorkFlowStepRepository(DBContext dBContext) : base(dBContext)
        {

        }
        public void BatchInsert(IEnumerable<WorkFlowStep> list)
        {
            _context.Set<WorkFlowStep>().AddRange(list);
            _context.SaveChanges();
        }
    }
}
