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
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static JsonReturn Success(dynamic data=null)
        {
            return new JsonReturn("0", "", data);
        }
        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static JsonReturn Error(string msg)
        {
            return new JsonReturn("1",msg);
        }
    }
}
