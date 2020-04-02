using Microsoft.Extensions.DependencyInjection;
using ProcessEngine.Application.IService;
using ProcessEngine.Application.IService.WorkFlow;
using ProcessEngine.Application.Service.WorkFlow;
using ProcessEngine.Repository;
using ProcessEngine.Repository.Interface;
using ProcessEngine.Repository.Interface.WorkFlow;
using ProcessEngine.Repository.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessEngine.Web.AppCode
{
    public static class Configure
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<,>),typeof(BaseRepository<,>));

            services.AddTransient<IWorkFlowRepository, WorkFlowRepository>();
            services.AddTransient<IWorkFlowService, WorkFlowService>();

            services.AddTransient<IWorkFlowNodeRepository, WorkFlowNodeRepository>();
            services.AddTransient<IWorkFlowNodeService, WorkFlowNodeService>();
        }
    }
}
