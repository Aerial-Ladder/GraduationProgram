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
        public ActionResult GoodsIndex(int? tid_1,int? typeid_1,int? typeid_2)
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
                Session["Goods"] = GoodsBll.SelectAllGoods().OrderBy(p => p.GoodsHot).ToList();
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
            List<GoodsTable> list = GoodsBll.SelectAllGoods();
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
            return View(GoodsBll.SelectGoodsIdGoods(goodsid));
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

    }
}