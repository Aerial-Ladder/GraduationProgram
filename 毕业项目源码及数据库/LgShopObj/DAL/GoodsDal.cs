using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public static class GoodsDal
    {
        /// <summary>
        /// 查询所有商品信息
        /// </summary>
        /// <returns></returns>
        public static List<GoodsTable> SelectAllGoods() {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.GoodsTable.Include("TypeTable").ToList();
            }
        }


        /// <summary>
        /// 查询第一分类的商品信息
        /// </summary>
        /// <param name="typeid">第一分类id</param>
        /// <returns></returns>
        public static List<GoodsTable> SelectType1Goods(int typeid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) 
            {
                return db.Database.SqlQuery<GoodsTable>($"select * from GoodsTable where TID in(select TypeID from TypeTable where TID in (select TypeID from TypeTable where TID = (select TypeID from TypeTable where TypeID = {typeid}))) and IsDelte=0").ToList();
            }
        }

        /// <summary>
        /// 管理员查询第一分类的商品信息
        /// </summary>
        /// <param name="typeid">第一分类id</param>
        /// <returns></returns>
        public static List<GoodsTable> SelectType1GoodsAdmin(int typeid)
        {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                return db.Database.SqlQuery<GoodsTable>($"select * from GoodsTable where TID in(select TypeID from TypeTable where TID in (select TypeID from TypeTable where TID = (select TypeID from TypeTable where TypeID = {typeid})))").ToList();
            }
        }


        /// <summary>
        /// 根据tid查找商品信息
        /// </summary>
        /// <param name="tid">商品tid</param>
        /// <returns></returns>
        public static List<GoodsTable> SelectTidGoods(int tid) {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                try
                {
                    return db.Database.SqlQuery<GoodsTable>($"select * from GoodsTable where TID in (select TypeID from TypeTable where TID = {tid}) and IsDelte=0 ").ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 根据typeid查询商品信息
        /// </summary>
        /// <param name="typeid">商品typeid</param>
        /// <returns></returns>
        public static List<GoodsTable> SelectTypeidGoods(int typeid)
        {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                try
                {
                    return db.GoodsTable.Where(p => p.TID == typeid&&p.IsDelte==0).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 根据商品id查询商品
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <returns></returns>
        public static GoodsTable SelectGoodsIdGoods(int goodsid) {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                return db.GoodsTable.Find(goodsid);
            }
        }

        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        public static int UpdateGoods(GoodsTable good) {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                return db.Database.ExecuteSqlCommand($"update GoodsTable set GoodsName='{good.GoodsName}',GoodsPrice={good.GoodsPrice},OldGoodsPrice={good.OldGoodsPrice},GoodsInventory={good.GoodsInventory},TID={good.TID},GoodsDescribe='{good.GoodsDescribe}',IsGet={good.IsGet} where GoodsID={good.GoodsID}");
            }
        }

        /// <summary>
        /// 商品停售或出售
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <returns></returns>
        public static int GoodsDel(int goodsid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                GoodsTable good = db.GoodsTable.Find(goodsid);
                if (good.IsDelte == 0)
                {
                    //停售
                    good.IsDelte = 1;
                    //发布停售公告
                    db.Database.ExecuteSqlCommand($"insert into NoticeTable values(null,'商品停售公告','各位乐购商城用户你们好,于{DateTime.Now.ToLongDateString().ToString()}起{good.GoodsName}商品将从商城下架，查看详情请联系TEL:13677447830','{DateTime.Now}',1)");
                }
                else {
                    //出售
                    good.IsDelte = 0;
                    //发布出售公告
                    db.Database.ExecuteSqlCommand($"insert into NoticeTable values(null,'商品出售公告','各位乐购商城用户你们好,于{DateTime.Now.ToLongDateString().ToString()}起{good.GoodsName}商品将重新出售，查看详情请联系TEL:13677447830','{DateTime.Now}',1)");
                }
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="good">商品对象</param>
        /// <returns></returns>
        public static int AddGoods(GoodsTable good)
        {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                try
                {
                    //新增商品上架公告
                    db.Database.ExecuteSqlCommand($"insert into NoticeTable values(null,'新商品上架公告','各位乐购商城用户你们好,新商品{good.GoodsName}于{DateTime.Now.ToLongDateString().ToString()}上架，期待您的购买！','{DateTime.Now}',1)");
                    return db.Database.ExecuteSqlCommand($"insert into GoodsTable values('{good.GoodsName}',{good.GoodsPrice},{good.OldGoodsPrice},{good.GoodsInventory},{good.TID},'{good.GoodsDescribe}',0,0,0,0)");
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 查询推荐商品
        /// </summary>
        /// <param name="typeid">商品id</param>
        /// <returns></returns>
        public static List<GoodsTable> SelectGetGoods(int typeid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                try
                {
                    return db.Database.SqlQuery<GoodsTable>("select * from GoodsTable where TID in ("+
                    "select TypeID from TypeTable where TID in ("+
                    "select TypeID from TypeTable where TID = ("+
                    $"select TypeID from TypeTable where TypeID = ( select TID from TypeTable where TypeID = (select TID from TypeTable where TypeID ={typeid} ))))) and IsGet = 1 and IsDelte=0").ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

    }
}
