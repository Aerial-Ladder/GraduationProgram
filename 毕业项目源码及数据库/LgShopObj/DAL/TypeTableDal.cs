using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public static class TypeTableDal
    {
        /// <summary>
        /// 导航栏查询分类
        /// </summary>
        /// <returns></returns>
        public static List<TypeTable> SelectTypeTable() {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                return db.Database.SqlQuery<TypeTable>("select * from TypeTable where TID in (select TypeID from TypeTable where TID is null)").ToList();
            }
        }

        /// <summary>
        /// 查询所有分类
        /// </summary>
        /// <returns></returns>
        public static List<TypeTable> SelectAllType() {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                //修改了false值为true
                db.Configuration.LazyLoadingEnabled = true;
                return db.TypeTable.Include("TypeTable1").ToList();
            }
        }

        /// <summary>
        /// 查询指定的类型
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public static List<TypeTable> SelectIndexType(int index) {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Database.SqlQuery<TypeTable>($"select * from TypeTable t1,TypeTable t2 where t1.TID=t2.TypeID  and t2.TID={index}").ToList();
            }
        }

    }
}
