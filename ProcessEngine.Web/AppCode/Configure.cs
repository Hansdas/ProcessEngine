using Autofac;
using Microsoft.Extensions.DependencyInjection;
using ProcessEngine.Application.IService;
using ProcessEngine.Application.IService.WorkFlow;
using ProcessEngine.Application.Service.WorkFlow;
using ProcessEngine.Core.AOP;
using ProcessEngine.Repository;
using ProcessEngine.Repository.Interface;
using ProcessEngine.Repository.Interface.WorkFlow;
using ProcessEngine.Repository.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using ProcessEngine.Core.AOP.Transaction;
using ProcessEngine.Application.Service;

namespace ProcessEngine.Web.AppCode
{
    public static class Configure
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<,>),typeof(BaseRepository<,>));
            services.AddTransient<ITransactionInterceptor, TransactionInterceptor>();

            services.AddTransient<IWorkFlowRepository, WorkFlowRepository>();
            services.AddTransient<IWorkFlowService, WorkFlowService>();

            services.AddTransient<IWorkFlowNodeRepository, WorkFlowNodeRepository>();
            services.AddTransient<IWorkFlowNodeService, WorkFlowNodeService>();
        }
        /// <summary>
        /// 3.0不支持返回IServiceProvider
        /// </summary>
        /// <param name="containerBuilder"></param>
        public static void GetAutofacServiceProvider(this ContainerBuilder containerBuilder)
        {
            var assembly = Assembly.Load("ProcessEngine.Application");
            containerBuilder.RegisterType<Interceptor>();
            containerBuilder.RegisterAssemblyTypes(assembly)
                         .Where(type => typeof(IInterceptorHandler).IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract)
                         .AsImplementedInterfaces()
                         .InstancePerLifetimeScope()
                         .EnableInterfaceInterceptors()
                         .InterceptedBy(typeof(Interceptor));
        }
    }
}
