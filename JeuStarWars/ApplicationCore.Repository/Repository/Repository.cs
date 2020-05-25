using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Repository
{
    public abstract class Repository<T, TContext> : IRepository<T>
         where T : BaseEntity, new()
        where TContext : DataContext
    {
        protected DataContext RepositoryContext { get; set; }
        protected DbSet<T> dbSet { get; set; }
        public Repository(TContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
            this.dbSet = repositoryContext.Set<T>();
        }
        public virtual bool Insert(T entity)
        {
            try
            {
                dbSet.Add(entity);
                this.RepositoryContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Delete(T entity)
        {
            try
            {
                dbSet.Remove(entity);
                RepositoryContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual IEnumerable<T> FindAll()
        {
            return this.dbSet.AsNoTracking().ToList();

        }
        public virtual T FindById(int Id)
        {
            return dbSet.Find(Id);
        }
       

        public virtual bool Update(T entity)
        {

            try
            {
                this.RepositoryContext.Entry(entity).State = EntityState.Modified;
                RepositoryContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual bool Exists(int Id)
        {

            return dbSet.Find(Id) != null;
        }
    }
}
