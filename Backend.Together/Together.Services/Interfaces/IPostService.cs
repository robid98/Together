using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Together.Data;
using Together.Data.DTOs;
using Together.Data.Models;

namespace Together.Services.Interfaces
{
    public interface IPostService
    {
        Task<ResultModel<PostDTO>> AddNewPost(PostModel post);

        Task<ResultModel<List<PostDTO>>> GetAllPosts();

        Task<ResultModel<PostDTO>> GetPostById(Guid postId);

        Task<ResultModel<PostDTO>> UpdateExistingPost(PostModel post);
    }
}
