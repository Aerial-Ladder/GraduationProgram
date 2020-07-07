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
                    int num = db.Database.ExecuteSqlCommand($"insert into CommentTable values({userid},{goodid},'{content}',{starrating},'{DateTime.Now}',0,0)");
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

        /// <summary>
        /// 评论举报
        /// </summary>
        /// <param name="commentid">评论id</param>
        /// <returns></returns>
        public static int AddReport(int commentid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                return db.Database.ExecuteSqlCommand($"update CommentTable set Reportingnums+=1 where CommentID={commentid}");
            }
        }

        /// <summary>
        /// 查询所有评论
        /// </summary>
        /// <returns></returns>
        public static List<CommentTable> SelectAllComment() {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                return db.CommentTable.Include("UserInfo").ToList();
            }
        }

        /// <summary>
        /// 根据tid查询商品评论
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public static List<CommentTable> SelectByTidComment(int tid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                return db.Database.SqlQuery<CommentTable>($"select * from CommentTable where GoodsID in(select GoodsID from GoodsTable where TID in (select TypeID from TypeTable where TID in (select TypeID from TypeTable where TID = {tid})))").ToList();
            }
        }

        /// <summary>
        /// 评论是否置顶操作
        /// </summary>
        /// <param name="commentid">评论id</param>
        /// <param name="istop">是否置顶</param>
        /// <returns></returns>
        public static int UpdateIsTop(int commentid, string istop) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                try
                {
                    if (istop == "0")
                    {
                        //将评论置顶
                        db.CommentTable.Find(commentid).IsTop = 1;
                    }
                    else
                    {
                        //将评论取消置顶
                        db.CommentTable.Find(commentid).IsTop = 0;
                    }
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 管理员删除用户评论
        /// </summary>
        /// <param name="commentid">评论id</param>
        /// <returns></returns>
        public static int CommentDelAdmin(int commentid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                try
                {
                    CommentTable con = db.CommentTable.Find(commentid);
                    int id = con.UserID??0;
                    db.CommentTable.Remove(con);
                    db.Database.ExecuteSqlCommand($"insert into NoticeTable values({id},'违规评论处理通知','尊敬的用户您好，您评论的内容:{con.CommentContent} 因违反了乐购商城相关规定，已被删除，请您遵守相关规定！','{DateTime.Now}',0)");
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

    }
}
