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
            return GoodsDal.SelectAllGoods();   
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
    }
}
