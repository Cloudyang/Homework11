using Bussiness.Interface.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Interface
{
    public interface IService<T> : IDisposable
        where T : class
    {
        /// <summary>
        /// 根据Id查询实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find(int id);

        /// <summary>
        /// 提供对单表的查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<T> Set();

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        IQueryable<T> Query(Expression<Func<T, bool>> funcWhere);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">行数据类型</typeparam>
        /// <typeparam name="S">排序字段类型</typeparam>
        /// <param name="funcWhere"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="funcOrderby"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        PageResult<T> QueryPage<S>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex,
            Expression<Func<S>> funcOrderby, bool isAsc = true);

        /// <summary>
        /// 新增数据，即时Commit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        int Insert(T t);

        /// <summary>
        /// 新增数据，即时Commit
        /// 多条Sql 一个连接，事务插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tList"></param>
        /// <returns>返回带主键的集合</returns>
        int Insert(IEnumerable<T> tList);

        /// <summary>
        /// 更新数据,即时Commit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        int Update(T t);

        /// <summary>
        /// 更新数据，即时Commit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tList"></param>
        int Update(IEnumerable<T> tList);

        /// <summary>
        /// 根据主键删除数据，即时Commit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        int Delete(int Id);

        /// <summary>
        /// 删除数据，即时Commit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        int Delete(T t);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tList"></param>
        /// <returns></returns>
        int Delete(IEnumerable<T> tList);

        /// <summary>
        /// 立即保存全部修改
        /// </summary>
        int Commit();

        /// <summary>
        /// 执行Sql 返回集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IQueryable<T> ExcuteQuery(string sql, SqlParameter[] parameters);

        /// <summary>
        /// 执行sql,无返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        int Excute(string sql, SqlParameter[] parameters);
    }
}
