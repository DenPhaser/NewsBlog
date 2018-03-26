using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsBlog.Models;

namespace NewsBlog.Domain
{
    public class Post : BaseEntity
    {
        public string UserId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string ImagePath { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
