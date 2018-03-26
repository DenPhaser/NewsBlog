using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NewsBlog.Domain;

namespace NewsBlog.Models
{
    using NewsBlog.Data;

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        public virtual List<Post> Posts { get; set; } 
    }
}
