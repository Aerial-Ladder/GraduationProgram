using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public static class OrderDal
    {
        /// <summary>
        /// 新增多条订单
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int AddOrders(List<OrderTable> list, int userid, decimal price)
        {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                try
                {
                    //获取用户对象
                    UserInfo user = db.UserInfo.Find(userid);
                    //新增订单记录
                    foreach (var item in list)
                    {
                        //获取商品对象
                        GoodsTable good = db.GoodsTable.Find(item.GoodsID);
                        db.Database.ExecuteSqlCommand($"insert into OrderTable values({userid},{item.GoodsID},{item.GoodsNum},'{DateTime.Now}',{item.OrderAmount},0,0)");
                        //删除购物车记录
                        db.Database.ExecuteSqlCommand($"delete from ShoppingCartTable where userid={userid} and GoodsID={item.GoodsID}");
                        //商品库存减少
                        db.GoodsTable.SingleOrDefault(p => p.GoodsID == item.GoodsID).GoodsInventory -= item.GoodsNum ?? 0;
                        //新增商品发货消息
                        db.Database.ExecuteSqlCommand($"insert into NoticeTable values({userid},'商品发货通知','尊敬的{user.UserName}用户，您于{DateTime.Now.ToLongDateString().ToString()}购买的商品{good.GoodsName}已发货，请注意查收！','{DateTime.Now}',0)");
                    }
                    //扣除用户余额
                    db.UserInfo.Find(userid).UserWallet -= price;
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 新增单个订单
        /// </summary>
        /// <param name="ord">订单对象</param>
        /// <returns></returns>
        public static bool AddOrder(OrderTable ord)
        {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                try
                {
                    //获取用户对象
                    UserInfo user = db.UserInfo.Find(ord.UserID);
                    //获取商品对象
                    GoodsTable good = db.GoodsTable.Find(ord.GoodsID);
                    db.OrderTable.Add(ord);
                    //商品库存减少
                    db.GoodsTable.SingleOrDefault(p => p.GoodsID == ord.GoodsID).GoodsInventory -= ord.GoodsNum ?? 0;
                    db.SaveChanges();
                    //扣除用户余额
                    db.UserInfo.Find(ord.UserID).UserWallet -= ord.OrderAmount;
                    //新增商品发货消息
                    db.Database.ExecuteSqlCommand($"insert into NoticeTable values({ord.UserID},'商品发货通知','尊敬的{ord.UserInfo.UserName}用户，您于{DateTime.Now.ToLongDateString().ToString()}购买的商品{good.GoodsName}已发货，请注意查收！','{DateTime.Now}',0)");
                    return db.SaveChanges() == 1;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 查询一个用户的所有订单
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static List<OrderTable> SelectUserOrder(int userid)
        {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                return db.OrderTable.Include("GoodsTable").Where(p => p.UserID == userid).ToList();
            }
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <returns></returns>
        public static int IsReceiving(int orderid)
        {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                try
                {
                    //商品的热度增加
                    OrderTable ord = db.OrderTable.Find(orderid);
                    db.GoodsTable.Find(ord.GoodsID).GoodsHot += ord.GoodsNum;
                    db.SaveChanges();
                    return db.Database.ExecuteSqlCommand($"update OrderTable set IsReceiving=1 where OrderID={orderid}");
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 查询一个订单
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static OrderTable SelectOneOrder(int orderid)
        {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                try
                {
                    return db.OrderTable.Include("GoodsTable").SingleOrDefault(p => p.OrderID == orderid);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 提交了评价
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <returns></returns>
        public static int IsComment(int orderid)
        {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                try
                {
                    return db.Database.ExecuteSqlCommand($"update OrderTable set IsComment=1 where OrderID={orderid}");
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <returns></returns>
        public static int OrderDel(int orderid)
        {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                try
                {
                    db.OrderTable.Remove(db.OrderTable.Find(orderid));
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

    }
}
