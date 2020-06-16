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

        /// <summary>
        /// 用户修改信息
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        public static int UserInfoUpdate(UserInfo user) {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                return db.Database.ExecuteSqlCommand($"update UserInfo set UserName='{user.UserName}',UserAccount='{user.UserAccount}',UserEmail='{user.UserEmail}',UserAge='{user.UserAge}',UserSex='{user.UserSex}',UserCard='{user.UserCard}',ReceivingAddress='{user.ReceivingAddress}',UserBirthdays='{user.UserBirthdays}',UserPhont='{user.UserPhont}' where UserID='{user.UserID}'");
            }
        }

        /// <summary>
        /// 根据用户id查询某个用户信息
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static UserInfo SelectUser(int userid) {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                return db.UserInfo.Find(userid);
            }
        }
    }
}
