using MyFrameCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.IDAL
{
    public interface ISys_MenuDAL
    {
        /// <summary>
        /// 获取根菜单
        /// </summary>
        /// <param name="UserId">登陆者ID</param>
        /// <returns></returns>
        List<sys_menu> GetRootMenuList(string UserId);

        /// <summary>
        /// 根据根目录菜单ID获取左侧导航菜单
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        List<sys_menu> GetNavigateUrl(string UserId);
        /// <summary>
        /// 菜单表分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="model">菜单条件对象</param>
        /// <returns></returns>
        PageModel<sys_menu> MenuPage(int PageIndex, int PageSize, sys_menu model);
        /// <summary>
        /// 保存菜单按钮
        /// </summary>
        /// <param name="MBList">菜单按钮集合</param>
        /// <returns></returns>
        bool SaveMenuButton(List<sys_menubutton> MBList);
        /// <summary>
        /// 错误日志分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        PageModel<sys_errorLog> ErrorPage(int PageIndex, int PageSize);
    }
}
