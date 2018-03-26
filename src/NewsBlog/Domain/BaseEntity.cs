using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsBlog.Domain
{
    public abstract class BaseEntity : IEntity<long>
    {
        public long Id { get; set; }
    }
}
