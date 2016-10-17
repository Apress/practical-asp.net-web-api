using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Robusta.TalentManager.Domain;

namespace Robusta.TalentManager.Data
{
    public class Repository<T> : IRepository<T> where T : class, IIdentifiable
    {
        private readonly Context context;

        public Repository(IUnitOfWork uow)
        {
            context = uow.Context as Context;
        }

        public IQueryable<T> All
        {
            get
            {
                return context.Set<T>();
            }
        }

        public IQueryable<T> AllEager(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public T Find(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Insert(T item)
        {
            context.Entry(item).State = EntityState.Added;
        }

        public void Update(T item)
        {
            context.Set<T>().Attach(item);
            context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var item = context.Set<T>().Find(id);
            context.Set<T>().Remove(item);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
