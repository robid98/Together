using System;
using System.Collections.Generic;
using Together.Data.Models;

namespace Together.Data.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<PostModel> GetAllPosts();
        PostModel GetPostByGuid(Guid postId);
        void InsertPost(PostModel post);
        void UpdatePost(PostModel post);
        void DeletePost(PostModel post);
    }
}
