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
        Task<ResultModel<PostDTO>> AddNewUserPost(Guid userAuthenticationGuid, PostModel post);

        Task<ResultModel<List<PostDTO>>> GetAllUsersPosts();

        Task<ResultModel<List<PostDTO>>> GetUserPostsById(Guid userAuthenticationGuid);

        Task<ResultModel<PostDTO>> UpdateExistingUserPost(Guid userAuthenticationGuid, Guid postId, PostModel post);

        Task<ResultModel<PostDTO>> DeleteExistingUserPost(Guid userAuthenticationGuid, Guid postId);
    }
}
