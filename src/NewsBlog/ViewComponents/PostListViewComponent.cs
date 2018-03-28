namespace NewsBlog.ViewComponents
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using NewsBlog.Services;


    public class PostListViewComponent : ViewComponent
    {
        private readonly IPostService _postService;

        public PostListViewComponent(IPostService postService)
        {
            this._postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int page = 1)
        {
            var items = await Task.Run(() => this._postService.GetPosts(
                page: page,
                pageSize: 5));
            return View(items);
        }
    }
}
