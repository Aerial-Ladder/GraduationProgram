﻿using System;
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

        /// <summary>
        /// 更改图片
        /// </summary>
        /// <param name="filename">图片路径</param>
        /// <param name="userid">用户id</param>
        /// <param name="isuserphoto">是否修改头像</param>
        /// <returns></returns>
        public static bool UpdateUserPhoto(string filename,int userid,bool isuserphoto) {
            try
            {
                using (LgShopDBEntities db = new LgShopDBEntities())
                {
                    if (isuserphoto)
                    {
                        db.UserInfo.Find(userid).UserPhoto = filename;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.UserInfo.Find(userid).CoverPhoto = filename;
                        db.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 匹配用户账号和邮箱
        /// </summary>
        /// <param name="UserAccount">账号</param>
        /// <param name="UserEmail">邮箱</param>
        /// <returns></returns>
        public static UserInfo UpdatePwd_1(string UserAccount,string UserEmail) {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                try
                {
                    return db.UserInfo.SingleOrDefault(p=>p.UserAccount==UserAccount&&p.UserEmail==UserEmail);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userpwd">用户新密码</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static bool UpdateUserPwd_2(string userpwd, string userid)
        {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                try
                {
                    return db.Database.ExecuteSqlCommand($"update UserInfo set UserPassword='{userpwd}' where UserID={userid}")==1 ;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
