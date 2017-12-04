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
    public class Sys_UserBLL : BaseBLL
    {
        ISys_UserDAL dal = new Sys_UserDAL();

        /// <summary>
        /// 根据主键获取登陆者信息
        /// </summary>
        /// <param name="KeyId"></param>
        /// <returns></returns>
        public sys_user GetUserInfo(string KeyId)
        {
            return dal.GetUserInfo(KeyId);
        }
        /// <summary>
        /// 保存用户角色信息
        /// </summary>
        /// <param name="RoleIds">角色ID数据集合</param>
        /// <param name="UserId">用户id</param>
        /// <returns></returns>
        public bool SaveUserRole(string RoleIds, string UserId)
        {
            if (!string.IsNullOrEmpty(RoleIds) && !string.IsNullOrEmpty(UserId))
            {
                List<sys_userrole> list = new List<sys_userrole>();
                var array = RoleIds.Trim(',').Split(',');
                foreach (var item in array)
                {
                    list.Add(new sys_userrole
                    {
                        UserId = UserId,
                        RoleId = item,
                        CreateDate = DateTime.Now,
                        KeyId = Guid.NewGuid().ToString()
                    });
                }
                return dal.SaveUserRole(list);
            }
            return false;
        }

        /// <summary>
        /// 用户列表分页
        /// </summary>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <returns></returns>
        public PageModel<dynamic> UserPage(int PageIndex, int PageSize, sys_user model)
        {
            return dal.UserPage(PageIndex, PageSize, model);
        }

        /// <summary>
        /// 根据登录账户和密码获取登陆者信息
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        public sys_user GetUserInfo(string Account, string PassWord)
        {
            return dal.GetUserInfo(Account, PassWord);
        }
        /// <summary>
        /// 该登陆者是否可以访问此菜单
        /// </summary>
        /// <param name="UserId">登陆者ID</param>
        /// /// <param name="NavigateUrl">导航菜单的链接地址</param>
        /// <returns></returns>
        public bool IsAllowedMenu(string UserId, string NavigateUrl)
        {
            return dal.IsAllowedMenu(UserId, NavigateUrl);
        }
    }
}
