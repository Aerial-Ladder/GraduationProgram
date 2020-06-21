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
    }
}
