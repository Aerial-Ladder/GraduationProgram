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
        [Admin]
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


        /// <summary>
        /// 后台公告界面
        /// </summary>
        /// <returns></returns>
        public ActionResult BackNoticeIndex() {
            Session["li_1"] = 4;
            Session["li_2"] = 0;
            //查询所有反馈
            List<NoticeTable> list = NoticeBll.SelectAllNotice().Where(p => p.UserID == null).ToList();
            if (list != null && list.Count() > 0)
            {
                Session["noticecount"] = list.Count();
            }
            else
            {
                Session["noticecount"] = 0;
            }
            ViewBag.IsDealwith = 2;
            Session["noticepagecount"] = Math.Ceiling(list.Count() / 10.0);
            Session["allnotice"] = list.Take(10).ToList();
            Session["noticeuserlist"] = UserInfoBll.SelectAllUser();
            return View();
        }

        /// <summary>
        /// 后台公告分布视图
        /// </summary>
        /// <returns></returns>
        public ActionResult BackNoticePartial(int? pageindex,int? LookType) {
            int index = LookType ?? 0;
            List<NoticeTable> list = new List<NoticeTable>();
            if (index == 0)
            {
                list = NoticeBll.SelectAllNotice().Where(p => p.UserID == null).ToList();
            }
            else {
                list = NoticeBll.SelectAllNotice().Where(p => p.UserID != null).ToList();
            }
            if (list != null && list.Count() > 0)
            {
                Session["noticecount"] = list.Count();
            }
            else
            {
                Session["noticecount"] = 0;
            }
            ViewBag.LookType = index;
            ViewBag.pageindex = pageindex ?? 1;
            Session["noticepagecount"] = Math.Ceiling(list.Count() / 10.0);
            Session["allnotice"] = list.Skip(((pageindex ?? 1) - 1) * 10).Take(10).ToList();
            return PartialView("BackNoticePartial");
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="NoticeID">公告id</param>
        /// <returns></returns>
        public JsonResult NoticeDel(string NoticeID) {
            if (NoticeBll.DeleteNotice(Convert.ToInt32(NoticeID)))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增公告/消息
        /// </summary>
        /// <param name="UserID">用户id</param>
        /// <param name="NoticeTitle">标题</param>
        /// <param name="NoticeConten">内容</param>
        /// <returns></returns>
        public JsonResult NoticeAdd(string UserID,string NoticeTitle,string NoticeConten) {
            NoticeTable notice = new NoticeTable() { 
            UserID=Convert.ToInt32(UserID),
            NoticeTitle=NoticeTitle,
            NoticeContent=NoticeConten
            };
            if (NoticeBll.AddNotice(notice))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 后台评论界面
        /// </summary>
        /// <returns></returns>
        public ActionResult BackCommentIndex() {
            Session["li_1"] = 5;
            Session["li_2"] = 0;
            //查询所有评论
            List<CommentTable> list = CommentBll.SelectAllComment().OrderByDescending(p=>p.IsTop).ToList();
            if (list != null && list.Count() > 0)
            {
                Session["plcount"] = list.Count();
            }
            else
            {
                Session["plcount"] = 0;
            }
            ViewBag.SelectType = 0;
            Session["plpagecount"] = Math.Ceiling(list.Count() / 10.0);
            Session["allpl"] = list.Take(10).ToList();
            //商品图片
            Session["GoodsPhoto"] = GoodsPhotoBll.SelectAllGoodsPhoto();
            return View();
        }

        /// <summary>
        /// 后台评论分布视图
        /// </summary>
        /// <returns></returns>
        public ActionResult BackCommentPartial(int? pageindex,int? SelectType) {
            int index = SelectType ?? 0;
            List<CommentTable> list = new List<CommentTable>();
            if (index == 0)
            {
                list = CommentBll.SelectAllComment().OrderByDescending(p => p.IsTop).ToList();
            }
            else if (index == 5)
            {
                //查询违规评论
                list = CommentBll.SelectAllComment().Where(p=>p.Reportingnums>0).OrderByDescending(p => p.IsTop).OrderByDescending(p=>p.Reportingnums).ToList();
            }
            else
            {
                //查询第一大类的商品评论
                list = CommentBll.SelectByTidComment(index).OrderByDescending(p => p.IsTop).ToList();
                //增加userinfo对象，防止报错
                list.ForEach(p => p.UserInfo = UserInfoBll.SelectUser(p.UserID ?? 1));
            }
            if (list != null && list.Count() > 0)
            {
                Session["plcount"] = list.Count();
            }
            else
            {
                Session["plcount"] = 0;
            }
            ViewBag.SelectType = index;
            ViewBag.pageindex = pageindex ?? 1;
            Session["plpagecount"] = Math.Ceiling(list.Count() / 10.0);
            Session["allpl"] = list.Skip(((pageindex ?? 1) - 1) * 10).Take(10).ToList();
            return PartialView("BackCommentPartial");
        }

        /// <summary>
        /// 修改评论是否置顶
        /// </summary>
        /// <param name="CommentID">评论id</param>
        /// <param name="IsTop">是否置顶</param>
        /// <returns></returns>
        public JsonResult UpdateTop(string CommentID,string IsTop) {
            if (CommentBll.UpdateIsTop(Convert.ToInt32(CommentID), IsTop))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 管理员删除用户评论
        /// </summary>
        /// <param name="CommentID">评论id</param>
        /// <returns></returns>
        public JsonResult DelComment(string CommentID)
        {
            if (CommentBll.CommentDelAdmin(Convert.ToInt32(CommentID)))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

    }
}