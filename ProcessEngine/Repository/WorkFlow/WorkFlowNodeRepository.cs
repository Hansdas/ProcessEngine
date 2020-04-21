using Microsoft.EntityFrameworkCore;
using ProcessEngine.Domain.WorkFlow;
using ProcessEngine.EF;
using ProcessEngine.Maps;
using ProcessEngine.Repository.Interface.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ProcessEngine.Repository.WorkFlow
{
   public class WorkFlowNodeRepository: BaseRepository<WorkFlowNode, string>, IWorkFlowNodeRepository
    {
        public WorkFlowNodeRepository(DBContext dBContext) : base(dBContext)
        {

        }
        /// <summary>
        /// 查询部分列
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="selector">结果列</param>
        /// <returns></returns>
        public IList<WorkFlowNode> SelectWithCol(Expression<Func<WorkFlowNode, bool>> where = null, Expression<Func<WorkFlowNode, WorkFlowNode>> selector=null)
        {
            IQueryable<WorkFlowNode> result = GetQueryable(where).Select(selector);
            return result.ToList();
        }

        public void BatchUpdate(IList<WorkFlowNode> workFlowNodes)
        {
            for(int i = 0; i < workFlowNodes.Count; i++)
            {
                _context.Entry(workFlowNodes[i]).State = EntityState.Modified;
                _context.Entry(workFlowNodes[i]).Property(s => s.Id).IsModified = false;
            }         
            _context.SaveChanges();
        }
    }
}
