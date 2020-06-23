using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UI.Controllers
{
    public class ShopCarController : Controller
    {
        // GET: 购物车首页
        public ActionResult ShopCarIndex()
        {
            //商品图片
            ViewBag.GoodsPhoto = GoodsPhotoBll.SelectAllGoodsPhoto();
            //查询当前用户所有购物车信息
            return View(ShopingCarBll.SelectAllShopCar(Convert.ToInt32(Session["userid"])));
        }

        /// <summary>
        /// 删除购物车记录
        /// </summary>
        /// <param name="JsonArr">购物车id数组</param>
        /// <returns></returns>
        public JsonResult ShopCarDelete(string JsonArr) {
            JArray jarr = (JArray)JsonConvert.DeserializeObject(JsonArr);
            if (ShopingCarBll.DeleteShopCar(Convert.ToInt32(Session["userid"]), jarr)) {
                //修改购物车数量
                Session["carcount"] = Convert.ToInt32(Session["carcount"]) - jarr.Count();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0,JsonRequestBehavior.AllowGet);
        }
    }
}