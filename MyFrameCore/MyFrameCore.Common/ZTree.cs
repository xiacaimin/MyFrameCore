using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.Common
{
    public class ZTree
    {
        /// <summary>
        /// 节点id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 父节点id
        /// </summary>
        public string pId { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool open { get; set; }
        /// <summary>
        /// 是否强制为父节点(默认false)
        /// </summary>
        public bool isParent { get; set; }
    }
}
