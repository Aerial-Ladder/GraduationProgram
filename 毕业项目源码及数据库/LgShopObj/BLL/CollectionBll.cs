using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    public static class CollectionBll
    {
        /// <summary>
        /// 新增或修改收藏关系
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static bool CreateOrUpdateCollection(int goodsid, int userid)
        {
            return CollectionDal.CreateOrUpdateCollection(goodsid,userid)==1;
        }

        /// <summary>
        /// 查询一个用户的所有收藏
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<CollectionTable> SelectUserCollection(int userid)
        {
            return CollectionDal.SelectUserCollection(userid);
        }

        /// <summary>
        /// 查询一个用户与一个商品的关系(用户是否收藏了该商品)
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="goodsid">商品id</param>
        /// <returns></returns>
        public static bool SelectOneCollection(int userid, int goodsid)
        {
            CollectionTable coll = CollectionDal.SelectOneCollection(userid, goodsid);
            return coll != null && coll.IsCollection == 1;
        }

    }
}
