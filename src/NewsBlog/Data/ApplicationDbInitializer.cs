using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsBlog.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using NewsBlog.Models;

    public class ApplicationDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbInitializer(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task Initialize()
        {
            IdentityRole adminRole;
            if (await this._roleManager.RoleExistsAsync("Administrator"))
            {
                adminRole =
                    new IdentityRole()
                    {
                        Name = "Administrator",
                    };
                await this._roleManager.CreateAsync(adminRole);
            }
            else
            {
                adminRole = await this._roleManager.FindByNameAsync("Admin");
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

                this._context.UserRoles.AddRange(
                    new List<IdentityUserRole<string>>()
                    {
                        new IdentityUserRole<string>()
                        {
                            UserId = defaultUser.Id,
                            RoleId = adminRole.Id
                        }
                    });

                await this._context.SaveChangesAsync();
            }
        }
    }
}
