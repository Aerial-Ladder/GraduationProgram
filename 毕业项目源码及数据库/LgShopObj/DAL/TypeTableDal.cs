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
        public static List<TypeTable> SelectTypeTable()
        {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                return db.Database.SqlQuery<TypeTable>("select * from TypeTable where TID in (select TypeID from TypeTable where TID is null)").ToList();
            }
        }

        /// <summary>
        /// 查询所有分类
        /// </summary>
        /// <returns></returns>
        public static List<TypeTable> SelectAllType()
        {
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
        public static List<TypeTable> SelectIndexType(int index)
        {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Database.SqlQuery<TypeTable>($"select * from TypeTable t1,TypeTable t2 where t1.TID=t2.TypeID  and t2.TID={index}").ToList();
            }
        }

        /// <summary>
        /// 查询所有最小级别的分类
        /// </summary>
        /// <returns></returns>
        public static List<TypeTable> SelectSmallType()
        {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                return db.Database.SqlQuery<TypeTable>(@"select * from TypeTable where tid in(
                (select typeid from TypeTable where tid in (select TypeID from TypeTable where TID is null)))").ToList();
            }
        }

        /// <summary>
        /// 根据typeid查询一个分类对象
        /// </summary>
        /// <param name="typeid">分类id</param>
        /// <returns></returns>
        public static TypeTable SelectOne(int typeid)
        {
            using (LgShopDBEntities db = new LgShopDBEntities())
            {
                return db.TypeTable.Find(typeid);
            }
        }

        /// <summary>
        /// 修改分类信息
        /// </summary>
        /// <param name="type">分类对象</param>
        /// <returns></returns>
        public static int UpdateType(TypeTable type)
        {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                try
                {
                    if (type.TID == null)
                    {
                        return db.Database.ExecuteSqlCommand($"update TypeTable set TypeName='{type.TypeName}' where TypeID={type.TypeID}");
                    }
                    else
                    {
                        return db.Database.ExecuteSqlCommand($"update TypeTable set TypeName='{type.TypeName}',TID={type.TID} where TypeID={type.TypeID}");
                    }
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 新增分类
        /// </summary>
        /// <param name="typename">类型名称</param>
        /// <param name="tid">所属分类id</param>
        /// <returns></returns>
        public static int AddType(string typename, int tid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                try
                {
                    return db.Database.ExecuteSqlCommand($"insert into TypeTable values('{typename}',{tid})");
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="typeid">分类id</param>
        /// <returns></returns>
        public static int DeleteType(int typeid) {
            using (LgShopDBEntities db = new LgShopDBEntities()) {
                try
                {
                    db.TypeTable.Remove(db.TypeTable.Find(typeid));
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
