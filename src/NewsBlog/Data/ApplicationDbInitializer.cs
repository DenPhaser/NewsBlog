using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsBlog.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using NewsBlog.Models;

    public class ApplicationDbInitializer
    {
        private ApplicationDbContext _context;

        public ApplicationDbInitializer(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task Initialize()
        {
            if (!this._context.Roles.Any())
            {
                var roles =
                    new List<IdentityRole>()
                    {
                        new IdentityRole()
                        {
                            Name = "Admin",
                            NormalizedName = "Administrator"
                        },
                        new IdentityRole()
                        {
                            Name = "Customer",
                            NormalizedName = "Customer"
                        },
                    };

                this._context.Roles.AddRange(roles);
                await this._context.SaveChangesAsync();
            }

            if (!this._context.Users.Any())
            {
                var defaultUser =
                    new ApplicationUser()
                    {
                        Email = "default@test.test",
                        UserName = "default@test.test",
                        NormalizedEmail = "DEFAULT@TEST.TEST",
                        NormalizedUserName = "DEFAULT@TEST.TEST",
                        PasswordHash = "AQAAAAEAACcQAAAAEBmSzIrNf6WYchGt827QeiQgtM9OIFif5nC7gAzS3BH0UACmhMxAu3Vc/AJQIetzkA==", // test12
                        SecurityStamp = "fe94668c-29b3-4bcc-bbe8-992eb3f59c87",
                        ConcurrencyStamp = "eafa09f6-8298-4701-9a2b-b49712f7fe4c",

                        AccessFailedCount = 0,
                        EmailConfirmed = false,
                        LockoutEnabled = true,
                        LockoutEnd = null,
                        PhoneNumber = null,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                    };

                this._context.Users.Add(defaultUser);

                await this._context.SaveChangesAsync();
            }
        }
    }
}
