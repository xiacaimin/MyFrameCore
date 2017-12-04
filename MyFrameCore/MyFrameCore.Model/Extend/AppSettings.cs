using System;
using System.Collections.Generic;
using System.Text;

namespace MyFrameCore.Model
{
    /// <summary>
    /// 配置管理
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 接口网络路径
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// 图片网络路径
        /// </summary>
        public string ImgUrl { get; set; }
        /// <summary>
        /// 图片根目录
        /// </summary>
        public string MyFrameImgRoot { get; set; }
        /// <summary>
        /// Redis相关配置
        /// </summary>
        public RedisModel Redis { get; set; }
    }
    /// <summary>
    /// redis
    /// </summary>
    public class RedisModel
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int? Port { get; set; }
        /// <summary>
        /// 最大并非数
        /// </summary>
        public int? PoolSize { get; set; }
        /// <summary>
        /// 登陆者XX分钟过期
        /// </summary>
        public int? LoginTimeOutMin { get; set; }
    }
}
