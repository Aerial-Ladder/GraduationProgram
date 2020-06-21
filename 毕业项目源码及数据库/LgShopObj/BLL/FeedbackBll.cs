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
    }
}
