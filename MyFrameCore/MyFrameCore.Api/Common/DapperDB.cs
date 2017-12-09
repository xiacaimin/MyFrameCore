using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyFrameCore.Api.Common
{
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
}
