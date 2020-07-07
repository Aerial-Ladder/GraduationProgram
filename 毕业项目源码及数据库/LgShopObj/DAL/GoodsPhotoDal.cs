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
                return db.GoodsPhoto.Include("GoodsTable").ToList();
            }
        }

        /// <summary>
        /// 新增商品图片
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <param name="pathlist">图片路径集合</param>
        /// <returns></returns>
        public static int AddGoodsPhoto(int goodsid,List<string> pathlist)
        {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                try
                {
                    foreach (var item in pathlist)
                    {
                        List<GoodsPhoto> list = db.GoodsPhoto.Where(p => p.GoodsID == goodsid && p.PhotoPath == item).ToList();
                        if (list == null || list.Count() <1)
                        {
                            db.GoodsPhoto.Add(new GoodsPhoto() { GoodsID = goodsid, PhotoPath = item });
                        }
                    }
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 删除商品图片
        /// </summary>
        /// <param name="rid">商品图片主键id</param>
        /// <returns></returns>
        public static int GoodsPhotoDel(int rid)
        {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                GoodsPhoto gp = db.GoodsPhoto.Find(rid);
                List<GoodsPhoto> list = db.GoodsPhoto.Where(p => p.GoodsID == gp.GoodsID).ToList();
                if (list != null && list.Count() > 1)
                {
                    db.GoodsPhoto.Remove(gp);
                    return db.SaveChanges();
                }
                else {
                    return 2;
                }
                
            }
        }

    }
}
