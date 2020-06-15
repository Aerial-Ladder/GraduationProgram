using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public static class UserInfoDal
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        public static int AddUserInfo(UserInfo user) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                return db.Database.ExecuteSqlCommand($"insert into UserInfo values('{user.UserName}','{user.UserAccount}','{user.UserPassword}','default.jpg','{user.UserSex}',{user.UserAge},'{user.UserEmail}','{user.UserPhont}','{user.UserCard}','{user.UserBirthdays}',0,'','')");
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserAccount">账号</param>
        /// <param name="UserPwd">密码</param>
        /// <returns></returns>
        public static UserInfo UserInfoLogin(string UserAccount, string UserPwd) {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                try
                {
                    return db.UserInfo.SingleOrDefault(p => p.UserAccount == UserAccount && p.UserPassword == UserPwd);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
