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
    public abstract class GenericService<T> : IService<T>
         where T : class
    {
        protected DbContext Context { get; private set; }

        public GenericService(DbContext context)
        {
            this.Context = context;
        }

        public T Find(int id)
        {
            return this.Context.Set<T>().Find(id);
        }

        public IQueryable<T> Set()
        {
            return this.Context.Set<T>().AsQueryable();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> funcWhere)
        {
            return this.Context.Set<T>().Where(funcWhere);
        }

        public PageResult<T> QueryPage<S>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex, Expression<Func<T, S>> funcOrderby, bool isAsc = true)
        {
            var list = this.Set();
            if (funcWhere != null)
            {
                list = list.Where(funcWhere);
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
                TotalCount = this.Set().Count(funcWhere)
            };
            return result;
        }

        public int Insert(T t)
        {
            this.Context.Set<T>().Add(t);
            return this.Commit();           
        }

        public int Insert(IEnumerable<T> tList)
        {
            this.Context.Set<T>().AddRange(tList);
            return this.Commit();
        }

        public int Update(T t)
        {
            this.Context.Set<T>().Attach(t);
            this.Context.Entry(t).State = EntityState.Modified;
            return this.Commit();
        }

        public int Update(IEnumerable<T> tList)
        {
            foreach (var t in tList)
            {
                this.Context.Set<T>().Attach(t);
                this.Context.Entry(t).State = EntityState.Modified;
            }
            return this.Commit();
        }

        public int Delete(int Id)
        {
            T t = this.Find(Id);
            this.Context.Set<T>().Remove(t);
            return this.Commit();
        }

        public int Delete(T t)
        {
            DbSet<T> dbSet = this.Context.Set<T>();
            dbSet.Attach(t);
            dbSet.Remove(t);
            return this.Commit();
        }

        public int Delete(IEnumerable<T> tList)
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

        public IQueryable<T> ExcuteQuery(string sql, SqlParameter[] parameters)
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
