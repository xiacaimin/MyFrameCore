using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.Common
{
    public class ImgUploadModel
    {
        /// <summary>
        /// 字节
        /// </summary>
        public byte[] Byte { get; set; }
        /// <summary>
        /// 子目录
        /// 例子:主题/自然/花
        /// </summary>
        public string ChiPath { get; set; }
        /// <summary>
        /// 文件名称(不包含后缀)
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 后缀
        /// </summary>
        public string Postfix { get; set; }

    }
}
