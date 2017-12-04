using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.Common
{
    /// <summary>
    /// 执行结果
    /// </summary>
    public class ExecuteResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回值
        /// </summary>
        public object ReturnVal { get; set; }
    }
}
