using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace BLL
{
    public static class OrderBll
    {
        /// <summary>
        /// 新增多条订单
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool AddOrders(List<OrderTable> list,int userid,decimal price)
        {
            return OrderDal.AddOrders(list,userid,price)>0;
        }

        /// <summary>
        /// 新增单个订单
        /// </summary>
        /// <param name="ord">订单对象</param>
        /// <returns></returns>
        public static bool AddOrder(OrderTable ord)
        {
            return OrderDal.AddOrder(ord);
        }

        /// <summary>
        /// 查询一个用户的所有订单
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static List<OrderTable> SelectUserOrder(int userid)
        {
            return OrderDal.SelectUserOrder(userid);
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <returns></returns>
        public static bool IsReceiving(int orderid)
        {
            return OrderDal.IsReceiving(orderid)==1;
        }

        /// <summary>
        /// 查询一个订单
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static OrderTable SelectOneOrder(int orderid)
        {
            return OrderDal.SelectOneOrder(orderid);
        }

        /// <summary>
        /// 提交了评价
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <returns></returns>
        public static bool IsComment(int orderid)
        {
            return OrderDal.IsComment(orderid)==1;
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <returns></returns>
        public static bool OrderDel(int orderid)
        {
            return OrderDal.OrderDel(orderid)==1;
        }

    }
}
