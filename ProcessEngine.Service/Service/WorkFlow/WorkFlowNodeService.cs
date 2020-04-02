using ProcessEngine.Application.IService.WorkFlow;
using ProcessEngine.Domain;
using ProcessEngine.Domain.WokrFlow;
using ProcessEngine.Repository.Interface.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessEngine.Application.Service.WorkFlow
{
    public class WorkFlowNodeService : IWorkFlowNodeService
    {
        private IWorkFlowNodeRepository _workFlowNodeRepository;
        public WorkFlowNodeService(IWorkFlowNodeRepository workFlowNodeRepository)
        {
            _workFlowNodeRepository = workFlowNodeRepository;
        }

        public ProcessFlow AddFlowStep(WorkFlowNode workFlowNode)
        {
            workFlowNode.CreateTime = DateTime.Now;
            _workFlowNodeRepository.Insert(workFlowNode);
            StringBuilder jsText = new StringBuilder("" +
                "jsPlumb.ready(function () {\n" +
                " var flowConnector = {\n" +
                "            connector:\'Straight\',\n" +
                "            paintStyle: { lineWidth: 2, strokeStyle: '#61B7CF', fillStyle: 'transparent' },\n" +
                "            overlays: [['Arrow', { width: 10, length: 10, location: 1 }]],\n" +
                "            endpoint: ['Dot', { radius: 1 }]\n" +
                "        };\n'" +
                 "var returnConnector = {\n" +
               "    connector: 'Bezier',\n'=" +
               "   paintStyle: { lineWidth: 2, strokeStyle: 'red', fillStyle: 'transparent' },\n" +
                 "  overlays: [['Arrow', { width: 10, length: 10, location: 1 }]],\n" +
                   " endpoint: ['Dot', { radius: 1 }]\n" +
                "};\n"
                );
            StringBuilder divHtml = new StringBuilder();
            IEnumerable<WorkFlowNode> workFlowNodes = _workFlowNodeRepository.Select(s => s.WorkFlowId == workFlowNode.WorkFlowId);
            int i = 0;
            int top = 0;
            int left = 0;
            IList<Connection> connections = new List<Connection>();
            foreach (var item in workFlowNodes)
            {
                Connection connection = new Connection();
                switch (item.WorkFlowNodeType)
                {
                    case WorkFlowNodeType.开始:
                        Tuple<int, int> tuple = ProcessFlow.GetCoordinate(workFlowNodes, item.Id, left, top);
                        divHtml.Append("<div id='" + item.Id + "' class='start' style='left:" + left + "px;top:" + top + "px'>开始<i class='layui-icon layui-icon-upload-circle'></i></div>");
                        connection.SourceId = item.Id;
                        connection.TargetId = item.NextNode;
                        connection.OperationName = "通过";
                        connection.ConnectionId = item.Id;
                        connections.Add(connection);
                        break;
                    case WorkFlowNodeType.判断:
                        divHtml.Append("<div id='" + item.Id + "' class='start' style='left:" + left + "px;top:" + top + "px'>开始<i class='layui-icon layui-icon-upload-circle'></i></div>");
                        connection.SourceId = item.Id;
                        connection.TargetId = item.NextNode;
                        connection.OperationName = "通过";
                        connection.ConnectionId = item.Id;
                        connections.Add(connection);
                        break;
                    case WorkFlowNodeType.单人处理:
                        IList<WorkFlowNode> workFlowNodeList = workFlowNodes.Where(s => s.PreviousNode == item.Id).ToList();
                        divHtml.Append("<div id='" + item.Id + "' class='start' style='top:" + top + "px'>开始<i class='layui-icon layui-icon-upload-circle'></i></div>");
                        connection.SourceId = item.Id;
                        connection.TargetId = item.NextNode;
                        connection.OperationName = "通过";
                        connection.ConnectionId = item.Id;
                        connections.Add(connection);
                        break;
                }
                i++;
                left += 50;
                top += 50;
            }
            foreach (Connection connection in connections)
            {
                string js = "var " + connection.SourceId + "= jsPlumb.connect({\n" +
                 "            source: \'" + connection.SourceId + "\',\n" +
                 "            target: \'" + connection.TargetId + "\',\n" +
                 "           anchor: [\'Right\', \'Left\']\n" +
                 "        },flowConnector)\n" +
                 "        " + connection.SourceId + ".setLabel(`<span style=\'display:block;padding:10px;opacity: 0.8;filter: alpha(opacity=80);font-family: helvetica;height:auto;background-color:white;border:1px solid #346789;text-align:center;font-size:12px;color:black;border-radius:0.5em;\'>" + connection.OperationName + "</span>`);\n";
                jsText.Append(js);
            }
            jsText.Append("})");
            ProcessFlow process = new ProcessFlow();
            process.DivHtml = divHtml.ToString();
            process.JsText = jsText.ToString();
            return process;
        }
        private struct Connection
        {
            /// <summary>
            ///  ConnectionId
            /// </summary>
            public string ConnectionId { get; set; }
            /// <summary>
            /// 开始连接点
            /// </summary>
            public string SourceId { get; set; }
            /// <summary>
            /// 结束连接点
            /// </summary>
            public string TargetId { get; set; }
            /// <summary>
            /// 操作名称
            /// </summary>
            public string OperationName { get; set; }
            /// <summary>
            /// 是否退回
            /// </summary>
            public bool IsReturn { get; set; }
        }
        private void createJsPlumb(StringBuilder jsText, StringBuilder divHtml, IEnumerable<WorkFlowNode> workFlowNodes)
        {
        
        }

        private string getConnectionJs(string previousId, string id, string operationName)
        {
            string js = "var " + previousId + "= jsPlumb.connect({\n" +
                  "            source: \'" + previousId + "\',\n" +
                  "            target: \'" + id + "\',\n" +
                  "           anchor: [\'Right\', \'Left\']\n" +
                  "        },flowConnector)\n" +
                  "        " + previousId + ".setLabel(`<span style=\'display:block;padding:10px;opacity: 0.8;filter: alpha(opacity=80);font-family: helvetica;height:auto;background-color:white;border:1px solid #346789;text-align:center;font-size:12px;color:black;border-radius:0.5em;\'>" + operationName + "</span>`);\n";
            return js;

        }

    }
}
