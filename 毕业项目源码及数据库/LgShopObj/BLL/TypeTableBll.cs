using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace BLL
{
    public class TypeTableBll
    {
        /// <summary>
        /// 导航栏查询分类
        /// </summary>
        /// <returns></returns>
        public static List<TypeTable> SelectTypeTable()
        {
            return TypeTableDal.SelectTypeTable();
        }

        /// <summary>
        /// 查询所有分类
        /// </summary>
        /// <returns></returns>
        public static List<TypeTable> SelectAllType()
        {
            return TypeTableDal.SelectAllType();
        }

        /// <summary>
        /// 查询指定的类型
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public static List<TypeTable> SelectIndexType(int index)
        {
            return TypeTableDal.SelectIndexType(index);
        }
    }
}
