using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsBlog.Extensions;
using NewsBlog.Services;

namespace NewsBlog.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using NewsBlog.Domain;
    using NewsBlog.Models.NewsViewModels;

    public class NewsController : BaseController
    {
        #region Fields

        private readonly IPostService postService;

        private readonly IMapper mapper;

        #endregion

        #region Ctor

        public NewsController(
            IPostService postService,
            IMapper mapper)
            : base()
        {
            this.postService = postService;
            this.mapper = mapper;
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

        // GET: /News/Create
        [HttpGet]
        public ActionResult View(long id)
        {
            var post = this.postService.GetPostById(id);
            var model = this.mapper.Map<PostViewModel>(post);

            return View(model);
        }

        // GET: /News/Create
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var model = new PostViewModel();

            return View(model);
        }

        // POST: /News/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(PostViewModel model)
        {
            var post = this.mapper.Map<Post>(model);
            post.User = this.GetCurrentUser();

            this.postService.InsertPost(post);

            return RedirectToAction("Edit", new { id = post.Id });
        }

        // GET: /News/Edit
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(long id)
        {
            var post = this.postService.GetPostById(id);
            var model = this.mapper.Map<PostViewModel>(post);

            return View(model);
        }

        // POST: /News/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(PostViewModel model)
        {
            var post = this.postService.GetPostById(model.Id);
            //post = this.mapper.Map(model, post);
            post.Title = model.Title;
            post.Text = model.Content;
            this.postService.UpdatePost(post);

            return RedirectToAction("Edit", new { id = post.Id });
        }

        // GET: /News/Delete
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(long id)
        {
            var post = this.postService.GetPostById(id);
            this.postService.DeletePost(post);

            return RedirectToAction("Index");
        }

        #endregion
    }
}
