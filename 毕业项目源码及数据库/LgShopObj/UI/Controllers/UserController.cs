using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using UI.Models;
using BLL;
using System.IO;
using UI.Filter;

namespace UI.Controllers
{
    public class UserController : Controller
    {
        [GoLogin]
        // GET: User
        public ActionResult UserIndex()
        {
            //查询用户的订单
            Session["userorder"] = OrderBll.SelectUserOrder(Convert.ToInt32(Session["userid"]));
            //获取所有图片
            Session["goodphotos"] = GoodsPhotoBll.SelectAllGoodsPhoto();
            //获得用户未查看的消息
            try
            {
                ViewBag.nolook = NoticeBll.SelectUserNotice(Convert.ToInt32(Session["userid"])).Where(p => p.IsLook == 0).ToList();
            }
            catch (Exception)
            {
                ViewBag.nolook = null;
            }

            return View(UserInfoBll.SelectUser(Convert.ToInt32(Session["userid"])));
        }

        /// <summary>
        /// 用户退出，清空session
        /// </summary>
        /// <returns></returns>
        public ActionResult UserOut()
        {
            Session.Remove("user");
            Session.Remove("userid");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 用户修改基本信息
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="UserPhont">用户手机号码</param>
        /// <param name="UserCard">身份证号码</param>
        /// <param name="UserEmail">用户邮箱</param>
        /// <param name="ReceivingAddress">收货地址</param>
        /// <returns></returns>
        public JsonResult UserUpdate(string UserName, string UserPhont, string UserCard, string UserEmail, string ReceivingAddress)
        {
            UserInfo user = Session["user"] as UserInfo;
            UserInfo updateUser;
            if (user.UserCard != UserCard)
            {
                //用户修改了身份证号码
                updateUser = new UserInfo()
                {
                    UserID = user.UserID,
                    UserName = UserName,
                    UserAccount = UserPhont,
                    UserEmail = UserEmail,
                    UserAge = UserCard.GetUserBirthdays().GetUserAge(),
                    UserSex = UserCard.GetUserSex(),
                    UserPhont = UserPhont,
                    UserCard = UserCard,
                    UserBirthdays = UserCard.GetUserBirthdays(),
                    ReceivingAddress = ReceivingAddress
                };
            }
            else
            {
                updateUser = new UserInfo()
                {
                    UserID = user.UserID,
                    UserName = UserName,
                    UserAccount = UserPhont,
                    UserEmail = UserEmail,
                    UserAge = user.UserAge,
                    UserSex = user.UserSex,
                    UserPhont = UserPhont,
                    UserCard = UserCard,
                    UserBirthdays = user.UserBirthdays,
                    ReceivingAddress = ReceivingAddress
                };
            }
            //调用修改的方法
            if (UserInfoBll.UserInfoUpdate(updateUser))
            {
                Session["user"] = UserInfoBll.SelectUser(user.UserID);
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="file_1">图片路径</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserPhotoUpdate(HttpPostedFileBase file_1, string isuserphoto)
        {
            if (file_1 != null)
            {
                if (file_1.ContentLength == 0)
                {
                    //文件大小（以字节为单位）为0，返回到视图
                    return RedirectToAction("UserIndex");
                }
                else
                {
                    UserInfo user = Session["user"] as UserInfo;
                    string fileName = Path.GetFileName(file_1.FileName);
                    if (isuserphoto == "0")
                    {
                        UserInfoBll.UpdateUserPhoto(fileName, user.UserID, true);
                    }
                    else
                    {
                        UserInfoBll.UpdateUserPhoto(fileName, user.UserID, false);
                    }
                    Session["user"] = UserInfoBll.SelectUser(user.UserID);
                    //保存文件
                    //应用程序需要有服务器UploadFile文件夹的读写权限
                    file_1.SaveAs(Server.MapPath("~/Content/Images/" + fileName));
                }
            }
            return RedirectToAction("UserIndex");
        }

        /// <summary>
        /// 用户反馈界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserFeedback()
        {
            return View();
        }

        /// <summary>
        /// 用户反馈界面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UserFeedback(string UserFeedback)
        {
            //调用新增反馈方法
            if (FeedbackBll.AddFeedback(UserFeedback, Session["userid"].ToString()))
            {
                //新增成功
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 我的订单分布视图
        /// </summary>
        /// <returns></returns>
        public ActionResult MyOrder(string num = "0")
        {
            List<OrderTable> list = OrderBll.SelectUserOrder(Convert.ToInt32(Session["userid"]));
            if (num == "1")
            {
                list = list.OrderBy(p => p.GetTime).ToList();
            }
            else if (num == "2")
            {
                list = list.OrderByDescending(p => p.OrderAmount).ToList();
            }
            else if (num == "3")
            {
                list = list.Where(p => p.IsReceiving == 0).ToList();
            }
            else if (num == "4")
            {
                list = list.Where(p => p.IsComment == 0).ToList();
            }
            else if (num == "5")
            {
                list = list.Where(p => p.IsReceiving == 1).ToList();
            }
            else if (num == "6")
            {
                list = list.Where(p => p.IsComment == 1).ToList();
            }
            Session["userorder"] = list;
            ViewBag.index = Convert.ToInt32(num);
            return PartialView("MyOrder");
        }

        /// <summary>
        /// 我的评价分布视图
        /// </summary>
        /// <returns></returns>
        public ActionResult MyComment()
        {
            return PartialView("MyComment", CommentBll.SelectUserComment(Convert.ToInt32(Session["userid"])));
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="OrderId">订单id</param>
        /// <returns></returns>
        public JsonResult IsReceiving(string OrderId)
        {
            if (OrderBll.IsReceiving(Convert.ToInt32(OrderId)))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询商品订单信息
        /// </summary>
        /// <param name="OrderId">订单id</param>
        /// <returns></returns>
        public JsonResult AjaxSelect(string OrderId)
        {
            OrderTable ord = OrderBll.SelectOneOrder(Convert.ToInt32(OrderId));
            //获取图片路径
            string filename = GoodsPhotoBll.SelectAllGoodsPhoto().FirstOrDefault(p => p.GoodsID == ord.GoodsID).PhotoPath;
            return Json(new { oid = OrderId, gid = ord.GoodsID, gname = ord.GoodsTable.GoodsName, gimg = filename }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        /// <summary>
        /// 用户提交评论
        /// </summary>
        /// <param name="OrderId">订单id</param>
        /// <param name="GoodId">商品id</param>
        /// <param name="StarRating">星级</param>
        /// <param name="Content">评论内容</param>
        /// <returns></returns>
        public JsonResult IsComment(string OrderId, string GoodId, string StarRating, string Content)
        {
            int userid = Convert.ToInt32(Session["userid"]);
            //如果都为true
            if (CommentBll.AddUserComment(userid, Convert.ToInt32(GoodId), Content, Convert.ToInt32(StarRating)) && OrderBll.IsComment(Convert.ToInt32(OrderId)))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="OrderId">订单id</param>
        /// <returns></returns>
        public JsonResult OrderDel(string OrderId)
        {
            if (OrderBll.OrderDel(Convert.ToInt32(OrderId)))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        /// <summary>
        /// 删除评价
        /// </summary>
        /// <param name="CommentID"></param>
        /// <returns></returns>
        public JsonResult CommentDel(int CommentID)
        {
            //调用方法删除
            if (CommentBll.DeleteUserComment(CommentID))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 我的账户分布视图
        /// </summary>
        /// <returns></returns>
        public ActionResult MyWallet()
        {
            Session["user"] = UserInfoBll.SelectUser(Convert.ToInt32(Session["userid"]));
            UserInfo user = Session["user"] as UserInfo;
            List<OrderTable> list = OrderBll.SelectUserOrder(user.UserID);
            //账户余额
            ViewBag.UserWallet = user.UserWallet;
            //累计消费
            decimal countmoeny = 0;
            list.ForEach(p => countmoeny += p.OrderAmount ?? 0);
            ViewBag.countmoeny = countmoeny;
            //本月消费
            decimal monthmoeny = 0;
            list.Where(p => p.GetTime.Value.Year == DateTime.Now.Year && p.GetTime.Value.Month == DateTime.Now.Month).ToList().ForEach(p => monthmoeny += p.OrderAmount ?? 0);
            ViewBag.monthmoeny = monthmoeny;
            return PartialView("MyWallet", list);
        }

        [HttpPost]
        /// <summary>
        /// 用户充值
        /// </summary>
        /// <param name="Money">充值的金额</param>
        /// <returns></returns>
        public JsonResult AddWallet(string Money)
        {
            //调用充值方法
            if (UserInfoBll.UserAddWallet(Convert.ToDouble(Money), Convert.ToInt32(Session["userid"])))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 我的反馈分布视图
        /// </summary>
        /// <returns></returns>
        public ActionResult MyFeedback()
        {
            List<FeedbackTable> list = FeedbackBll.SeleteUserFeedback(Convert.ToInt32(Session["userid"]));
            return PartialView("MyFeedback", list);
        }

        [HttpPost]
        /// <summary>
        /// 用户删除反馈
        /// </summary>
        /// <param name="FeedbackID">反馈id</param>
        /// <returns></returns>
        public ActionResult FeedbackDel(int FeedbackID)
        {
            //删除反馈记录
            if (FeedbackBll.UserFeedbackDal(FeedbackID))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("MyFeedback","User");
        }

        /// <summary>
        /// 我的收藏分布视图
        /// </summary>
        /// <returns></returns>
        public ActionResult MyCollection()
        {
            List<CollectionTable> list = CollectionBll.SelectUserCollection(Convert.ToInt32(Session["userid"])).Where(p => p.IsCollection == 1).ToList();
            return PartialView("MyCollection", list);
        }

        /// <summary>
        /// 我的消息分布视图
        /// </summary>
        /// <returns></returns>
        public ActionResult MyNotice()
        {
            List<NoticeTable> list = NoticeBll.SelectUserNotice(Convert.ToInt32(Session["userid"]));
            if (list != null && list.Count() > 0)
            {
                list = list.OrderByDescending(p => p.NoticeID).ToList();
            }
            //修改用户未查看的消息为已经查看
            NoticeBll.UpdateUserNotice(Convert.ToInt32(Session["userid"]));
            return PartialView("MyNotice", list);
        }

        [HttpPost]
        /// <summary>
        /// 用户删除消息
        /// </summary>
        /// <param name="NoticeID">消息id</param>
        /// <returns></returns>
        public JsonResult NoticeDel(string NoticeID)
        {
            if (NoticeBll.DeleteNotice(Convert.ToInt32(NoticeID)))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

    }
}