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
    }
}
