﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.IDAL
{
    public interface IBaseDAL
    {
        /// <summary>
        /// 获取泛型数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>数据集合</returns>
        List<T> GetList<T>(Expression<Func<T, bool>> whereExpression) where T : class, new();


        /// <summary>
        /// 根据主键字段查找数据
        /// </summary>
        /// <param name="KeyId">主键ID</param>
        /// <returns>实体对象</returns>
        T GetModelById<T>(string KeyId) where T : class, new();


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="expression">条件表达式</param>
        /// <returns>受影响行数</returns>
        int Delete<T>(Expression<Func<T, bool>> expression) where T : class, new();


        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体对象</param>
        /// <returns>受影响行数</returns>
        int Insert<T>(T model) where T : class, new();
        /// <summary>
        /// 新增数据(返回自增ID)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体对象</param>
        /// <returns>最新自增ID</returns>
        int InsertReturnIdentity<T>(T model) where T : class, new();

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体对象</param>
        /// <returns>受影响行数</returns>
        int Update<T>(T model) where T : class, new();

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响行数</returns>
        int ExecuteSql(string sql, object parameters = null);

    }
}