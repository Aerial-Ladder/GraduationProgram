using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public static class FeedbackDal
    {
        /// <summary>
        /// 用户提交反馈
        /// </summary>
        /// <param name="UserFeedback">反馈内容</param>
        /// <param name="Userid">用户id</param>
        /// <returns></returns>
        public static bool AddFeedback(string UserFeedback,string Userid) {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                try
                {
                    //新增反馈信息
                    return db.Database.ExecuteSqlCommand($"insert into FeedbackTable values({Userid},'{UserFeedback}','{DateTime.Now}',0)") == 1;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 查询一个用户的所有反馈记录
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<FeedbackTable> SeleteUserFeedback(int userid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                try
                {
                    return db.FeedbackTable.Where(p => p.UserID == userid).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 删除一条反馈记录
        /// </summary>
        /// <param name="feedbackid">反馈id</param>
        /// <returns></returns>
        public static int UserFeedbackDal(int feedbackid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                try
                {
                    return db.Database.ExecuteSqlCommand($"delete from FeedbackTable where FeedbackID={feedbackid}");
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 查询所有反馈
        /// </summary>
        /// <returns></returns>
        public static List<FeedbackTable> SelectAllFeedback() {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                db.Configuration.LazyLoadingEnabled = false;
                return db.FeedbackTable.Include("UserInfo").ToList();
            }
        }

        /// <summary>
        /// 处理反馈
        /// </summary>
        /// <param name="feedbackid">反馈id</param>
        /// <returns></returns>
        public static int UpdateFeedback(int feedbackid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                db.FeedbackTable.Find(feedbackid).IsDealwith = 1;
                return db.SaveChanges();
            }
        }

    }
}
