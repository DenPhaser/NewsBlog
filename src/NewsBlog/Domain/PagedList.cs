using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NewsBlog.Models;

namespace NewsBlog.Domain
{
    public class PagedList<TEntity> : List<TEntity>
        where TEntity : BaseEntity
    {
        public int PageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public int TotalCount { get; private set; }

        protected PagedList(List<TEntity> items, int count, int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.TotalCount = count;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
            => (PageIndex > 1);

        public bool HasNextPage
            => (PageIndex < TotalPages);

        public static PagedList<TEntity> Create(
            IQueryable<TEntity> source,
            int page = 1,
            int pageSize = int.MaxValue)
        {
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<TEntity>(items, count, page, pageSize);
        }
    }
}
