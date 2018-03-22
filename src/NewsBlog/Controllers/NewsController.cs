using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsBlog.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using NewsBlog.Models.NewsViewModels;

    public class NewsController : BaseController
    {
        #region Ctor

        public NewsController()
            : base()
        {
        }

        #endregion

        #region Methods

        // GET: /News/Index
        [HttpGet]
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }

        // GET: /News/View
        [HttpGet]
        public ActionResult View(long id)
        {
            throw new NotImplementedException();
        }

        // GET: /News/Add
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Add()
        {
            throw new NotImplementedException();
        }

        // POST: /News/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Add(NewsViewModel model)
        {
            throw new NotImplementedException();
        }

        // GET: /News/Edit
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(long id)
        {
            throw new NotImplementedException();
        }

        // POST: /News/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(NewsViewModel model)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
