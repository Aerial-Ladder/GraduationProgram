using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    public static class NoticeBll
    {
        /// <summary>
        /// 查询一个用户所有消息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<NoticeTable> SelectUserNotice(int userid)
        {
            List<NoticeTable> list = NoticeDal.SelectUserNotice(userid);
            if (list != null && list.Count() > 0)
            {
                return list;
            }
            return null;
        }

        /// <summary>
        /// 删除某个消息
        /// </summary>
        /// <param name="noticeid">消息id</param>
        /// <returns></returns>
        public static bool DeleteNotice(int noticeid)
        {
            return NoticeDal.DeleteNotice(noticeid) == 1;
        }

        /// <summary>
        /// 修改用户的消息为已查看
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool UpdateUserNotice(int userid)
        {
            return NoticeDal.UpdateUserNotice(userid) > 0;
        }

        /// <summary>
        /// 查询所有消息
        /// </summary>
        /// <returns></returns>
        public static List<NoticeTable> SelectAllNotice()
        {
            return NoticeDal.SelectAllNotice();
        }

        /// <summary>
        /// 新增公告
        /// </summary>
        /// <param name="notice">公告对象</param>
        /// <returns></returns>
        public static bool AddNotice(NoticeTable notice)
        {
            return NoticeDal.AddNotice(notice)==1;
        }

    }
}
