using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProcessEngine.Application.IService;
using ProcessEngine.Application.IService.WorkFlow;
using ProcessEngine.Domain.WokrFlow;
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
        public IActionResult Start()
        {
            return View();
        }
        public IActionResult FlowList()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateFlow([FromBody]WorkFlow workFlow)
        {
            JsonReturn jsonReturn = null;
            try
            {              
                _workFlowService.Create(workFlow);
                jsonReturn = new JsonReturn("0","",workFlow.Id);
            }
            catch (Exception ex)
            {
                jsonReturn = new JsonReturn("1",ex.Message);
            }
            return Json(jsonReturn);
        }
        [HttpPost]
        public IActionResult AddFlowStep([FromBody]WorkFlowNode workFlowNode)
        {
            JsonReturn jsonReturn = null;
            try
            {
                ProcessFlow process = _workFlowNodeService.AddFlowStep(workFlowNode);
                jsonReturn = new JsonReturn("0", process);
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
        [HttpPost]
        public IActionResult GetList()
        {
            int currentPage = Convert.ToInt32(Request.Form["currentPage"]);
            int pageSize = Convert.ToInt32(Request.Form["pageSize"]);
            int count = _workFlowRepository.SelectCount();
            IEnumerable<WorkFlow> workFlows = _workFlowRepository.SelectByPage(currentPage,pageSize);
            return Json(new JsonReturn("0", new { total=count,data=workFlows}));
        }
    }
}