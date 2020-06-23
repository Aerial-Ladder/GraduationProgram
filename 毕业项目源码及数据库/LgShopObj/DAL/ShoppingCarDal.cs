using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json.Linq;

namespace DAL
{
    public static class ShoppingCarDal
    {
       /// <summary>
       /// 加入购物车
       /// </summary>
       /// <param name="goodsid">商品id</param>
       /// <param name="shopnum">购买数量</param>
       /// <param name="userid">用户id</param>
       /// <returns></returns>
        public static int AddShopCar(string goodsid,string shopnum,string userid) {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                try
                {
                    return db.Database.ExecuteSqlCommand($"insert into ShoppingCartTable values({userid},{goodsid},{shopnum},'{DateTime.Now}')");
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 查询某一条购物车纪录
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="goodsid">商品id</param>
        /// <returns></returns>
        public static ShoppingCartTable SelectShopCar(int userid, int goodsid) {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                try
                {
                    return db.ShoppingCartTable.Single(p => p.UserID == userid && p.GoodsID == goodsid);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 查询用户所有的购物车信息
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static List<ShoppingCartTable> SelectAllShopCar(int userid) {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return db.ShoppingCartTable.Include("GoodsTable").Where(p => p.UserID == userid).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 删除购物车记录
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="jarr">购物车id数组</param>
        /// <returns></returns>
        public static bool DeleteShopCar(int userid,JArray jarr) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                try
                {
                    for (int i = 0; i < jarr.Count(); i++)
                    {
                        db.Database.ExecuteSqlCommand($"delete from ShoppingCartTable where userid={userid} and CartID={jarr[i]}");
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }


    }
}
