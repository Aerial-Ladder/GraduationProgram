using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    public static class CommentBll
    {
        /// <summary>
        /// 查询用户的评论
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<CommentTable> SelectUserComment(int userid)
        {
            return CommentDal.SelectUserComment(userid);
        }

        /// <summary>
        /// 新增评价
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="goodid">商品id</param>
        /// <param name="content">评价内容</param>
        /// <param name="starrating">星级</param>
        /// <returns></returns>
        public static bool AddUserComment(int userid, int goodid, string content, int starrating)
        {
            return CommentDal.AddUserComment(userid, goodid, content, starrating) == 1;
        }

        /// <summary>
        /// 删除评价
        /// </summary>
        /// <param name="commentid">评价id</param>
        /// <returns></returns>
        public static bool DeleteUserComment(int commentid)
        {
            return CommentDal.DeleteUserComment(commentid)==1;
        }

        /// <summary>
        /// 根据商品id查询该商品的评价
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <returns></returns>
        public static List<CommentTable> SelectGoodsComment(int goodsid)
        {
            return CommentDal.SelectGoodsComment(goodsid);
        }

        /// <summary>
        /// 评论举报
        /// </summary>
        /// <param name="commentid">评论id</param>
        /// <returns></returns>
        public static bool AddReport(int commentid)
        {
            return CommentDal.AddReport(commentid) == 1;
        }

        /// <summary>
        /// 查询所有评论
        /// </summary>
        /// <returns></returns>
        public static List<CommentTable> SelectAllComment()
        {
            return CommentDal.SelectAllComment();
        }

        /// <summary>
        /// 根据tid查询商品评论
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public static List<CommentTable> SelectByTidComment(int tid)
        {
            return CommentDal.SelectByTidComment(tid);
        }

        /// <summary>
        /// 评论是否置顶操作
        /// </summary>
        /// <param name="commentid">评论id</param>
        /// <param name="istop">是否置顶</param>
        /// <returns></returns>
        public static bool UpdateIsTop(int commentid, string istop)
        {
            return CommentDal.UpdateIsTop(commentid,istop)==1;
        }

        /// <summary>
        /// 管理员删除用户评论
        /// </summary>
        /// <param name="commentid">评论id</param>
        /// <returns></returns>
        public static bool CommentDelAdmin(int commentid)
        {
            return CommentDal.CommentDelAdmin(commentid)==1;
        }

    }
}
