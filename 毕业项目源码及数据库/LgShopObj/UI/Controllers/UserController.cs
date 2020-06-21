using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using UI.Models;
using BLL;
using System.IO;

namespace UI.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserIndex()
        {
            return View(UserInfoBll.SelectUser(Convert.ToInt32(Session["userid"])));
        }

        /// <summary>
        /// 用户退出，清空session
        /// </summary>
        /// <returns></returns>
        public ActionResult UserOut()
        {
            Session.Remove("user");
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
    }
}