using MyFrameCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.IDAL
{
    public interface ISys_DictionaryDAL
    {
        /// <summary>
        /// 获取字典配置
        /// </summary>
        /// <param name="TypeName">配置类型名称</param>
        /// <returns></returns>
        List<sys_dictionary> GetConfigList(string TypeName);
    }
}
