using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.GenericRepo
{

    public class GenericRepo<TEntity> where TEntity : class
    {
        private dynamic meContext;
        private DbSet<TEntity> meDbSet;

        protected DbContext Context
        {
            get
            {
                if (meContext == null)
                {
                    meContext = new AISContext();
                }

                return meContext;
            }
        }

        protected DbSet<TEntity> DBSet
        {
            get
            {
                if (meDbSet == null)
                {
                    meDbSet = this.Context.Set<TEntity>();
                }

                return meDbSet;
            }
        }

        #region Get Methods
        public virtual TEntity GetById(object id)
        {
            return this.DBSet.Find(id);
        }

        public virtual IQueryable<TEntity> Table
        {
            get
            {
                return this.DBSet;
            }
        }

        #endregion


        #region Inserts

        public virtual void Insert<T>(T entity) where T : class
        {
            DbSet<T> dbSet = this.Context.Set<T>();
            dbSet.Add(entity);
        }

        public virtual void Insert(TEntity entity)
        {
            this.DBSet.Add(entity);
        }

        public virtual T InsertReturnsObject<T>(T entity) where T : class
        {
            DbSet<T> dbSet = this.Context.Set<T>();
            dbSet.Add(entity);

            return entity;
        }

        #endregion

        #region Update
        public virtual void Update<T>(T entity) where T : class
        {
            DbSet<T> dbSet = this.Context.Set<T>();
            dbSet.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update(TEntity entity)
        {
            this.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
        }

        #endregion

        #region Delete
        public virtual void Delete<T>(T entity) where T : class
        {
            DbSet<T> dbSet = this.Context.Set<T>();

            if (this.Context.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);

            dbSet.Remove(entity);

        }

        public virtual void Delete(TEntity entity)
        {
            if (this.Context.Entry(entity).State == EntityState.Detached)
                this.Attach(entity);

            this.DBSet.Remove(entity);

        }

        public virtual void Delete<T>(object[] id) where T : class
        {
            DbSet<T> dbSet = this.Context.Set<T>();
            T entity = dbSet.Find(id);
            dbSet.Attach(entity);
            dbSet.Remove(entity);

        }

        public virtual void Delete(object id)
        {
            TEntity entity = this.DBSet.Find(id);
            this.Delete(entity);
        }

        #endregion

        public virtual void Attach(TEntity entity)
        {
            if (this.Context.Entry(entity).State == EntityState.Detached)
                this.DBSet.Attach(entity);
        }

        public virtual void SaveChanges()
        {
            this.Context.SaveChanges();
        }
    }
}