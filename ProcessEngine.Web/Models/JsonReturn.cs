using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessEngine.Web.Models
{
    public class JsonReturn
    {

        public JsonReturn(string code)
        {
            Code = code;
        }
        public JsonReturn(string code,string message)
        {
            Code = code;
            Message = message;
        }
        public JsonReturn(string code, dynamic data)
        {
            Code = code;
            Data = data;
        }
        public JsonReturn(string code,string message, dynamic data)
        {
            Code = code;
            Data = data;
            Message = message;
        }
        /// <summary>
        /// 返回编码
        /// </summary>
        public string Code { get; private set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public dynamic Data { get; private set; }
    }
}
