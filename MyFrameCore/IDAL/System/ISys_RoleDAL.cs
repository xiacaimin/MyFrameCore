using MyFrameCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.IDAL
{
    public interface ISys_RoleDAL
    {
        /// <summary>
        /// 角色管理分页
        /// </summary>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <returns></returns>
        PageModel<sys_role> RolePage(int PageIndex, int PageSize, sys_role model);

        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="RoleMenuList">当前角色菜单数据</param>
        /// <param name="RoleMenuButtonList">当前角色菜单按钮数据</param>
        /// <param name="RoleId">角色ID</param>
        /// <returns></returns>
        bool SaveRole(List<sys_rolemenu> RoleMenuList, List<sys_rolemenubutton> RoleMenuButtonList, string RoleId);
    }
}
