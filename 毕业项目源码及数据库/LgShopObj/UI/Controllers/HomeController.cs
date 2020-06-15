using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Models;
using Newtonsoft.Json;
//引入命名空间
using System.Net;
using System.Net.Mail;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 商城首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //获取导航栏的分类数据
            Session["Type"] = TypeTableBll.SelectTypeTable();
            //获得所有分类数据
            ViewBag.Type = TypeTableBll.SelectAllType();
            return View();
        }

        /// <summary>
        /// 商城购物车
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopCar() {
            return View();
        }

        /// <summary>
        /// 通过ajax获取类型
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTypeAjax(int index) {
            //获取数据
            var list = TypeTableBll.SelectAllType().Where(p=>p.TID==index).Select(p=>new { TypeName=p.TypeName,TID=p.TID,TypeID=p.TypeID });
            JsonSerializerSettings jsonstring = new JsonSerializerSettings();
            jsonstring.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string ret_1 = JsonConvert.SerializeObject(list,jsonstring);
            string ret_2= JsonConvert.SerializeObject(TypeTableBll.SelectIndexType(index).Select(p => new { TypeName = p.TypeName, TID = p.TID, TypeID = p.TypeID }), jsonstring);
            return Json(new { list_1=ret_1,list_2=ret_2 },JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="Emial">用户邮箱</param>
        /// <returns></returns>
        public JsonResult GetYzm(string UserEmail) {
            //发送邮件的地址，标题，内容
            string to = UserEmail;
            //创建发送邮件的q服务器对象先引用命名空间
            SmtpClient smtp = new SmtpClient("smtp.qq.com");
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;

            //发件人的地址
            MailAddress from = new MailAddress("2580497963@qq.com");
            //收件人的地址
            MailAddress t = new MailAddress(to);

            //发送的邮件来自哪里，发送给谁
            MailMessage mail = new MailMessage(from, t);
            //给邮件标题赋值
            mail.Subject = "请记住本次操作的验证码，不要泄露给他人！";
            //给邮件内容赋值
            //随机生成一个4位数的验证码，让客户输入，判断验证码是否正确
            Random r = new Random();
            string yzm = null;
            for (int i = 0; i < 4; i++)
            {
                yzm += r.Next(0, 100);
            }
            //将生成的验证码存入session中
            Session["yzm"] = yzm;
            mail.Body =  $"本次的验证码为:{yzm},请尽快完成操作！";

            try
            {
                //创建证书对象
                NetworkCredential net = new NetworkCredential("2580497963", "dzdeexcfposfecea");
                //设置资格证书
                smtp.Credentials = net;
                //发送邮件
                smtp.Send(mail);
            }
            catch (Exception)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="UserPwd">密码</param>
        /// <param name="UserCard">身份证号</param>
        /// <param name="UserPhont">电话</param>
        /// <param name="UserEmail">邮箱</param>
        /// <param name="Yzm">用户输入的验证码</param>
        /// <returns></returns>
        public JsonResult RegistAjax(string UserName,string UserPwd,string UserCard,string UserPhont,string UserEmail,string Yzm) {
            //判断验证码是否输入正确
            if (Session["yzm"].ToString().Trim().ToLower() == Yzm.Trim().ToLower())
            {
                UserInfo user = new UserInfo()
                {
                    UserName = UserName,
                    UserPassword = UserPwd,
                    UserAccount = UserPhont,
                    UserEmail = UserEmail,
                    UserAge = UserCard.GetUserBirthdays().GetUserAge(),
                    UserSex = UserCard.GetUserSex(),
                    UserPhont =UserPhont,
                    UserCard=UserCard,
                    UserBirthdays=UserCard.GetUserBirthdays(),
                    ReceivingAddress=""
                };
                if (UserInfoBll.AddUserInfo(user))
                {
                    //注册成功
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
            }
            //注册失败
            return Json(0, JsonRequestBehavior.AllowGet);
        }
    }
}