using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Together.Data.DTOs;
using Together.Data.Models;

namespace Together.Data.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<ResultModel<List<PostModel>>> GetAllUsersPosts();
        Task<ResultModel<List<PostModel>>> GetUserPostsByGuid(Guid userAuthenticationGuid);
        Task<ResultModel<PostModel>> GetUserSpecificPost(Guid userAuthenticationGuid, Guid postId);
        Task<ResultModel<PostModel>> InsertUserPost(Guid userAuthenticationGuid, PostModel post);
        Task<ResultModel<PostModel>> UpdateExistingPost(Guid userAuthenticationGuid, Guid postId, PostModel post);
        Task<ResultModel<PostModel>> DeleteUserPost(Guid userAuthenticationGuid, Guid postId);
    }
}
