using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsBlog.Domain;

namespace NewsBlog.Services
{
    public interface IPostService
    {
        PagedList<Post> GetPosts(
            string userId = null,
            int page = 1,
            int pageSize = int.MaxValue);

        Post GetPostById(long id);

        void InsertPost(Post post);

        void UpdatePost(Post post);

        void DeletePost(Post post);
    }
}
