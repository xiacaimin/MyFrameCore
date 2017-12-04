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
    public class Sys_ButtonBLL : BaseBLL
    {
        ISys_ButtonDAL dal = new Sys_ButtonDAL();
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public PageModel<sys_button> ButtonPage(int PageIndex, int PageSize, sys_button model)
        {
            return dal.ButtonPage(PageIndex, PageSize, model);
        }
    }
}
