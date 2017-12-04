using MyFrameCore.DAL;
using MyFrameCore.IDAL;
using MyFrameCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.BLL
{
    public class Sys_DictionaryBLL : BaseBLL
    {
        ISys_DictionaryDAL dal = new Sys_DictionaryDAL();
        /// <summary>
        /// 获取字典配置
        /// </summary>
        /// <param name="TypeName">配置类型名称</param>
        /// <returns></returns>
        public List<sys_dictionary> GetConfigList(string TypeName)
        {
            if (string.IsNullOrEmpty(TypeName))
            {
                return null;
            }
            return dal.GetConfigList(TypeName);
        }
    }
}
