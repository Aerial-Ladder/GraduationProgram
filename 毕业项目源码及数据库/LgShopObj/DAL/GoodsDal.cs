﻿using System;
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
                    return db.Database.SqlQuery<GoodsTable>($"select * from GoodsTable where TID in (select TypeID from TypeTable where TID = {tid}) ").ToList();
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
                    return db.GoodsTable.Where(p => p.TID == typeid).ToList();
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

    }
}
