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
    public class Sys_MenuBLL : BaseBLL
    {
        ISys_MenuDAL dal = new Sys_MenuDAL();

        /// <summary>
        /// 获取根菜单
        /// </summary>
        /// <param name="UserId">登陆者ID</param>
        /// <returns></returns>
        public List<sys_menu> GetRootMenuList(string UserId)
        {
            return dal.GetRootMenuList(UserId);
        }

        /// <summary>
        /// 根据根目录菜单ID获取左侧导航菜单
        /// </summary>
        /// <param name="UserId">登录ID</param>
        /// <returns></returns>
        public List<sys_menu> GetNavigateUrl(string UserId)
        {
            return dal.GetNavigateUrl(UserId);
        }

        /// <summary>
        /// 菜单表分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="model">菜单条件对象</param>
        /// <returns></returns>
        public PageModel<sys_menu> MenuPage(int PageIndex, int PageSize, sys_menu model)
        {
            return dal.MenuPage(PageIndex, PageSize, model);
        }

        /// <summary>
        /// 保存菜单按钮
        /// </summary>
        /// <param name="MenuId">菜单id</param>
        /// <param name="Ids">按钮id集合</param>
        /// <returns></returns>
        public bool SaveMenuButton(string MenuId, string Ids)
        {
            if (!string.IsNullOrEmpty(MenuId) && !string.IsNullOrEmpty(Ids))
            {
                List<sys_menubutton> list = new List<sys_menubutton>();
                var array = Ids.Trim(',').Split(',');
                foreach (var item in array)
                {
                    list.Add(new sys_menubutton
                    {
                        MenuId = MenuId,
                        ButtonId = item,
                        CreateDate = DateTime.Now,
                        KeyId = Guid.NewGuid().ToString()
                    });
                }
                return dal.SaveMenuButton(list);
            }
            return false;
        }

        /// <summary>
        /// 错误日志分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public PageModel<sys_errorLog> ErrorPage(int PageIndex, int PageSize)
        {
            return dal.ErrorPage(PageIndex, PageSize);
        }
    }
}
