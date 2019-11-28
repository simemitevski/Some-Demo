using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SecurityDemo.Data.Repositories
{
    public class RepositoryBase<T>
        where T : class
    {
        private static readonly string GenericTypeName = typeof(T).Name;

        public RepositoryBase(ISecurityDemoContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        protected ISecurityDemoContext Context { get; }

        protected DbSet<T> DbSet { get; }

        public virtual IList<T> GetAll()
        {
            return this.DbSet.ToList();
        }

        public virtual T GetById(int id)
        {
            return this.DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            this.DbSet.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            this.DbSet.Remove(entity);
        }
    }
}
