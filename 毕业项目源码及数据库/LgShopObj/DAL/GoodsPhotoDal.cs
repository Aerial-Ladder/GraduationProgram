using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public static class GoodsPhotoDal
    {
        /// <summary>
        /// 查询所有商品图片
        /// </summary>
        /// <returns></returns>
        public static List<GoodsPhoto> SelectAllGoodsPhoto() {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                return db.GoodsPhoto.ToList();
            }
        }
    }
}
