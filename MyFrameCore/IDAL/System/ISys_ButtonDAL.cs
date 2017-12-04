using MyFrameCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.IDAL
{
    public interface ISys_ButtonDAL
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        PageModel<sys_button> ButtonPage(int PageIndex, int PageSize, sys_button model);
    }
}
