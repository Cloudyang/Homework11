using Bussiness.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.Interface.Model;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Bussiness.Service
{
    public abstract class BaseService : IBaseService
    {
        protected DbContext Context { get; private set; }

        public BaseService(DbContext context)
        {
            this.Context = context;
        }

        public T Find<T>(int id) where T : class
        {
            return Context.Set<T>().Find(id);
        }

        public IQueryable<T> Set<T>() where T : class
        {
            return Context.Set<T>();
        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class
        {
            return Set<T>().Where(funcWhere);
        }

        public PageResult<T> QueryPage<T, S>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex, Expression<Func<T, S>> funcOrderby, bool isAsc = true) where T : class
        {
            var list = this.Set<T>();
            if (funcWhere != null)
            {
                list = list.Where<T>(funcWhere);
            }
            if (funcOrderby != null)
            {
                if (isAsc)
                {
                    list = list.OrderBy(funcOrderby);
                }
                else
                {
                    list = list.OrderByDescending(funcOrderby);
                }
            }
            PageResult<T> result = new PageResult<T>()
            {
                DataList = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = this.Set<T>().Count(funcWhere)
            };
            return result;
        }

        public int Insert<T>(T t) where T : class
        {
            this.Context.Set<T>().Add(t);
            return this.Commit();           
        }

        public int Insert<T>(IEnumerable<T> tList) where T : class
        {
            this.Context.Set<T>().AddRange(tList);
            return this.Commit();
        }

        public int Update<T>(T t) where T : class
        {
            this.Context.Set<T>().Attach(t);
            this.Context.Entry<T>(t).State = EntityState.Modified;
            return this.Commit();
        }

        public int Update<T>(IEnumerable<T> tList) where T : class
        {
            foreach (var t in tList)
            {
                this.Context.Set<T>().Attach(t);
                this.Context.Entry<T>(t).State = EntityState.Modified;
            }
            return this.Commit();
        }

        public int Delete<T>(int Id) where T : class
        {
            T t = this.Find<T>(Id);
            this.Context.Set<T>().Remove(t);
            return this.Commit();
        }

        public int Delete<T>(T t) where T : class
        {
            DbSet<T> dbSet = this.Context.Set<T>();
            dbSet.Attach(t);
            dbSet.Remove(t);
            return this.Commit();
        }

        public int Delete<T>(IEnumerable<T> tList) where T : class
        {
            DbSet<T> dbSet = this.Context.Set<T>();
            foreach (var t in tList)
            {
                dbSet.Attach(t);
            }
            dbSet.RemoveRange(tList);
            return this.Commit();
        }

        public int Commit()
        {
            return this.Context.SaveChanges();
        }

        public IQueryable<T> ExcuteQuery<T>(string sql, SqlParameter[] parameters) where T : class
        {
            return this.Context.Database.SqlQuery<T>(sql, parameters).AsQueryable();
        }

        public int Excute(string sql, SqlParameter[] parameters)
        {
            return this.Context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public virtual void Dispose()
        {
            this.Context?.Dispose();
        }
    }
}
