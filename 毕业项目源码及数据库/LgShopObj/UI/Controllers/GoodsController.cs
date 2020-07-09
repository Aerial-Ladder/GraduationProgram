using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Models;

namespace UI.Controllers
{
    public class GoodsController : Controller
    {
        // GET: Goods
        public ActionResult GoodsIndex(int? tid_1,int? typeid_1,int? typeid_2,string txt="")
        {
            //清空session数据
            Session.Remove("Goods");
            Session.Remove("Goods_tid");
            Session.Remove("Goods_typeid");
            //获得所有分类数据
            ViewBag.Type = TypeTableBll.SelectAllType();
            ViewBag.GoodsPhoto = GoodsPhotoBll.SelectAllGoodsPhoto();
            if (tid_1 != null)
            {
                //根据tid查询
                Session["Goods_tid"] = GoodsBll.SelectTidGoods(tid_1 ?? 0);
                //ViewBag.tid = tid_1 ?? 0;
            }
            else if (typeid_1 != null)
            {
                //根据tiyeid查询
                Session["Goods_typeid"] = GoodsBll.SelectTypeidGoods(typeid_1 ?? 0);
            }
            else if (typeid_2 != null)
            {
                Session["Goods_typeid_2"] = GoodsBll.SelectType1Goods(typeid_2??0);
            }
            else {
                Session["Goods"] = GoodsBll.SelectAllGoods().Where(p=>p.GoodsName.Contains(txt)&&p.IsDelte==0).OrderBy(p => p.GoodsHot).ToList();
            }
            return View();
        }

        /// <summary>
        /// 查询所有商品信息的分布视图
        /// </summary>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public ActionResult Show(int? pageindex) {
            ViewBag.GoodsPhoto = GoodsPhotoBll.SelectAllGoodsPhoto();
            List<GoodsTable> list = GoodsBll.SelectAllGoods().Where(p=>p.IsDelte==0).ToList();
            ViewBag.count =Math.Ceiling(list.Count() / 12.0);
            ViewBag.pageindex = pageindex;
            return PartialView("Show",list.Skip(((pageindex??1) - 1) * 12).Take(12).ToList());
        }

        /// <summary>
        /// 根据tid查询商品信息的分布视图
        /// </summary>
        /// <param name="pageindex">当前页</param>
        /// <returns></returns>
        public ActionResult Show_tid(int? pageindex) {
            ViewBag.GoodsPhoto = GoodsPhotoBll.SelectAllGoodsPhoto();
            List<GoodsTable> list = Session["Goods_tid"] as List<GoodsTable>;
            ViewBag.count = Math.Ceiling(list.Count() / 12.0);
            ViewBag.pageindex = pageindex;
            return PartialView("Show", list.Skip(((pageindex ?? 1) - 1) * 12).Take(12).ToList());
        }

        /// <summary>
        /// 根据typeid查询商品信息的分布视图
        /// </summary>
        /// <param name="pageindex">当前页</param>
        /// <returns></returns>
        public ActionResult Show_typeid(int? pageindex,int? typeid)
        {
            ViewBag.GoodsPhoto = GoodsPhotoBll.SelectAllGoodsPhoto();
            List<GoodsTable> list = null;
            if (typeid == null)
            {
                list = Session["Goods_typeid"] as List<GoodsTable>;
                ViewBag.count = Math.Ceiling(list.Count() / 12.0);
            }
            else {
                list = GoodsBll.SelectTypeidGoods(typeid??0);
                if (list == null)
                {
                    ViewBag.isnull = true;
                    return PartialView("Show", null);
                }
                else {
                    ViewBag.count = Math.Ceiling(list.Count() / 12.0);
                }
            }
            ViewBag.pageindex = pageindex;
            return PartialView("Show", list.Skip(((pageindex ?? 1) - 1) * 12).Take(12).ToList());
        }

        /// <summary>
        /// 第一大类商品分布页
        /// </summary>
        /// <param name="pageindex">当前页</param>
        /// <returns></returns>
        public ActionResult Show_typeid_2(int? pageindex)
        {
            ViewBag.GoodsPhoto = GoodsPhotoBll.SelectAllGoodsPhoto();
            List<GoodsTable> list = Session["Goods_typeid_2"] as List<GoodsTable>;
            ViewBag.count = Math.Ceiling(list.Count() / 12.0);
            ViewBag.pageindex = pageindex;
            return PartialView("Show", list.Skip(((pageindex ?? 1) - 1) * 12).Take(12).ToList());
        }

        /// <summary>
        /// 商品详情
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <returns></returns>
        public ActionResult GoodsDesc(int goodsid)
        {
            //商品图片
            ViewBag.GoodsPhoto = GoodsPhotoBll.SelectAllGoodsPhoto().Where(p=>p.GoodsID==goodsid).ToList();
            //商品的所有评价(置顶排序)
            List<CommentTable> list = CommentBll.SelectGoodsComment(goodsid).OrderByDescending(p=>p.IsTop).ToList();
            Session["GoodsComment"] = list;
            //用户是否收藏商品
            ViewBag.iscollection = CollectionBll.SelectOneCollection(Convert.ToInt32(Session["userid"]), goodsid);
            //加载或更新用户购物车数量
            if (Session["userid"] != null)
            {
                Session["carcount"] = ShopingCarBll.SelectAllShopCar(Convert.ToInt32(Session["userid"])).Count();
            }
            //获取到的该商品的信息
            GoodsTable good = GoodsBll.SelectGoodsIdGoods(goodsid);
            //相关商品的推荐(3条)
            ViewBag.GetGoods = GoodsBll.SelectGetGoods(good.TID??0).Where(p=>p.GoodsID!=good.GoodsID).OrderBy(p=>Guid.NewGuid()).Take(3).ToList();
            Session["GoodsPhoto"] = GoodsPhotoBll.SelectAllGoodsPhoto();
            return View(good);
        }

        /// <summary>
        /// 收藏关系修改
        /// </summary>
        /// <param name="GoodsId">商品id</param>
        /// <returns></returns>
        public JsonResult CollectionPlay(int GoodsId) {
            if (CollectionBll.CreateOrUpdateCollection(GoodsId, Convert.ToInt32(Session["userid"])))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="GoodsId">商品id</param>
        /// <param name="ShoppingNum">购买数量</param>
        /// <returns></returns>
        public JsonResult AddShopCar(string GoodsId,string ShoppingNum) {
            //执行新增操作
            int erroid = ShopingCarBll.AddShopCar(GoodsId, ShoppingNum, Session["userid"].ToString());
            //更新购物车数量
            Session["carcount"] = ShopingCarBll.SelectAllShopCar(Convert.ToInt32(Session["userid"])).Count();
            //调用新增购物车并返回值
            return Json(erroid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 立即购买
        /// </summary>
        /// <param name="GoodsId">商品id</param>
        /// <param name="ShoppingNum">购买数量</param>
        /// <param name="OrderAmount">小计</param>
        /// <returns></returns>
        public JsonResult AddOrder(string GoodsId, string ShoppingNum,string OrderAmount) {
            UserInfo user = Session["user"] as UserInfo;
            //判断收货地址是否为空
            if (user.ReceivingAddress == "")
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
            //余额是否满足
            if (user.UserWallet < Convert.ToDecimal(OrderAmount))
            {
                return Json(2, JsonRequestBehavior.AllowGet);
            }
            OrderTable ord = new OrderTable()
            {
                GoodsID = Convert.ToInt32(GoodsId),
                UserID = Convert.ToInt32(Session["userid"]),
                GoodsNum=Convert.ToInt32(ShoppingNum),
                GetTime=DateTime.Now,
                OrderAmount=Convert.ToDecimal(OrderAmount),
                IsComment=0,
                IsReceiving=0
            };

            if (OrderBll.AddOrder(ord))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GoodsComment(int num,int goodsid) {
            List<CommentTable> list = CommentBll.SelectGoodsComment(goodsid);
            //好评
            if (num == 0)
            {
                list = list.Where(p => p.CommentStarRating >= 4).ToList();
            }
            else if (num == 1)
            {
                //中评
                list = list.Where(p => p.CommentStarRating == 3).ToList();
            }
            else if(num==2)
            {
                //差评
                list = list.Where(p => p.CommentStarRating <=2).ToList();
            }
            ViewBag.num = num;
            Session["GoodsComment"] = list;
            return PartialView("GoodsComment");
        }

        /// <summary>
        /// 评论举报
        /// </summary>
        /// <param name="CommentID">评论id</param>
        /// <returns></returns>
        public JsonResult AddReport(string CommentID) {
            if (CommentBll.AddReport(Convert.ToInt32(CommentID)))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

    }
}