using MyFrameCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.IDAL
{
    public interface ISys_UserDAL
    {
        /// <summary>
        /// 根据主键获取登陆者信息
        /// </summary>
        /// <param name="KeyId"></param>
        /// <returns></returns>
        sys_user GetUserInfo(string KeyId);
        /// <summary>
        /// 保存用户角色信息
        /// </summary>
        /// <param name="URList">用户角色集合</param>
        /// <returns></returns>
        bool SaveUserRole(List<sys_userrole> URList);

        /// <summary>
        /// 用户列表分页
        /// </summary>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <returns></returns>
        PageModel<dynamic> UserPage(int PageIndex, int PageSize, sys_user model);
        /// <summary>
        /// 根据登录账户和密码获取登陆者信息
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        sys_user GetUserInfo(string Account, string PassWord);
        /// <summary>
        /// 该登陆者是否可以访问此菜单
        /// </summary>
        /// <param name="UserId">登陆者ID</param>
        /// /// <param name="NavigateUrl">导航菜单的链接地址</param>
        /// <returns></returns>
        bool IsAllowedMenu(string UserId, string NavigateUrl);
    }
}
