using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.Model
{
    public class PageModel<T>
    {
        /// <summary>
        /// 当前页数据信息
        /// </summary>
        public object rows { get; set; }
        /// <summary>
        /// 每页最多显示数据量
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 总数据量
        /// </summary>
        public int totalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int totalPage { get; set; }
    }
}
