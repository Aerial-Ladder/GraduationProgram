using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public static class CollectionDal
    {
        /// <summary>
        /// 新增或修改收藏关系
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static int CreateOrUpdateCollection(int goodsid,int userid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                List<CollectionTable> list = db.Database.SqlQuery<CollectionTable>($"select * from CollectionTable where userid={userid} and GoodsID={goodsid}").ToList();
                if (list.Count() > 0)
                {
                    //如果存在数据，如果已经收藏
                    if (list[0].IsCollection == 1)
                    {
                        return db.Database.ExecuteSqlCommand($"update CollectionTable set IsCollection=0 where CollectionID={list[0].CollectionID}");
                    }
                    else {
                        //未收藏
                        return db.Database.ExecuteSqlCommand($"update CollectionTable set IsCollection=1 where CollectionID={list[0].CollectionID}");
                    }
                }
                else {
                    //不存在则创建
                    return db.Database.ExecuteSqlCommand($"insert into CollectionTable values({userid},{goodsid},1)");
                }
            }
        }

        /// <summary>
        /// 查询一个用户的所有收藏
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<CollectionTable> SelectUserCollection(int userid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                return db.CollectionTable.Include("GoodsTable").Where(p => p.UserID == userid).ToList();
            }
        }

        /// <summary>
        /// 查询一个用户与一个商品的关系
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="goodsid">商品id</param>
        /// <returns></returns>
        public static CollectionTable SelectOneCollection(int userid, int goodsid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                try
                {
                    return db.CollectionTable.Single(p => p.UserID == userid && p.GoodsID == goodsid);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

    }
}
