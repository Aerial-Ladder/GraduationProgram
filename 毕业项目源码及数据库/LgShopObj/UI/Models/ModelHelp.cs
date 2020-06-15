using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public static class ModelHelp
    {
        /// <summary>
        /// 获取用户生日的扩展方法
        /// </summary>
        /// <param name="UserCard">用户身份证号</param>
        /// <returns></returns>
        public static DateTime GetUserBirthdays(this string UserCard)
        {
            //获取年月日
            string Year = UserCard.Substring(6, 4);
            string Month = UserCard.Substring(10, 2);
            string Day = UserCard.Substring(12, 2);
            string dateString = $"{Year}{Month}{Day}";
            //返回时间类型
            return DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 获取用户性别
        /// </summary>
        /// <param name="UserCard">用户身份证</param>
        /// <returns></returns>
        public static string GetUserSex(this string UserCard)
        {
            if (Convert.ToInt32(UserCard.Substring(17, 1)) % 2 == 0)
            {
                return "男";
            }
            else {
                return "女";
            }
        }

        /// <summary>
        /// 获取用户年龄
        /// </summary>
        /// <param name="datetime">用户生日</param>
        /// <returns></returns>
        public static int GetUserAge(this DateTime datetime)
        {
            int nowyear = DateTime.Now.Year;
            int nowmonth = DateTime.Now.Month;
            int userage = nowyear - datetime.Year;
            if (nowmonth <= datetime.Month)
            {
                if (nowmonth < datetime.Month)
                {
                    userage--;
                }
                else {
                    if (DateTime.Now.Day < datetime.Day)
                    {
                        userage--;
                    }
                }
            }
            return userage;
        }
    }
}