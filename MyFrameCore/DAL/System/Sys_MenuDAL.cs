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
    public class Sys_MenuDAL : BaseDAL, ISys_MenuDAL
    {
        /// <summary>
        /// 获取根菜单
        /// </summary>
        /// <param name="UserId">登陆者ID</param>
        /// <returns></returns>
        public List<sys_menu> GetRootMenuList(string UserId)
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                return db.Queryable<sys_userrole, sys_role, sys_rolemenu, sys_menu>((a, b, c, d) =>
                    new object[] {
                        JoinType.Inner,a.RoleId==b.KeyId,
                        JoinType.Inner,c.RoleId==a.RoleId,
                        JoinType.Inner,d.KeyId==c.MenuId
                    })
                    .Where((a, b, c, d) => a.UserId == UserId && d.IsDeleted == false && d.IsRoot == true)
                    .Select((a, b, c, d) => new sys_menu()
                    {
                        KeyId = d.KeyId,
                        CreateDate = d.CreateDate,
                        Description = d.Description,
                        FullName = d.FullName,
                        Icon = d.Icon,
                        IsDeleted = d.IsDeleted,
                        IsRoot = d.IsRoot,
                        NavigateUrl = d.NavigateUrl,
                        ParentId = d.ParentId,
                        SortNum = d.SortNum
                    }).OrderBy("SortNum").ToList();
            }
        }

        /// <summary>
        /// 根据根目录菜单ID获取左侧导航菜单
        /// </summary>
        /// <param name="UserId">登录ID</param>
        /// <returns></returns>
        public List<sys_menu> GetNavigateUrl(string UserId)
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                return db.Queryable<sys_userrole, sys_role, sys_rolemenu, sys_menu>((a, b, c, d) =>
                    new object[] {
                        JoinType.Inner,a.RoleId==b.KeyId,
                        JoinType.Inner,c.RoleId==a.RoleId,
                        JoinType.Inner,d.KeyId==c.MenuId
                    })
                    .Where((a, b, c, d) => a.UserId == UserId && d.IsDeleted == false && d.IsRoot == false)
                    .Select((a, b, c, d) => new sys_menu()
                    {
                        KeyId = d.KeyId,
                        CreateDate = d.CreateDate,
                        Description = d.Description,
                        FullName = d.FullName,
                        Icon = d.Icon,
                        IsDeleted = d.IsDeleted,
                        IsRoot = d.IsRoot,
                        NavigateUrl = d.NavigateUrl,
                        ParentId = d.ParentId,
                        SortNum = d.SortNum
                    }).OrderBy("SortNum").ToList();
            }
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
            using (var db = SqlSugarDB.MasterDB)
            {
                int totalNumber = 0;
                var list = db.Queryable<sys_menu>()
                    .Where(item => item.IsDeleted == false)
                    .WhereIF(!string.IsNullOrEmpty(model.FullName) && !string.IsNullOrEmpty(model.NavigateUrl), item => item.FullName.Contains(model.FullName) || item.NavigateUrl.Contains(model.NavigateUrl))
                    .WhereIF(!string.IsNullOrEmpty(model.ParentId), item => item.ParentId == model.ParentId)
                    .OrderBy(item => item.SortNum)
                    .ToPageList(PageIndex, PageSize, ref totalNumber);
                return GetPageInfo<sys_menu>(PageIndex, PageSize, list, totalNumber);
            }
        }

        /// <summary>
        /// 错误日志分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public PageModel<sys_errorLog> ErrorPage(int PageIndex, int PageSize)
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                int totalNumber = 0;
                var list = db.Queryable<sys_errorLog>()
                    .OrderBy(item => item.CreateTime, OrderByType.Desc)
                    .ToPageList(PageIndex, PageSize, ref totalNumber);
                return GetPageInfo<sys_errorLog>(PageIndex, PageSize, list, totalNumber);
            }
        }
        /// <summary>
        /// 保存菜单按钮
        /// </summary>
        /// <param name="MBList">菜单按钮集合</param>
        /// <returns></returns>
        public bool SaveMenuButton(List<sys_menubutton> MBList)
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                string MenuId = MBList[0].MenuId;
                try
                {
                    db.Ado.BeginTran();
                    //先删除旧数据
                    db.Deleteable<sys_menubutton>(item => item.MenuId == MenuId).ExecuteCommand();
                    //再新增新数据
                    db.Insertable(MBList).ExecuteCommand();
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
