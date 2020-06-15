using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class GoodsController : Controller
    {
        // GET: Goods
        public ActionResult GoodsIndex()
        {
            return View();
        }

        public ActionResult GoodsDesc() {
            return View();
        }
    }
}