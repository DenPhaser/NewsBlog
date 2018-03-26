using System.Linq;

namespace NewsBlog.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> Table { get; }

        TEntity GetById(long id);

        void Add(TEntity entity);

        void Delete(TEntity entity);

        void Update(TEntity entity);
    }
}
