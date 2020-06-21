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

        /// <summary>
        /// 更改图片
        /// </summary>
        /// <param name="filename">图片路径</param>
        /// <param name="userid">用户id</param>
        /// <param name="isuserphoto">是否修改头像</param>
        /// <returns></returns>
        public static bool UpdateUserPhoto(string filename, int userid, bool isuserphoto)
        {
            return UserInfoDal.UpdateUserPhoto(filename,userid,isuserphoto);
        }

        /// <summary>
        /// 匹配用户账号和邮箱
        /// </summary>
        /// <param name="UserAccount">账号</param>
        /// <param name="UserEmail">邮箱</param>
        /// <returns></returns>
        public static UserInfo UpdatePwd_1(string UserAccount, string UserEmail)
        {
            return UserInfoDal.UpdatePwd_1(UserAccount,UserEmail);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userpwd">用户新密码</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static bool UpdateUserPwd_2(string userpwd, string userid)
        {
            return UserInfoDal.UpdateUserPwd_2(userpwd,userid);
        }
    }
}
