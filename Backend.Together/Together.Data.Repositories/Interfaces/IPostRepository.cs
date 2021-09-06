using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Together.Data.Models;

namespace Together.Data.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<List<PostModel>> GetAllPosts();
        Task<PostModel> GetPostByGuid(Guid postId);
        Task InsertPost(PostModel post);
        Task UpdatePost(PostModel post);
        Task DeletePost(PostModel post);
    }
}
