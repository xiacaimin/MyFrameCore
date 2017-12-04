using MyFrameCore.IDAL;
using MyFrameCore.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.DAL
{
    public class Sys_RoleDAL : BaseDAL, ISys_RoleDAL
    {
        /// <summary>
        /// 角色管理分页
        /// </summary>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <returns></returns>
        public PageModel<sys_role> RolePage(int PageIndex, int PageSize, sys_role model)
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                int totalNumber = 0;
                var list = db.Queryable<sys_role>().Where(a => a.IsDeleted == false)
                .WhereIF(!string.IsNullOrEmpty(model.FullName), a => a.FullName.Contains(model.FullName))
                .ToPageList(PageIndex, PageSize, ref totalNumber);
                return GetPageInfo<sys_role>(PageIndex, PageSize, list, totalNumber);
            }
        }

        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="RoleMenuList">当前角色菜单数据</param>
        /// <param name="RoleMenuButtonList">当前角色菜单按钮数据</param>
        /// <param name="RoleId">角色ID</param>
        /// <returns></returns>
        public bool SaveRole(List<sys_rolemenu> RoleMenuList, List<sys_rolemenubutton> RoleMenuButtonList, string RoleId)
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                try
                {
                    db.Ado.BeginTran();
                    //先删除该角色以前的权限
                    db.Deleteable<sys_rolemenubutton>(item => item.RoleId == RoleId).ExecuteCommand();
                    db.Deleteable<sys_rolemenu>(item => item.RoleId == RoleId).ExecuteCommand();
                    //重新添加权限
                    db.Insertable(RoleMenuList.ToArray()).ExecuteCommand();
                    db.Insertable(RoleMenuButtonList.ToArray()).ExecuteCommand();
                    db.Ado.CommitTran();
                }
                catch (Exception ex)
                {
                    db.Ado.RollbackTran();
                    return false;
                }
            }
            return true;
        }
    }


}
