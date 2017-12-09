using MyFrameCore.IDAL;
using MyFrameCore.Model;
using MySql.Data.MySqlClient;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.DAL
{
    public class BaseDAL : IBaseDAL
    {
        /// <summary>
        /// 获取泛型数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>数据集合</returns>
        public List<T> GetList<T>(Expression<Func<T, bool>> whereExpression) where T : class, new()
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                return db.Queryable<T>().Where(whereExpression).ToList();
            }
        }

        /// <summary>
        /// 根据主键字段查找数据
        /// </summary>
        /// <param name="KeyId">主键ID</param>
        /// <returns>实体对象</returns>
        public T GetModelById<T>(string KeyId) where T : class, new()
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                return db.Queryable<T>().InSingle(KeyId);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="expression">条件表达式</param>
        /// <returns>受影响行数</returns>
        public int Delete<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                return db.Deleteable<T>().Where(expression).ExecuteCommand();
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体对象</param>
        /// <returns>受影响行数</returns>
        public int Insert<T>(T model) where T : class, new()
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                return db.Insertable(model).ExecuteCommand();
            }
        }
        /// <summary>
        /// 新增数据(返回自增ID)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体对象</param>
        /// <returns>最新自增ID</returns>
        public int InsertReturnIdentity<T>(T model) where T : class, new()
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                return db.Insertable(model).ExecuteReturnIdentity();
            }
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体对象</param>
        /// <returns>受影响行数</returns>
        public int Update<T>(T model) where T : class, new()
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                return db.Updateable(model).ExecuteCommand();
            }
        }
        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响行数</returns>
        public int ExecuteSql(string sql, object parameters = null)
        {
            using (var db = SqlSugarDB.MasterDB)
            {
                return db.Ado.ExecuteCommand(sql, parameters);
            }
        }
        /// <summary>
        /// 分页整理方法
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">每页数据量</param>
        /// <param name="rows">此页数据集合</param>
        /// <param name="totalNumber">总数据量</param>
        /// <returns>分页对象</returns>
        protected PageModel<T> GetPageInfo<T>(int PageIndex, int PageSize, object rows, int totalNumber)
        {
            return new PageModel<T>
            {
                rows = rows,
                total = PageSize,
                totalCount = totalNumber,
                totalPage = GetTotalPage(PageSize, totalNumber)
            };
        }

        private int GetTotalPage(int PageSize, int totalNumber)
        {
            int num = 1;
            if (PageSize != 0 && totalNumber != 0)
            {
                return (totalNumber + PageSize - 1) / PageSize;
            }
            return num;
        }
    }

    /**************
     * 
     * 
     数据库连接方式用字符串值代替xml配置获取更安全，更利于资源释放，减少IO消耗
     * 
     * 
    **************/

    public class DapperDB
    {
        /// <summary>
        /// mysql数据库链接
        /// </summary>
        public static IDbConnection MySqlDB
        {
            get
            {
                return new MySqlConnection("Database=myframedata;Data Source=60.205.187.235;User Id=root;Password=xcm123;pooling=false;CharSet=utf8;port=3306");
            }
        }
        /// <summary>
        /// sqlserver数据库链接
        /// </summary>
        public static IDbConnection SqlServerDB
        {
            get
            {
                return new SqlConnection("");
            }
        }
    }

    public class SqlSugarDB
    {

        /// <summary>
        /// 主库
        /// </summary>
        public static SqlSugarClient MasterDB
        {
            get
            {
                
                return new SqlSugarClient(new ConnectionConfig()
                {
                    //连接字符串
                    ConnectionString = "Database=myframedata;Data Source=60.205.187.235;User Id=root;Password=xcm123;pooling=false;CharSet=utf8;port=3306",
                    //数据库类型
                    DbType =SqlSugar.DbType.MySql,//必填
                    //是否自动释放数据库默认false
                    IsAutoCloseConnection = true,
                    //初始化主键和自增列信息的方式 (InitKeyType.SystemTable表示自动从数据库读取主键自增列的信息；InitKeyType.Attribute 表示从属性中读取 主键和自增列的信息)
                    InitKeyType = InitKeyType.SystemTable
                }); ;
            }
        }

        /// <summary>
        /// 从库
        /// </summary>
        public static SqlSugarClient SlaveDB
        {
            get
            {
                return new SqlSugarClient(new ConnectionConfig()
                {
                    //连接字符串
                    //ConnectionString = "Data Source=.;Initial Catalog=MyData;Persist Security Info=True;User ID=sa;Password=123456;MultipleActiveResultSets=True",//PubConstant.ConnectionString",
                    //ConnectionString = "Database=myframedata;Data Source=127.0.0.1;User Id=root;Password=root;pooling=false;CharSet=utf8;port=3307",
                    ConnectionString = "server=192.168.1.150;Database=mydata;Uid=root;Pwd=123456;", //必填
                    //数据库类型
                    DbType = SqlSugar.DbType.MySql,//必填
                    //是否自动释放数据库默认false
                    IsAutoCloseConnection = true,
                    //初始化主键和自增列信息的方式 (InitKeyType.SystemTable表示自动从数据库读取主键自增列的信息；InitKeyType.Attribute 表示从属性中读取 主键和自增列的信息)
                    InitKeyType = InitKeyType.SystemTable
                });
            }
        }
    }
}
