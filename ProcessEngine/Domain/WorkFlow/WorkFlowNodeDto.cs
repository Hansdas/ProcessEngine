using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Domain.WorkFlow
{
   public class WorkFlowNodeDto
    {
        public string Id { get; set; }
        public string Flow_id { get; set; }
        public string Process_name { get; set; }
        public string Process_to { get; set; }
        public string Icon { get; set; }
        public string Style { get; set; }
    }
}
