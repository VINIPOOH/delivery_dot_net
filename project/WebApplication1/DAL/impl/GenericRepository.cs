﻿﻿using ComputerNet.DAL.Interfaces;
using ComputerNet.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
 using Microsoft.EntityFrameworkCore;
 using WebApplication1.Dal;

 namespace ComputerNet.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly MyDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(MyDbContext context)
        {            
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }

        public virtual void Create(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetById(long id)
        {
            return DbSet.Find(id);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(int id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }
        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
