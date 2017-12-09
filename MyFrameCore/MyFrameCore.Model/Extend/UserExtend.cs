using System;
using System.Collections.Generic;
using System.Text;

namespace MyFrameCore.Model.Extend
{
    public class UserExtend : sys_user
    {
        /// <summary>
        /// 网站个人身份最后有效时间
        /// </summary>
        public DateTime? WebLastTime { get; set; }
        /// <summary>
        /// API个人身份最后有效时间
        /// </summary>
        public DateTime? ApiLastTime { get; set; }
        /// <summary>
        /// Api个人令牌
        /// </summary>
        public string ApiToken { get; set; }
    }
}
