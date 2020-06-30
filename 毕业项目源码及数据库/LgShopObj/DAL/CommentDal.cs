using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public static class CommentDal
    {
        /// <summary>
        /// 查询用户的评论
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<CommentTable> SelectUserComment(int userid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                return db.CommentTable.Include("GoodsTable").Where(p => p.UserID == userid).ToList();
            }
        }

        /// <summary>
        /// 新增评价
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="goodid">商品id</param>
        /// <param name="content">评价内容</param>
        /// <param name="starrating">星级</param>
        /// <returns></returns>
        public static int AddUserComment(int userid,int goodid,string content,int starrating) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                try
                {
                    //新增一条评价信息
                    int num = db.Database.ExecuteSqlCommand($"insert into CommentTable values({userid},{goodid},'{content}',{starrating},'{DateTime.Now}')");
                    return num;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 删除评价
        /// </summary>
        /// <param name="commentid">评价id</param>
        /// <returns></returns>
        public static int DeleteUserComment(int commentid)
        {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                try
                {
                    db.CommentTable.Remove(db.CommentTable.Find(commentid));
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 根据商品id查询该商品的评价
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <returns></returns>
        public static List<CommentTable> SelectGoodsComment(int goodsid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                return db.CommentTable.Include("UserInfo").Where(p => p.GoodsID == goodsid).ToList();
            }
        }

    }
}
