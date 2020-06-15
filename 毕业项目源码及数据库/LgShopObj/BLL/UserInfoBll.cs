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
            return UserInfoDal.UserInfoLogin(UserAccount,UserPwd);
        }
    }
}
