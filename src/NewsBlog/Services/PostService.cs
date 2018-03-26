using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsBlog.Domain;

namespace NewsBlog.Services
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _postRepository; 

        public PostService(
            IRepository<Post> postRepository)
        {
            this._postRepository = postRepository;
        }

        public PagedList<Post> GetPosts(
            string userId = null,
            int page = 1,
            int pageSize = int.MaxValue)
        {
            var query = this._postRepository.Table;

            // by user id
            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(p => p.UserId == userId);
            }

            query = query.OrderByDescending(p => p.CreatedAt);

            var posts = PagedList<Post>.Create(
                source: query,
                page: page,
                pageSize: pageSize);

            return posts;
        }

        public Post GetPostById(long id)
        {
            var post = this._postRepository.GetById(id);

            return post;
        }

        public void InsertPost(Post post)
        {
            this._postRepository.Add(post);
        }

        public void UpdatePost(Post post)
        {
            this._postRepository.Update(post);
        }
    }
}
