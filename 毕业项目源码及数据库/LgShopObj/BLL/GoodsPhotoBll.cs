using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    public static class GoodsPhotoBll
    {
        /// <summary>
        /// 查询所有商品图片
        /// </summary>
        /// <returns></returns>
        public static List<GoodsPhoto> SelectAllGoodsPhoto()
        {
            return GoodsPhotoDal.SelectAllGoodsPhoto();
        }

        /// <summary>
        /// 新增商品图片
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <param name="pathlist">图片路径集合</param>
        /// <returns></returns>
        public static bool AddGoodsPhoto(int goodsid, List<string> pathlist)
        {
            return GoodsPhotoDal.AddGoodsPhoto(goodsid,pathlist) >0;
        }

        /// <summary>
        /// 删除商品图片
        /// </summary>
        /// <param name="rid">商品图片主键id</param>
        /// <returns></returns>
        public static int GoodsPhotoDel(int rid)
        {
            int num = GoodsPhotoDal.GoodsPhotoDel(rid);
            if (num == 0)
            {
                return 0;
            }
            else if (num == 1)
            {
                return 1;
            }
            else {
                return 2;
            }
        }

    }
}
