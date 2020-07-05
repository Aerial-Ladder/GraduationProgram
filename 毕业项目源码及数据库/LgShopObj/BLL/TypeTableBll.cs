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

        /// <summary>
        /// 查询所有最小级别的分类
        /// </summary>
        /// <returns></returns>
        public static List<TypeTable> SelectSmallType()
        {
            return TypeTableDal.SelectSmallType();
        }

        /// <summary>
        /// 根据typeid查询一个分类对象
        /// </summary>
        /// <param name="typeid">分类id</param>
        /// <returns></returns>
        public static TypeTable SelectOne(int typeid)
        {
            return TypeTableDal.SelectOne(typeid);
        }

        /// <summary>
        /// 修改分类信息
        /// </summary>
        /// <param name="type">分类对象</param>
        /// <returns></returns>
        public static bool UpdateType(TypeTable type)
        {
            return TypeTableDal.UpdateType(type)==1;
        }

        /// <summary>
        /// 新增分类
        /// </summary>
        /// <param name="typename">类型名称</param>
        /// <param name="tid">所属分类id</param>
        /// <returns></returns>
        public static bool AddType(string typename, int tid)
        {
            return TypeTableDal.AddType(typename,tid)==1;
        }


        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="typeid">分类id</param>
        /// <returns></returns>
        public static bool DeleteType(int typeid)
        {
            return TypeTableDal.DeleteType(typeid)==1;
        }

    }
}
