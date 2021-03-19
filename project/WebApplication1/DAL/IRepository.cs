﻿using System.Linq;

namespace WebApplication1.Dal
{
    public interface IRepository<Entity>
    {
        IQueryable<Entity> GetAll();

        Entity GetById(int id);

        void Create(Entity entity);

        void Delete(Entity entity);

        void Update(Entity entity);
    }
}