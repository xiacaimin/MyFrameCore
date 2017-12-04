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
    public class Sys_RoleBLL : BaseBLL
    {
        ISys_RoleDAL dal = new Sys_RoleDAL();

        /// <summary>
        /// 角色管理分页
        /// </summary>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <returns></returns>
        public PageModel<sys_role> RolePage(int PageIndex, int PageSize, sys_role model)
        {
            return dal.RolePage(PageIndex, PageSize, model);
        }

        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="MenuIds">角色所有菜单ID</param>
        /// <param name="ButtonIds">角色所有菜单按钮ID</param>
        /// <param name="RoleId">角色ID</param>
        /// <returns></returns>
        public bool SaveRole(string MenuIds, string ButtonIds, string RoleId)
        {
            if (!string.IsNullOrEmpty(MenuIds) && !string.IsNullOrEmpty(RoleId))
            {
                var MenuArray = MenuIds.Trim(',').Split(',');
                ButtonIds = ButtonIds ?? "";
                var ButtonArray = ButtonIds.Trim(',').Split(',');
                #region 将数据对象化
                List<sys_rolemenu> RoleMenuList = new List<sys_rolemenu>();
                List<sys_rolemenubutton> RoleMenuButtonList = new List<sys_rolemenubutton>();
                //向【角色菜单】表添加数据
                foreach (var item in MenuArray)
                {
                    if (!string.IsNullOrEmpty(item))
                    {

                        RoleMenuList.Add(new sys_rolemenu
                        {
                            KeyId = Guid.NewGuid().ToString(),
                            MenuId = item,
                            RoleId = RoleId,
                            CreateDate = DateTime.Now
                        });
                    }
                }
                //向【角色菜单按钮】表添加数据
                foreach (var item in ButtonArray)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        RoleMenuButtonList.Add(new sys_rolemenubutton
                        {
                            KeyId = Guid.NewGuid().ToString(),
                            MenuId = item.Split('|')[1],
                            RoleId = RoleId,
                            ButtonId = item.Split('|')[0],
                            CreateDate = DateTime.Now
                        });
                    }
                }
                #endregion
                return dal.SaveRole(RoleMenuList, RoleMenuButtonList, RoleId);
            }
            return false;
        }
    }
}
