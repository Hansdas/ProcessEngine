using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProcessEngine.Application.IService;
using ProcessEngine.Application.IService.WorkFlow;
using ProcessEngine.Core;
using ProcessEngine.Domain;
using ProcessEngine.Domain.WorkFlow;
using ProcessEngine.Repository.Interface.WorkFlow;
using ProcessEngine.Web.Models;

namespace ProcessEngine.Web.Controler.FlowDesign
{
    public class FlowDesignController :Controller
    {
        private IWorkFlowNodeService _workFlowNodeService;
        private IWorkFlowService _workFlowService;
        private IWorkFlowNodeRepository _workFlowNodeRepository;
        private IWorkFlowRepository _workFlowRepository;
        public FlowDesignController(IWorkFlowNodeService workFlowNodeService, IWorkFlowService workFlowService
            , IWorkFlowNodeRepository workFlowNodeRepository, IWorkFlowRepository workFlowRepository)
        {
            _workFlowNodeService = workFlowNodeService;
            _workFlowService = workFlowService;
            _workFlowNodeRepository = workFlowNodeRepository;
            _workFlowRepository = workFlowRepository;
        }
        public IActionResult Design()
        {
            return View();
        }
        [HttpGet]
        [Route("flowdesign/nodetype-pre")]
        public IActionResult GetNodeTypeControl()
        {            
            return Json(new JsonReturn("0", typeof(WorkFlowNodeType).ToKeyValueList()));
        }
        //[HttpGet]
        //[Route("flowdesign/pre-control/{workFlowId}")]
        //public JsonResult GetPreviousNodes(string workFlowId)
        //{
        //    IList<WorkFlowNode> workFlowNodes = _workFlowNodeService.GetPreviousControl(workFlowId);
        //    return Json(new JsonReturn("0", workFlowNodes)); 
        //}
        [HttpGet]
        [Route("workflow/{id}")]
        public IActionResult GetWorkFlow(string id)
        {
           
            JsonReturn jsonReturn = null;
            try
            {
                IList<WorkFlowNodeDto> dtos = _workFlowNodeService.GetFlow(id);
                jsonReturn = new JsonReturn("0", "", new { total = dtos.Count, list = dtos }); ;
            }
            catch (Exception ex)
            {
                jsonReturn = new JsonReturn("1", ex.Message);
            }
            return Json(jsonReturn);

        }
        [HttpGet]
        [Route("flowdesign/workflownode/{nodeId}")]
        public IActionResult GetWorkFlowNode(string nodeId)
        {

            JsonReturn jsonReturn = null;
            try
            {
                WorkFlowNode workFlowNode = _workFlowNodeRepository.SelectSingle(s => s.Id == nodeId);
                jsonReturn = new JsonReturn("0", workFlowNode) ;
            }
            catch (Exception ex)
            {
                jsonReturn = new JsonReturn("1", ex.Message);
            }
            return Json(jsonReturn);

        }
        [HttpPost]
        public JsonResult AddFlowStep()
        {
            JsonReturn jsonReturn = null;
            string nodeJson= Request.Form["nodeJson"];
            bool update =Convert.ToBoolean(Request.Form["update"]);
            WorkFlowNode workFlowNode = JsonConvert.DeserializeObject<WorkFlowNode>(nodeJson);
            try
            {
                IList<WorkFlowNodeDto> dtos = _workFlowNodeService.AddOrUpdateStep(workFlowNode, update);
                jsonReturn = new JsonReturn("0", "", new { total = dtos.Count, list = dtos }); ;
            }
            catch (Exception ex)
            {
                jsonReturn = new JsonReturn("1", ex.Message);
            }
            return Json(jsonReturn);
        }
        [HttpPost]
        public IActionResult GetFlowCount([FromBody]WorkFlowNodeCondition workFlowNodeCondition)
        {
         
            JsonReturn jsonReturn = null;
            try
            {
                var condition = new WorkFlowNodeCondition(workFlowNodeCondition).CreateCondition();
                int count = _workFlowNodeRepository.SelectCount(condition);
                jsonReturn = new JsonReturn("0", count);
            }
            catch (Exception ex)
            {
                jsonReturn = new JsonReturn("1", ex.Message);
            }
            return Json(jsonReturn);
        }
          
    }
}