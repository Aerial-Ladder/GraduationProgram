using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using UI.Models;
using BLL;

namespace UI.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserIndex()
        {
            return View();
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
    }
}