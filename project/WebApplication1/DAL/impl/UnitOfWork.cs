﻿using ComputerNet.DAL.Interfaces;
using DAL.Entity;
using System;
 using DAL;
 using DAL.impl;
 using WebApplication1.Dal;

 namespace ComputerNet.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext context;
        public IWayRepository wayRepository;
//        public GenericRepository<City> cites;
        private bool disposed = false;

        public UnitOfWork()
        {
            context = new MyDbContext();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        
        public void Save()
        {
            context.SaveChanges();
        }

        public GenericRepository<T> Set<T>() where T : class
        {
            if (typeof(T) == typeof(IWayRepository))
            {
//                return global::DAL.impl.WayRepository as GenericRepository<T>;
            }

            return null;
        }

        public IWayRepository WayRepository =>
            wayRepository ??(wayRepository = new WayRepository(context));
        
//        public IUserRepository Users =>

//        public GenericRepository<City> Cites =>
//            cites ?? (cites = new GenericRepository<City>(context));

    }
}
