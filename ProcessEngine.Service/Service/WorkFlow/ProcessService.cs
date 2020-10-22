using ProcessEngine.Core;
using ProcessEngine.Core.AOP.Transaction;
using ProcessEngine.Domain;
using ProcessEngine.Domain.WorkFlow;
using ProcessEngine.Repository.Interface.WorkFlow;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ProcessEngine.Application.Service.WorkFlow
{
    public class ProcessService
    {
        private static ConcurrentDictionary<string, Type> classTypes = new ConcurrentDictionary<string, Type>();
        private static ConcurrentDictionary<string, IList<MethodInfo>> methodInfos = new ConcurrentDictionary<string, IList<MethodInfo>>();
        private static ConcurrentDictionary<string, object> classInstances = new ConcurrentDictionary<string, object>();

        private IWorkFlowStepRepository _workFlowStepRepository;
        private IWorkFlowNodeRepository _workFlowNodeRepository;
        private IWorkFlowRepository _workFlowRepository;
        public ProcessService(IWorkFlowStepRepository workFlowStepRepository, IWorkFlowRepository workFlowRepository, IWorkFlowNodeRepository workFlowNodeRepository)
        {
            _workFlowStepRepository = workFlowStepRepository;
            _workFlowNodeRepository = workFlowNodeRepository;
            _workFlowRepository = workFlowRepository;
        }
        /// <summary>
        /// 启动流程
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="id"></param>
        /// <param name="getDomain"></param>
        /// <param name="onProcessStart"></param>
        [Transaction(TransactionLevel.ReadCommitted)]
        public void StartProcess(string domainName,string id,Func<string, Entity<string>> getDomain, Action onProcessStart=null)
        {
            Domain.WorkFlow.WorkFlow workFlow = _workFlowRepository.SelectSingle(s => s.DomainName == domainName, s => s.Version);
            IEnumerable<WorkFlowNode> workFlowNodes = _workFlowNodeRepository.Select(s => s.WorkFlowId == workFlow.Id);
            WorkFlowNode workFlowNode = workFlowNodes.FirstOrDefault(s => s.Type == WorkFlowNodeType.开始);           
            WorkFlowStep workFlowStep = new WorkFlowStep();
            workFlowStep.DomainName = domainName;
            workFlowStep.DomainId = id;
            workFlowStep.WorkFlowNodeId = workFlowNode.Id;
            workFlowStep.StepName = workFlowNode.NodeName;
            _workFlowStepRepository.Insert(workFlowStep);
            onProcessStart.Invoke();
            Entity<string> domain = getDomain(id) ;
        }
        /// <summary>
        /// 添加下一步处理流程
        /// </summary>
        /// <param name="workFlow"></param>
        /// <param name="workFlowNodes"></param>
        public void AddNextStep(Domain.WorkFlow.WorkFlow workFlow, IEnumerable<WorkFlowNode> workFlowNodes,string nowNodeId, string domainName,string domainId, Entity<string> domain)
        {
            IList<WorkFlowNode> nextNodes = workFlowNodes.Where(s => s.PreviousNodeList.Contains(nowNodeId)).ToList();
            if (nextNodes.Count >1)
            {
                IList<WorkFlowNode> conditionNodes = nextNodes.Where(s => s.Type == WorkFlowNodeType.判断).ToList();
                if (conditionNodes.Count == 1)
                    throw new WorkFlowException("只获取到一个判断节点");
                if (conditionNodes.Count > 0)//后续节点是否有判断节点，如果有则处理判断逻辑
                {
                    foreach(var item in conditionNodes)
                    {
                        Assembly assembly = Assembly.Load("");

                        string nodeProperty = item.NodeProperty;
                        string[] arr = nodeProperty.Split("=");
                        string[] arr1 = arr[0].Split('.');
                        string className = arr1[0];
                        bool flag = JudgeFlowStep(domainName, arr1, className);
                        if (flag == Convert.ToBoolean(arr[1]))
                        {
                            WorkFlowStep workFlowStep = new WorkFlowStep();
                            workFlowStep.DomainName = domainName;
                            workFlowStep.DomainId = domainId;
                            workFlowStep.WorkFlowNodeId = item.Id;
                            workFlowStep.StepName = item.NodeName;
                            _workFlowStepRepository.Insert(workFlowStep);
                            break;
                        }
                    }
                }
                else//多个后续节点且没有判断节点
                {
                    IList<WorkFlowStep> workFlowSteps = new List<WorkFlowStep>();
                   foreach(var item in nextNodes)
                    {
                        WorkFlowStep workFlowStep = GetNextStep(domainName, domainId, domain, item);
                        workFlowSteps.Add(workFlowStep);
                    }
                    _workFlowStepRepository.BatchInsert(workFlowSteps);
                }
            }
            else if(nextNodes.Count==1)
            {
                WorkFlowStep workFlowStep = GetNextStep(domainName, domainId, domain, nextNodes[0]);
                _workFlowStepRepository.Insert(workFlowStep);
            }
        }

        /// <summary>
        /// 获取下一步处理人数据
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="domainId"></param>
        /// <param name="domain"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private WorkFlowStep GetNextStep(string domainName, string domainId, Entity<string> domain, WorkFlowNode item)
        {
            bool resolve = true;
            string nodeProperty = item.NodeProperty;
            int actor = 0;
            resolve = int.TryParse(nodeProperty, out actor);
            if (nodeProperty.Contains("domain"))
            {
                string[] arr = nodeProperty.Split('.');
                PropertyInfo property = domain.GetType().GetProperties().FirstOrDefault(s => s.Name == arr[1]);
                var value = property.GetType().GetProperty(arr[1]).GetValue(property);
                resolve = int.TryParse(value.ToString(), out actor);
            }
            else
            {
                PropertyInfo property = domain.GetType().GetProperties().FirstOrDefault(s => s.Name == "Id");
                var value = property.GetType().GetProperty("Id").GetValue(property);
                string[] arr = nodeProperty.Split('.');
                MethodInfo method;
                object obj;
                GetInstance(arr[1], arr[0], out method, out obj);
                var returnValue = method.Invoke(obj, new object[] { value });
                resolve = int.TryParse(value.ToString(), out actor);
            }
            if (!resolve)
                throw new WorkFlowException("节点" + item.Id + "获取流程处理人失败");
            WorkFlowStep workFlowStep = new WorkFlowStep();
            workFlowStep.DomainName = domainName;
            workFlowStep.DomainId = domainId;
            workFlowStep.WorkFlowNodeId = item.Id;
            workFlowStep.StepName = item.NodeName;
            workFlowStep.Actor = actor.ToString();
            return workFlowStep;
        }

        /// <summary>
        /// 处理判断节点
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="arr1"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        private  bool JudgeFlowStep(string domainName, string[] arr1, string className)
        {
            MethodInfo method;
            object obj;
            GetInstance(arr1[1], className, out method, out obj);
            bool flag = (bool)method.Invoke(obj, new object[] { Type.GetType(domainName) });
            return flag;
        }

        private  void GetInstance(string methodName, string className, out MethodInfo method, out object obj)
        {
            Type type = null;
            object instance = null;
            method = null;
            if (!classTypes.ContainsKey(className))
            {
                type = Type.GetType(className);
                classTypes.GetOrAdd(className, type);
            }
            else
                type = classTypes[className];

            if (!classInstances.ContainsKey(className))
            {
                instance = Activator.CreateInstance(type);
                classInstances.GetOrAdd(className, instance);
            }
            else
                instance = classInstances[className];

            if (methodInfos.ContainsKey(className))
            {
                IList<MethodInfo> methods = methodInfos[className];
                if (methods.Count(s => s.Name == methodName) == 0)
                {
                    method = type.GetMethod(methodName);
                    methods.Add(method);
                    methodInfos.AddOrUpdate(className, methods, (key, value) => methods);
                }
                else
                    method = methods.FirstOrDefault(s => s.Name == methodName);
            }
            else
            {
                method = type.GetMethod(methodName);
                methodInfos.GetOrAdd(className, s => new List<MethodInfo>()).Add(method);
            }
            obj = Activator.CreateInstance(type);
        }
    }
}
