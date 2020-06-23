using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
using Newtonsoft.Json.Linq;

namespace BLL
{
    public static class ShopingCarBll
    {
        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <param name="shopnum">购买数量</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static int AddShopCar(string goodsid, string shopnum, string userid)
        {
            //查询是否有这条购物车记录
            if (ShoppingCarDal.SelectShopCar(Convert.ToInt32(userid), Convert.ToInt32(goodsid)) == null)
            {
                //没有则添加
                if (ShoppingCarDal.AddShopCar(goodsid, shopnum, userid) == 1)
                {
                    return 2;
                }
            }
            else {
                //有记录则返回1
                return 1;
            }
            //错误返回0
            return 0;
        }

        /// <summary>
        /// 查询用户所有的购物车信息
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static List<ShoppingCartTable> SelectAllShopCar(int userid)
        {
            return ShoppingCarDal.SelectAllShopCar(userid);
        }

        /// <summary>
        /// 删除购物车记录
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="jarr">购物车id数组</param>
        /// <returns></returns>
        public static bool DeleteShopCar(int userid, JArray jarr)
        {
            return ShoppingCarDal.DeleteShopCar(userid, jarr);
        }

    }
}
