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
        public ActionResult GoodsIndex(int? typeid_1,int? typeid_2)
        {
            //获得所有分类数据
            ViewBag.Type = TypeTableBll.SelectAllType();
            ViewBag.GoodsPhoto = GoodsPhotoBll.SelectAllGoodsPhoto();
            if (typeid_1 == null && typeid_2 == null)
            {
                TempData["Goods"] = GoodsBll.SelectAllGoods();
            }
            return View();
        }

        public ActionResult GoodsDesc() {
            return View();
        }

        public ActionResult Show(int? pageindex) {
            ViewBag.GoodsPhoto = GoodsPhotoBll.SelectAllGoodsPhoto();
            List<GoodsTable> list = GoodsBll.SelectAllGoods();
            ViewBag.count =Math.Ceiling(list.Count() / 12.0);
            ViewBag.pageindex = pageindex;
            return PartialView("Show",list.Skip(((pageindex??1) - 1) * 12).Take(12).ToList());
        }
    }
}