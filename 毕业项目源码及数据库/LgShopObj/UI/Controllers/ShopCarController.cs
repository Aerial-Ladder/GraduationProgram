using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using UI.Filter;

namespace UI.Controllers
{
    public class ShopCarController : Controller
    {
        [GoLogin]
        // GET: 购物车首页
        public ActionResult ShopCarIndex()
        {
            //商品图片
            ViewBag.GoodsPhoto = GoodsPhotoBll.SelectAllGoodsPhoto();
            //查询当前用户与购物车中的商品的收藏关系
            ViewBag.UserCollection = CollectionBll.SelectUserCollection(Convert.ToInt32(Session["userid"]));
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


        /// <summary>
        /// 新增多个或单个订单
        /// </summary>
        /// <param name="OrderList">订单集合/对象</param>
        /// <returns></returns>
        public JsonResult AddOrder(string OrderList) {
            UserInfo user = Session["user"] as UserInfo;
            //判断收货地址是否为空
            if (user.ReceivingAddress == "")
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<OrderTable> list = js.Deserialize<List<OrderTable>>(OrderList);
            //计算总金额
            decimal price = 0;
            list.ToList().ForEach(p => price += p.OrderAmount??0);
            if (user.UserWallet < price)
            {
                return Json(2, JsonRequestBehavior.AllowGet);
            }
            if (OrderBll.AddOrders(list, Convert.ToInt32(Session["userid"]),price))
            {
                //修改购物车数量
                Session["carcount"] = Convert.ToInt32(Session["carcount"]) - list.Count();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

    }
}