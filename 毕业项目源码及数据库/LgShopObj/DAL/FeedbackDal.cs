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
    }
}
