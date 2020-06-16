using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    public static class UserInfoBll
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        public static bool AddUserInfo(UserInfo user)
        {
            return UserInfoDal.AddUserInfo(user) == 1;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserAccount">账号</param>
        /// <param name="UserPwd">密码</param>
        /// <returns></returns>
        public static UserInfo UserInfoLogin(string UserAccount, string UserPwd)
        {
            return UserInfoDal.UserInfoLogin(UserAccount, UserPwd);
        }

        /// <summary>
        /// 用户修改信息
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        public static bool UserInfoUpdate(UserInfo user)
        {
            return UserInfoDal.UserInfoUpdate(user) == 1;
        }

        /// <summary>
        /// 根据用户id查询某个用户信息
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static UserInfo SelectUser(int userid)
        {
            return UserInfoDal.SelectUser(userid);
        }
    }
}
