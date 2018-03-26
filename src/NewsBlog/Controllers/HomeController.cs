using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NewsBlog.Controllers
{
    using NewsBlog.Data;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "News");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
