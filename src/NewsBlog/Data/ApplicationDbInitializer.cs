using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using NewsBlog.Domain;
using NewsBlog.Services;

namespace NewsBlog.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using NewsBlog.Models;

    public class ApplicationDbInitializer
    {
        private readonly Random random;
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPostService _postService;

        public ApplicationDbInitializer(
            IHostingEnvironment hostingEnvironment,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IPostService postService)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._postService = postService;

            this.random = new Random();
        }

        public async Task Initialize()
        {
            // Initialize admin role
            if (!await this._roleManager.RoleExistsAsync("Administrator"))
            {
                var adminRole =
                    new IdentityRole()
                    {
                        Name = "Administrator",
                    };
                await this._roleManager.CreateAsync(adminRole);
            }

            // Initialize default user
            ApplicationUser defaultUser;
            if ((defaultUser = this._context.Users.SingleOrDefault(u => u.Email == "default@test.test")) == null)
            {
                defaultUser =
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

            // Assign administrator's role
            if (!await this._userManager.IsInRoleAsync(defaultUser, "Administrator"))
            {
                await this._userManager.AddToRoleAsync(defaultUser, "Administrator");
            }

            // Generate posts
            if (!await Task.Run(() => this._postService.GetPosts(userId: defaultUser.Id).Any()))
            {
                var posts = this.GetPosts(30);

                defaultUser.Posts = posts;

                await this._context.SaveChangesAsync();
            }
        }

        private List<Post> GetPosts(int count)
        {
            var posts = new List<Post>();

            var imagePaths = Directory.GetFiles(
                Directory.CreateDirectory(Path.Combine(_hostingEnvironment.WebRootPath, "uploads")).FullName,
                "*.jpg");

            Enumerable.Range(0, count).ToList().ForEach(i =>
            {
                string imageRelPath = null;
                var imageAbsPath = imagePaths.OrderBy(ip => Guid.NewGuid()).FirstOrDefault();
                if (!string.IsNullOrEmpty(imageAbsPath))
                {
                    imageRelPath = $"/uploads/{Path.GetFileName(imageAbsPath)}";
                }

                posts.Add(NewPost(GetRandomString(15, 60), GetRandomString(), imageRelPath));
            });

            return posts;
        }

        private Post NewPost(string title, string content, string imagePath = null)
        {
            var createdAt = DateTime.Now.AddDays(random.Next(1000) - 500);
            var modifiedAt = createdAt.AddDays(random.Next(200));

            return new Post()
            {
                Title = title,
                Text = content,
                ImagePath = imagePath,

                CreatedAt = createdAt,
                ModifiedAt = modifiedAt,
            };
        }

        private string GetRandomString(int minLength = 100, int maxLength = 500)
        {
            const string chars = "            abcdefghijklmnopqrstuvwxyz0123456789";
            var length = random.Next(minLength, maxLength);

            return new string(
                Enumerable
                    .Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray())
                    .Trim();
        }
    }
}
