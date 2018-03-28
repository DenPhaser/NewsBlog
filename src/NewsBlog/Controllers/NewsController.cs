using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NewsBlog.Extensions;
using NewsBlog.Models;
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

        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IPostService postService;

        private readonly IMapper mapper;

        #endregion

        #region Ctor

        public NewsController(
            IHostingEnvironment hostingEnvironment,
            IPostService postService,
            IMapper mapper)
            : base()
        {
            this.hostingEnvironment = hostingEnvironment;
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

        // POST: /News/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var post = this.mapper.Map<Post>(model);
            post.User = this.GetCurrentUser();
            post.ImagePath = await UploadImageAsync(model.Image);

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
        public async Task<IActionResult> Edit(PostViewModel model)
        {
            var post = this.postService.GetPostById(model.Id);
            post = this.mapper.Map(model, post);
            post.ImagePath = await UploadImageAsync(model.Image) ?? post.ImagePath;
            this.postService.UpdatePost(post);

            return RedirectToAction("Edit", new { id = post.Id });
        }

        // GET: /News/Delete
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(long id)
        {
            var post = this.postService.GetPostById(id);
            this.postService.DeletePost(post);

            return RedirectToAction("Index");
        }

        #endregion

        #region Utilites

        private async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null)
            {
                return null;
            }

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var relativePath = Path.Combine("uploads", fileName);
            var filePath = Path.Combine(hostingEnvironment.WebRootPath, relativePath);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/ {relativePath}";
        }

        #endregion
    }
}
