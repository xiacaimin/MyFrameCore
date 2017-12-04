using MyFrameCore.IDAL;
using MyFrameCore.Model;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace MyFrameCore.DAL
{
    public class Sys_UserDAL : BaseDAL, ISys_UserDAL
    {
        /// <summary>
        /// 根据主键获取登陆者信息
        /// </summary>
        /// <param name="KeyId"></param>
        /// <returns></returns>
        public sys_user GetUserInfo(string KeyId)
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                var sdb = db.SimpleClient;
                return sdb.GetById<sys_user>(KeyId);
            }
        }
        /// <summary>
        /// 保存用户角色信息
        /// </summary>
        /// <param name="URList">用户角色集合</param>
        /// <returns></returns>
        public bool SaveUserRole(List<sys_userrole> URList)
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                string UserId = URList[0].UserId;
                try
                {
                    db.Ado.BeginTran();
                    //先删除旧数据
                    db.Deleteable<sys_userrole>(item => item.UserId == UserId).ExecuteCommand();
                    //再新增新数据
                    db.Insertable(URList).ExecuteCommand();
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

        /// <summary>
        /// 用户列表分页
        /// </summary>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <returns></returns>
        public PageModel<dynamic> UserPage(int PageIndex, int PageSize, sys_user model)
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                int totalNumber = 0;
                var list = db.Queryable<sys_user, sys_organization>((a, b) => new object[] { JoinType.Left, a.OrgId == b.KeyID })
                .Where(a => a.IsDeleted == false)
                .WhereIF(!string.IsNullOrEmpty(model.Account), a => a.FullName.Contains(model.Account))
                .WhereIF(!string.IsNullOrEmpty(model.FullName), a => a.FullName.Contains(model.FullName))
                .WhereIF(!string.IsNullOrEmpty(model.Email), a => a.FullName.Contains(model.Email))
                .WhereIF(!string.IsNullOrEmpty(model.Phone), a => a.FullName.Contains(model.Phone))
                .WhereIF(model.Sex != null, a => a.Sex == model.Sex)
                .Select((a, b) => new
                {
                    a.KeyId,
                    a.FullName,
                    a.Account,
                    a.BirthDay,
                    a.Address,
                    a.CreateDate,
                    a.Educational,
                    a.Email,
                    a.FinishSchool,
                    a.Sex,
                    a.Phone,
                    a.OrgId,
                    OrgName = b.FullName,
                    a.IDCard,
                    a.Job,
                    a.SortNum
                })
                .ToPageList(PageIndex, PageSize, ref totalNumber);
                return GetPageInfo<dynamic>(PageIndex, PageSize, list, totalNumber);
            }
        }

        /// <summary>
        /// 根据登录账户和密码获取登陆者信息
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        public sys_user GetUserInfo(string Account, string PassWord)
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                return db.Queryable<sys_user>().Where(item => item.Account == Account && item.PassWord == PassWord).Single();
            }
        }
        /// <summary>
        /// 该登陆者是否可以访问此菜单
        /// </summary>
        /// <param name="UserId">登陆者ID</param>
        /// /// <param name="NavigateUrl">导航菜单的链接地址</param>
        /// <returns></returns>
        public bool IsAllowedMenu(string UserId, string NavigateUrl)
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                return db.Queryable<sys_userrole, sys_role, sys_rolemenu, sys_menu>((a, b, c, d) =>
                    new object[] {
                        JoinType.Inner,a.RoleId==b.KeyId,
                        JoinType.Inner,c.RoleId==a.RoleId,
                        JoinType.Inner,d.KeyId==c.MenuId
                    }).Where((a, b, c, d) => a.UserId == UserId && d.NavigateUrl != null && d.NavigateUrl.ToLower() == NavigateUrl.ToLower() && d.IsDeleted == false).Count() > 0;
            }
        }
    }
}
