using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using UI.Filter;
using System.IO;
using UI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace UI.Controllers
{
    [Admin]
    public class BacksTageController : Controller
    {
        // GET: BacksTage
        public ActionResult Index()
        {
            Session["li_1"] = 0;
            Session["li_2"] = 0;
            //查询所有用户信息
            List<UserInfo> list = UserInfoBll.SelectAllUser();
            if (list != null && list.Count() > 0)
            {
                Session["usercount"] = list.Count();
            }
            else
            {
                Session["usercount"] = 0;
            }
            if (list != null && list.Count() > 0)
            {
                Session["userpagecount"] = Math.Ceiling(list.Count() / 10.0);
                Session["alluserlist"] = list.Take(10).ToList();
            }
            return View();
        }

        /// <summary>
        /// 后台用户列表分布视图
        /// </summary>
        /// <returns></returns>
        public ActionResult UserBackIndex(int? pageindex, string text = "")
        {
            List<UserInfo> list = UserInfoBll.SelectAllUser().Where(p => p.UserName.Contains(text) || p.UserAccount.Contains(text) || text == "").ToList();
            if (list != null && list.Count() > 0)
            {
                Session["usercount"] = list.Count();
            }
            else
            {
                Session["usercount"] = 0;
            }
            Session["userpagecount"] = Math.Ceiling(list.Count() / 10.0);
            ViewBag.pageindex = pageindex??1;
            ViewBag.text = text;
            Session["alluserlist"] = list.Skip(((pageindex ?? 1) - 1) * 10).Take(10).ToList();
            return PartialView("UserBackIndex");
        }

        /// <summary>
        /// 查询单个用户的信息
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public JsonResult UserDesc(string UserID)
        {
            UserInfo user = UserInfoBll.SelectUser(Convert.ToInt32(UserID));
            if (user != null)
            {
                return Json(new { user = user, b = string.Format("{0:D}", user.UserBirthdays.Date) }, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 管理员退出
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminOut()
        {
            Session.Remove("admin");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 管理员修改用户信息
        /// </summary>
        /// <param name="UserPhoto">用户头像</param>
        /// <param name="user">用户对象</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="text">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserUpdate(HttpPostedFileBase UserPhoto, UserInfo user, int? pageindex, string text = "")
        {
            //修改用户头像
            if (UserPhoto != null && UserPhoto.ContentLength != 0)
            {
                string fileName = Path.GetFileName(UserPhoto.FileName);
                if (UserInfoBll.UpdateUserPhoto(fileName, user.UserID, true))
                {
                    //保存文件
                    //应用程序需要有服务器UploadFile文件夹的读写权限
                    UserPhoto.SaveAs(Server.MapPath("~/Content/Images/" + fileName));
                }
            }
            UserInfo user1 = UserInfoBll.SelectUser(user.UserID);
            UserInfo updateUser;
            if (user1.UserCard != user.UserCard)
            {
                //用户修改了身份证号码
                updateUser = new UserInfo()
                {
                    UserID = user.UserID,
                    UserName = user.UserName,
                    UserAccount = user.UserPhont,
                    UserEmail = user.UserEmail,
                    UserAge = user.UserCard.GetUserBirthdays().GetUserAge(),
                    UserSex = user.UserCard.GetUserSex(),
                    UserPhont = user.UserPhont,
                    UserCard = user.UserCard,
                    UserBirthdays = user.UserCard.GetUserBirthdays(),
                    ReceivingAddress = user.ReceivingAddress
                };
            }
            else
            {
                updateUser = new UserInfo()
                {
                    UserID = user.UserID,
                    UserName = user.UserName,
                    UserAccount = user.UserPhont,
                    UserEmail = user.UserEmail,
                    UserAge = user1.UserAge,
                    UserSex = user1.UserSex,
                    UserPhont = user.UserPhont,
                    UserCard = user.UserCard,
                    UserBirthdays = user1.UserBirthdays,
                    ReceivingAddress = user.ReceivingAddress
                };
            }
            //调用修改的方法
            UserInfoBll.UserInfoUpdate(updateUser);
            return RedirectToAction("UserBackIndex", "BacksTage", new { pageindex = pageindex, text = text });
        }

        /// <summary>
        /// 删除某个用户
        /// </summary>
        /// <param name="UserID">用户id</param>
        /// <returns></returns>
        public JsonResult UserDel(string UserID)
        {
            UserInfo user = UserInfoBll.SelectUser(Convert.ToInt32(UserID));
            if (user.UserWallet > 0)
            {
                //该用户有余额无法删除
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (UserInfoBll.UserDelete(Convert.ToInt32(UserID)))
                {
                    return Json(2, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增用户界面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUser()
        {
            Session["li_1"] = 0;
            Session["li_2"] = 1;
            return View();
        }


        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="UserPwd">密码</param>
        /// <param name="UserCard">身份证号</param>
        /// <param name="UserPhont">手机号</param>
        /// <param name="UserEmail">邮箱</param>
        /// <returns></returns>
        public JsonResult AddUserAjax(string UserName, string UserPwd, string UserCard, string UserPhont, string UserEmail)
        {
            UserInfo user = new UserInfo()
            {
                UserName = UserName,
                UserPassword = UserPwd,
                UserAccount = UserPhont,
                UserEmail = UserEmail,
                UserAge = UserCard.GetUserBirthdays().GetUserAge(),
                UserSex = UserCard.GetUserSex(),
                UserPhont = UserPhont,
                UserCard = UserCard,
                UserBirthdays = UserCard.GetUserBirthdays(),
                ReceivingAddress = ""
            };
            if (UserInfoBll.AddUserInfo(user))
            {
                //新增成功
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 后台商品管理
        /// </summary>
        /// <returns></returns>
        public ActionResult BackGoodsIndex()
        {
            Session["li_1"] = 1;
            Session["li_2"] = 0;
            //商品的图片
            Session["GoodsPhoto"] = GoodsPhotoBll.SelectAllGoodsPhoto();
            //查询所有商品
            List<GoodsTable> list = GoodsBll.SelectAllGoods();
            if (list != null && list.Count() > 0)
            {
                Session["goodscount"] = list.Count();
            }
            else
            {
                Session["goodscount"] = 0;
            }
            Session["goodpagecount"] = Math.Ceiling(list.Count() / 8.0);
            Session["allgoods"] = list.Take(8).ToList();
            return View();
        }

        /// <summary>
        /// 商品列表分布视图
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsIndexPartial(int? pageindex, int typeid, string text = "")
        {
            List<GoodsTable> list = null;
            if (typeid == 0)
            {
                list = GoodsBll.SelectAllGoods().Where(p => p.GoodsName.Contains(text)).ToList();
            }
            else
            {
                //查询第一大类的商品
                list = GoodsBll.SelectType1GoodsAdmin(typeid).Where(p => p.GoodsName.Contains(text)).ToList();
                //查询所有分类
                List<TypeTable> typelist = TypeTableBll.SelectAllType();
                //防止错误赋值给typetable导航属性
                list.ForEach(p => p.TypeTable = typelist.Where(a => a.TypeID == p.TID).ToList()[0]);
            }
            if (list != null && list.Count() > 0)
            {
                Session["goodscount"] = list.Count();
            }
            else
            {
                Session["goodscount"] = 0;
            }
            Session["goodpagecount"] = Math.Ceiling(list.Count() / 8.0);
            ViewBag.pageindex = pageindex;
            ViewBag.text = text;
            ViewBag.typeid = typeid;
            Session["allgoods"] = list.Skip(((pageindex ?? 1) - 1) * 8).Take(8).ToList();
            return PartialView("GoodsIndexPartial");
        }

        /// <summary>
        /// 查询一个商品的详情
        /// </summary>
        /// <param name="GoodsID">商品id</param>
        /// <returns></returns>
        public JsonResult GoodsDesc(string GoodsID)
        {
            //查询商品的信息
            GoodsTable good = GoodsBll.SelectAllGoods().SingleOrDefault(p => p.GoodsID == Convert.ToInt32(GoodsID));
            //查询商品对应的所有图片
            var goodsphoto = GoodsPhotoBll.SelectAllGoodsPhoto().Where(p => p.GoodsID == Convert.ToInt32(GoodsID)).Select(p => new { RID = p.RID, GoodsID = p.GoodsID, PhotoPath = p.PhotoPath }).ToList();
            //查询所有最小的分类
            var typelist = TypeTableBll.SelectSmallType().Select(p => new { typeid = p.TypeID, typename = p.TypeName });
            JsonSerializerSettings jsonstring = new JsonSerializerSettings();
            jsonstring.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string list_1 = JsonConvert.SerializeObject(good, jsonstring);
            string list_2 = JsonConvert.SerializeObject(goodsphoto, jsonstring);
            string list_3 = JsonConvert.SerializeObject(typelist, jsonstring);
            return Json(new { Good = list_1, GoodPhoto = list_2, GoodType = list_3 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="goodsfile">商品图片（多选）</param>
        /// <param name="good">商品对象</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="typeid">分类id</param>
        /// <param name="text">查询内容</param>
        /// <returns></returns>
        public ActionResult GoodsUpdate(GoodsTable good, int? pageindex, int typeid, string text = "")
        {
            //添加商品图片
            if (Request.Files.Count>0)
            {
                string[] fileTypeStr = { "image/gif","image/png","image/jpeg","image/jpg","image/bmp"};
                List<string> strlist = new List<string>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    if (fileTypeStr.Contains(Request.Files[i].ContentType))
                    {
                        string fileName = Path.GetFileName(Request.Files[i].FileName);
                        strlist.Add(fileName);
                    }
                }
                if (GoodsPhotoBll.AddGoodsPhoto(good.GoodsID, strlist))
                {
                    //保存文件
                    //应用程序需要有服务器UploadFile文件夹的读写权限
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        Request.Files[i].SaveAs(Server.MapPath("~/Content/GoodImgs/" + Request.Files[i].FileName));
                    }
                }
            }
            //修改商品信息
            GoodsBll.UpdateGoods(good);
            return RedirectToAction("GoodsIndexPartial", "BacksTage", new { pageindex = pageindex, text = text,typeid=typeid });
        }

        /// <summary>
        /// 商品图片删除
        /// </summary>
        /// <param name="RID"></param>
        /// <returns></returns>
        public JsonResult GoodsImgDel(string RID) {
            return Json(GoodsPhotoBll.GoodsPhotoDel(Convert.ToInt32(RID)), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 商品出售或停售
        /// </summary>
        /// <param name="GoodsID">商品id</param>
        /// <returns></returns>
        public JsonResult GoodsDel(string GoodsID) {
            if (GoodsBll.GoodsDel(Convert.ToInt32(GoodsID)))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增商品界面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddGoods() {
            Session["li_1"] = 1;
            Session["li_2"] = 1;
            //查询所有最小的分类
            List<TypeTable> typelist = TypeTableBll.SelectSmallType().Select(p => new TypeTable{ TypeID=p.TypeID,TypeName=p.TypeName }).ToList();
            Session["smalltype"] = typelist;
            return View();
        }

        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="Arr">图片数组</param>
        /// <param name="GoodsPrice">现价</param>
        /// <param name="GoodsName">名称</param>
        /// <param name="OldGoodsPrice">原价</param>
        /// <param name="GoodsInventory">库存</param>
        /// <param name="TID">分类id</param>
        /// <param name="GoodsDescribe">描述</param>
        /// <returns></returns>
        public JsonResult AddGoodsAjax() {
            GoodsTable good = new GoodsTable()
            {
                GoodsName= Request.Form["goodsname"],
                GoodsPrice=Convert.ToDecimal(Request.Form["goodsprice"]),
                OldGoodsPrice=Convert.ToDecimal(Request.Form["oldgoodsprice"]),
                TID=Convert.ToInt32(Request.Form["tid"]),
                GoodsInventory=Convert.ToInt32(Request.Form["goodsinventory"]),
                GoodsDescribe= Request.Form["goodsdescribe"]
            };
            List<string> list = new List<string>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                list.Add(Path.GetFileName(Request.Files[i].FileName));
            }
            //调用新增方法
            if (GoodsBll.AddGoods(good))
            {
                //查询最后一条商品
                GoodsTable lastgood = GoodsBll.SelectAllGoods().LastOrDefault();
                //调用新增商品图片方法
                if (GoodsPhotoBll.AddGoodsPhoto(lastgood.GoodsID, list))
                {
                    //保存图片
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        Request.Files[i].SaveAs(Server.MapPath("~/Content/GoodImgs/" + Request.Files[i].FileName));
                    }
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

    }
}