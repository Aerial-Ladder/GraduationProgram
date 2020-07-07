using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace BLL
{
    public static class GoodsBll
    {
        /// <summary>
        /// 查询所有商品信息
        /// </summary>
        /// <returns></returns>
        public static List<GoodsTable> SelectAllGoods()
        {
            List<GoodsTable> list= GoodsDal.SelectAllGoods();
            if (list.Count() > 0)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 查询第一分类的商品信息
        /// </summary>
        /// <param name="typeid">第一分类id</param>
        /// <returns></returns>
        public static List<GoodsTable> SelectType1Goods(int typeid)
        {
            return GoodsDal.SelectType1Goods(typeid);
        }

        /// <summary>
        /// 管理员查询第一分类的商品信息
        /// </summary>
        /// <param name="typeid">第一分类id</param>
        /// <returns></returns>
        public static List<GoodsTable> SelectType1GoodsAdmin(int typeid)
        {
            return GoodsDal.SelectType1GoodsAdmin(typeid);
        }

        /// <summary>
        /// 根据tid查找商品信息
        /// </summary>
        /// <param name="tid">商品tid</param>
        /// <returns></returns>
        public static List<GoodsTable> SelectTidGoods(int tid)
        {
            List<GoodsTable> list= GoodsDal.SelectTidGoods(tid);
            if (list.Count() > 0)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据typeid查询商品信息
        /// </summary>
        /// <param name="typeid">商品typeid</param>
        /// <returns></returns>
        public static List<GoodsTable> SelectTypeidGoods(int typeid)
        {
            List<GoodsTable> list = GoodsDal.SelectTypeidGoods(typeid);
            if (list.Count() > 0)
            {
                return list;
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// 根据商品id查询商品
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <returns></returns>
        public static GoodsTable SelectGoodsIdGoods(int goodsid)
        {
            return GoodsDal.SelectGoodsIdGoods(goodsid);
        }

        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        public static bool UpdateGoods(GoodsTable good)
        {
            return GoodsDal.UpdateGoods(good) == 1;
        }

        /// <summary>
        /// 商品停售或出售
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <returns></returns>
        public static bool GoodsDel(int goodsid)
        {
            return GoodsDal.GoodsDel(goodsid)==1;
        }

        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="good">商品对象</param>
        /// <returns></returns>
        public static bool AddGoods(GoodsTable good)
        {
            return GoodsDal.AddGoods(good)==1;
        }

        /// <summary>
        /// 查询推荐商品
        /// </summary>
        /// <param name="typeid">商品id</param>
        /// <returns></returns>
        public static List<GoodsTable> SelectGetGoods(int typeid)
        {
            return GoodsDal.SelectGetGoods(typeid);
        }


    }
}
