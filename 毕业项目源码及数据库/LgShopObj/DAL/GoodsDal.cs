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
                return db.GoodsTable.ToList();
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
                return db.Database.SqlQuery<GoodsTable>($"select * from GoodsTable where TID in(select TypeID from TypeTable where TID in (select TypeID from TypeTable where TID = (select TypeID from TypeTable where TypeID = {typeid})))").ToList();
            }
        }
    }
}
