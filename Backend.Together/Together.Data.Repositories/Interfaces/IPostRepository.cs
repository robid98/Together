using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Together.Data.DTOs;
using Together.Data.Models;

namespace Together.Data.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<ResultModel<List<PostModel>>> GetAllPosts();
        Task<ResultModel<PostModel>> GetPostByGuid(Guid postId);
        Task<ResultModel<PostModel>> InsertPost(PostModel post);
        Task<ResultModel<PostModel>> UpdatePost(Guid id, PostModel post);
        Task<ResultModel<PostModel>> DeletePost(Guid postId);
    }
}
