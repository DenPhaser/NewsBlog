using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsBlog.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using NewsBlog.Models;

    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
        }

        protected ApplicationUser GetCurrentUser()
        {
            return this.GetCurrentUserAsync().Result;
        }

        protected Task<ApplicationUser> GetCurrentUserAsync()
        {
            var userManager = HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>))
                                  as UserManager<ApplicationUser>;

            return userManager.GetUserAsync(HttpContext.User);
        }
    }
}
