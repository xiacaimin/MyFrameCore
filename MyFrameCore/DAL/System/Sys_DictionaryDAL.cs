using MyFrameCore.IDAL;
using MyFrameCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.DAL
{
    public class Sys_DictionaryDAL : BaseDAL, ISys_DictionaryDAL
    {
        /// <summary>
        /// 获取字典配置
        /// </summary>
        /// <param name="TypeName">配置类型名称</param>
        /// <returns></returns>
        public List<sys_dictionary> GetConfigList(string TypeName)
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                var model = db.Queryable<sys_dictionary>().Where(item => item.IsDeleted == false && item.FullName.Equals(TypeName.Trim()) && item.ParentId == null).First();
                if (model != null)
                {
                    return db.Queryable<sys_dictionary>().Where(item => item.ParentId == model.KeyId).OrderBy(item=>item.SortNum).ToList();
                }
                return null;
            }
        }
    }
}
