using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;

namespace UI.Controllers
{
    public class NoticeController : Controller
    {
        // GET: Notice
        public ActionResult NoticeIndex()
        {
            return View(NoticeBll.SelectAllNotice().Where(p=>p.UserID==null).ToList());
        }
    }
}