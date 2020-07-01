using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;

namespace UI.Controllers
{
    public class BacksTageController : Controller
    {
        // GET: BacksTage
        public ActionResult Index()
        {
            //查询所有用户信息
            Session["alluserlist"] = UserInfoBll.SelectAllUser();
            return View();
        }

        /// <summary>
        /// 后台用户列表分布视图
        /// </summary>
        /// <returns></returns>
        public ActionResult UserBackIndex(int? pageindex,string text="") {
            List<UserInfo> list = UserInfoBll.SelectAllUser().Where(p => p.UserName.Contains(text) || p.UserAccount.Contains(text) || text == "").ToList();
            ViewBag.count = Math.Ceiling(list.Count() / 12.0);
            ViewBag.pageindex = pageindex;
            Session["alluserlist"] = list.Skip(((pageindex ?? 1) - 1) * 8).Take(8).ToList();
            return PartialView("UserBackIndex");
        }

        /// <summary>
        /// 查询单个用户的信息
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public JsonResult UserDesc(string UserID) {
            UserInfo user = UserInfoBll.SelectUser(Convert.ToInt32(UserID));
            if (user != null) {
                return Json(new { user=user,b= string.Format("{0:D}", user.UserBirthdays.Date) }, JsonRequestBehavior.AllowGet);
            }
            return Json(null,JsonRequestBehavior.AllowGet);
        }
    }
}