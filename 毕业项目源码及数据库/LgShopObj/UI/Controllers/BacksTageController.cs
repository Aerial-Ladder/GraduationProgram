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
        public ActionResult UserBackIndex() {
            List<UserInfo> list = UserInfoBll.SelectAllUser();
            return PartialView("UserBackIndex", list);
        }
    }
}