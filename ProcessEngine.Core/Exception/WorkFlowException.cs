using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Core
{
   public class WorkFlowException:Exception
    {
        public WorkFlowException()
        {

        }
        public WorkFlowException(string message) : base(message)
        {

        }
        public WorkFlowException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
