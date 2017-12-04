using MyFrameCore.IDAL;
using MyFrameCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.DAL
{
    public class Sys_ButtonDAL : BaseDAL, ISys_ButtonDAL
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public PageModel<sys_button> ButtonPage(int PageIndex, int PageSize, sys_button model)
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                int totalNumber = 0;
                var list = db.Queryable<sys_button>()
                    .Where(item => item.IsDeleted == false)
                    .WhereIF(!string.IsNullOrEmpty(model.FullName), item => item.FullName.Contains(model.FullName))
                    .OrderBy(item => item.SortNum)
                    .ToPageList(PageIndex, PageSize, ref totalNumber);
                return GetPageInfo<sys_button>(PageIndex, PageSize, list, totalNumber);
            }
        }
    }
}
