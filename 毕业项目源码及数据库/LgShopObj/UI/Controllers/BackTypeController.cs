using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using UI.Filter;

namespace UI.Controllers
{
    public class BackTypeController : Controller
    {
        //[Admin]
        // GET: BackType
        public ActionResult BackTypeIndex()
        {
            Session["li_1"] = 2;
            Session["li_2"] = 0;
            List<TypeTable> typetable = TypeTableBll.SelectAllType();
            return View(typetable);
        }

        /// <summary>
        /// 修改分类名称或所属分类
        /// </summary>
        /// <param name="TypeID">分类id</param>
        /// <param name="TypeName">分类名称</param>
        /// <returns></returns>
        public JsonResult UpdateTypeName(string TypeID,string TypeName,string TID) {
            TypeTable type = new TypeTable();
            if (TID == "")
            {
                type = new TypeTable() { TypeID = Convert.ToInt32(TypeID), TypeName = TypeName, TID = null };
            }
            else {
                type = new TypeTable() { TypeID = Convert.ToInt32(TypeID), TypeName = TypeName, TID = Convert.ToInt32(TID) };
            }
            if (TypeTableBll.UpdateType(type))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询上一级分类
        /// </summary>
        /// <param name="TypeID">分类id</param>
        /// <returns></returns>
        public JsonResult SelectTid(string index)
        {
            List<TypeTable> list = new List<TypeTable>();
            if (index == "2")
            {
                //查询第一分类
                list = TypeTableBll.SelectAllType().Where(p=>p.TID==null).Select(p => new TypeTable() { TypeID = p.TypeID, TypeName = p.TypeName }).ToList();
            }
            else {
                //查询第二分类
                list = TypeTableBll.SelectTypeTable().Select(p => new TypeTable() { TypeID = p.TypeID, TypeName = p.TypeName }).ToList();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增分类
        /// </summary>
        /// <param name="TID">所属分类id</param>
        /// <param name="TypeName">类型名称</param>
        /// <returns></returns>
        public JsonResult AddType(string TID, string TypeName) {
            if (TypeTableBll.AddType(TypeName, Convert.ToInt32(TID)))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="TypeID">分类id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TypeDel(string TypeID) {
            if (TypeTableBll.DeleteType(Convert.ToInt32(TypeID)))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 后台反馈界面
        /// </summary>
        /// <returns></returns>
        public ActionResult BackFeedbackIndex() {
            Session["li_1"] = 3;
            Session["li_2"] = 0;
            //查询所有反馈
            List<FeedbackTable> list = FeedbackBll.SelectAllFeedback().OrderBy(p => p.IsDealwith).ToList();
            if (list != null && list.Count() > 0)
            {
                Session["fkcount"] = list.Count();
            }
            else
            {
                Session["fkcount"] = 0;
            }
            ViewBag.IsDealwith = 2;
            Session["fkpagecount"] = Math.Ceiling(list.Count() / 10.0);
            Session["allfk"] = list.Take(10).ToList();
            return View();
        }

        /// <summary>
        /// 后台反馈分布视图
        /// </summary>
        /// <returns></returns>
        public ActionResult BackFeedbackPartial(int? pageindex, int? IsDealwith) {
            List<FeedbackTable> list = list = FeedbackBll.SelectAllFeedback().Where(p =>IsDealwith==null|| p.IsDealwith == (IsDealwith ?? 0)).OrderBy(p=>p.IsDealwith).ToList();
            if (list != null && list.Count() > 0)
            {
                Session["fkcount"] = list.Count();
            }
            else
            {
                Session["fkcount"] = 0;
            }
            ViewBag.pageindex = pageindex??1;
            ViewBag.IsDealwith = IsDealwith??2;
            Session["fkpagecount"] = Math.Ceiling(list.Count() / 10.0);
            Session["allfk"] = list.Skip(((pageindex ?? 1) - 1) * 10).Take(10).ToList();
            return PartialView("BackFeedbackPartial");
        }

        /// <summary>
        /// 处理反馈
        /// </summary>
        /// <param name="FeedbackID">反馈id</param>
        /// <returns></returns>
        public JsonResult FeedbackUpdate(string FeedbackID) {
            if (FeedbackBll.UpdateFeedback(Convert.ToInt32(FeedbackID)))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

    }
}