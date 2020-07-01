﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public static class NoticeDal
    {
        /// <summary>
        /// 查询一个用户所有消息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<NoticeTable> SelectUserNotice(int userid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                return db.NoticeTable.Where(p => p.UserID == userid).ToList();
            }
        }

        /// <summary>
        /// 删除某个消息
        /// </summary>
        /// <param name="noticeid">消息id</param>
        /// <returns></returns>
        public static int DeleteNotice(int noticeid) {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                db.NoticeTable.Remove(db.NoticeTable.Find(noticeid));
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 修改用户的消息为已查看
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int UpdateUserNotice(int userid) {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                return db.Database.ExecuteSqlCommand($"update NoticeTable set IsLook=1 where userid={userid}");
            }
        }

        /// <summary>
        /// 查询所有消息
        /// </summary>
        /// <returns></returns>
        public static List<NoticeTable> SelectAllNotice() {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                return db.NoticeTable.ToList();
            }
        }

    }
}
