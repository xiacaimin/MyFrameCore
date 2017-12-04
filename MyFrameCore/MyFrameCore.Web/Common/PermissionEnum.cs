using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFrameCore.Web
{
    /// <summary>
    /// 权限执行方式枚举
    /// </summary>
    public enum PermissionEnum
    {
        /// <summary>
        /// 执行
        /// </summary>
        Enforce,
        /// <summary>
        /// 忽略
        /// </summary>
        Ignore
    }
}