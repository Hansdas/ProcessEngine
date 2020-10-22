using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProcessEngine.Application.IService;
using ProcessEngine.Application.IService.WorkFlow;
using ProcessEngine.Domain.WorkFlow;
using ProcessEngine.Repository.Interface.WorkFlow;
using ProcessEngine.Web.Models;

namespace ProcessEngine.Web.Controllers
{  
    [ApiController]
    [Route("workflow")]
    public class WorkFlowController : Controller
    {
        private IWorkFlowRepository _workFlowRepository;
        private IWorkFlowService _workFlowService;
        private IWorkFlowNodeService _workFlowNodeService;
        public WorkFlowController(IWorkFlowRepository workFlowRepository, IWorkFlowService workFlowService, IWorkFlowNodeService workFlowNodeService)
        {
            _workFlowRepository = workFlowRepository;
            _workFlowService = workFlowService;
            _workFlowNodeService = workFlowNodeService;
        }
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }
        [Route("list")]
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Diagram()
        {
            return View();
        }
        [Route("edit")]
        public IActionResult Edit()
        {
            return View();
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("list/page")]
        public JsonResult PageList()
        {
            int currentPage = Convert.ToInt32(Request.Form["currentPage"]);
            int pageSize = Convert.ToInt32(Request.Form["pageSize"]);
            int count = _workFlowRepository.SelectCount();
            IEnumerable<WorkFlow> workFlows = _workFlowRepository.SelectByPage(currentPage, pageSize);
            return Json(JsonReturn.Success(new { total = count, data = workFlows }));
        }
         /// <summary>
         /// 删除
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(string id)
        {
            JsonReturn jsonReturn = null;
            try
            {
                _workFlowService.Delete(id);
                jsonReturn = new JsonReturn("0");
            }
            catch (Exception ex)
            {
                jsonReturn = new JsonReturn("1", ex.Message);
            }
            return Json(jsonReturn);
        }
         /// <summary>
         /// 流程图
         /// </summary>
         /// <param name="workFlowId"></param>
         /// <returns></returns>
        [HttpGet]
        [Route("workflow/getDiagram/{workFlowId}")]
        public IActionResult GetDiagram(string workFlowId)
        {
            JsonReturn jsonReturn = null;
            //try
            //{
            //    Tuple<string, string> processFlow = _workFlowNodeService.GetProcessFlow(workFlowId);
            //    jsonReturn = new JsonReturn("0", new { processFlow.Item1, processFlow.Item2 });
            //}
            //catch (Exception ex)
            //{
            //    jsonReturn = new JsonReturn("1", ex.Message);
            //}
            return Json(jsonReturn);
        }
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="workFlow"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public IActionResult CreateFlow([FromBody]WorkFlow workFlow)
        {
            workFlow=_workFlowService.Create(workFlow);
            return Json(JsonReturn.Success(workFlow.Id));
        }
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="workFlow"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("save/diagram")]
        public JsonResult SaveDiagram()
        {
            JsonReturn jsonReturn = null;
            string diagramJson = Request.Form["diagramJson"];
            string id = Request.Form["id"];
            _workFlowService.SaveDiagram(diagramJson, id);
            return Json(jsonReturn);
        }
    }
}