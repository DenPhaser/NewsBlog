﻿namespace NewsBlog.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    using NewsBlog.Domain;
    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EfRepository(ApplicationDbContext context)
        {
            this._context = context;
            this._dbSet = this._context.Set<TEntity>();
        }

        public IQueryable<TEntity> Table =>
            this._dbSet.AsNoTracking();

        public TEntity GetById(long id)
        {
            return this.Table.SingleOrDefault(e => e.Id == id);
        }

        public void Add(TEntity entity)
        {
            this._dbSet.Add(entity);
            this._context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            this._dbSet.Remove(entity);
            this._context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            this._dbSet.Update(entity);
            this._context.SaveChanges();
        }
    }
}
