using System;
using System.Collections.Generic;
using Together.Data.Interfaces;
using Together.Data.Models;

namespace Together.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        public PostRepository()
        {
                
        }

        public void DeletePost(PostModel post)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostModel> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public PostModel GetPostByGuid(Guid postId)
        {
            throw new NotImplementedException();
        }

        public void InsertPost(PostModel post)
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(PostModel post)
        {
            throw new NotImplementedException();
        }
    }
}
