using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsBlog.Domain;
using NewsBlog.Models;

namespace NewsBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Consts

        private const int StringKeyMaxLength = 150;

        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasKey(e => e.Id);
            builder.Entity<ApplicationUser>().Property(e => e.Id).HasMaxLength(StringKeyMaxLength);

            builder.Entity<IdentityRole>().Property(e => e.Id).HasMaxLength(StringKeyMaxLength);

            builder.Entity<IdentityUserToken<string>>().Property(e => e.UserId).HasMaxLength(StringKeyMaxLength);
            builder.Entity<IdentityUserToken<string>>().Property(e => e.LoginProvider).HasMaxLength(StringKeyMaxLength);
            builder.Entity<IdentityUserToken<string>>().Property(e => e.Name).HasMaxLength(StringKeyMaxLength);

            builder.Entity<IdentityUserLogin<string>>().Property(e => e.LoginProvider).HasMaxLength(StringKeyMaxLength);
            builder.Entity<IdentityUserLogin<string>>().Property(e => e.ProviderKey).HasMaxLength(StringKeyMaxLength);

            builder.Entity<IdentityUserRole<string>>().Property(e => e.UserId).HasMaxLength(StringKeyMaxLength);
            builder.Entity<IdentityUserRole<string>>().Property(e => e.RoleId).HasMaxLength(StringKeyMaxLength);

            // Post mapping
            builder.Entity<Post>().HasKey(e => e.Id);
            builder.Entity<Post>().Property(e => e.Title).IsRequired();
            builder.Entity<Post>()
                .HasOne(e => e.User)
                .WithMany(e => e.Posts)
                .HasForeignKey(e => e.UserId);
        }
    }
}
