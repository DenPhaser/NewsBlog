using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsBlog.Extensions;
using NewsBlog.Services;

namespace NewsBlog.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using NewsBlog.Models.NewsViewModels;

    public class NewsController : BaseController
    {
        #region Fields

        private readonly IPostService _postService;

        #endregion

        #region Ctor

        public NewsController(
            IPostService postService)
            : base()
        {
            this._postService = postService;
        }

        #endregion

        #region Methods

        // GET: /News/Index
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            ViewBag.Page = page;

            return View();
        }

        // GET: /News/View
        [HttpGet]
        public ActionResult View(long id)
        {
            var post = this._postService.GetPostById(id);

            return View(post.ToModel());
        }

        // GET: /News/Add
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Add()
        {
            var postModel = new PostViewModel();

            return View(postModel);
        }

        // POST: /News/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Add(PostViewModel model)
        {
            var post = model.ToEntity();
            this._postService.InsertPost(post);

            return RedirectToAction("Edit", new { id = post.Id });
        }

        // GET: /News/Edit
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(long id)
        {
            var postModel = this._postService.GetPostById(id).ToModel();

            return View(postModel);
        }

        // POST: /News/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(PostViewModel model)
        {
            var post = model.ToEntity();

            this._postService.UpdatePost(post);

            return RedirectToAction("Edit", new { id = post.Id });
        }

        #endregion
    }
}
