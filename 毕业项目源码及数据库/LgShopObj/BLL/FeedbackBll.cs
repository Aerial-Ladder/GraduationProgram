using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    public static class FeedbackBll
    {
        /// <summary>
        /// 用户提交反馈
        /// </summary>
        /// <param name="UserFeedback">反馈内容</param>
        /// <param name="Userid">用户id</param>
        /// <returns></returns>
        public static bool AddFeedback(string UserFeedback, string Userid)
        {
            return FeedbackDal.AddFeedback(UserFeedback,Userid);
        }

        /// <summary>
        /// 查询一个用户的所有反馈记录
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<FeedbackTable> SeleteUserFeedback(int userid)
        {
            return FeedbackDal.SeleteUserFeedback(userid);
        }

        /// <summary>
        /// 删除一条反馈记录
        /// </summary>
        /// <param name="feedbackid">反馈id</param>
        /// <returns></returns>
        public static bool UserFeedbackDal(int feedbackid)
        {
            return FeedbackDal.UserFeedbackDal(feedbackid)==1;
        }

        /// <summary>
        /// 查询所有反馈
        /// </summary>
        /// <returns></returns>
        public static List<FeedbackTable> SelectAllFeedback()
        {
            return FeedbackDal.SelectAllFeedback();
        }

        /// <summary>
        /// 处理反馈
        /// </summary>
        /// <param name="feedbackid">反馈id</param>
        /// <returns></returns>
        public static bool UpdateFeedback(int feedbackid)
        {
            return FeedbackDal.UpdateFeedback(feedbackid)==1;
        }

    }
}
