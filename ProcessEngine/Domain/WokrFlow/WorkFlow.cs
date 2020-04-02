using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProcessEngine.Domain.WokrFlow
{
    /// <summary>
    /// 流程信息
    /// </summary>
    public class WorkFlow: Entity<string>
    {
        /// <summary>
        /// 流程名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 实体名称
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisable { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTimeDTO {
            get {
                return CreateTime.ToString("yyyy-MM-dd");
            }
        }
    }
}
