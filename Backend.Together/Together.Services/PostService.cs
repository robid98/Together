using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Together.Data;
using Together.Data.DTOs;
using Together.Data.Models;
using Together.Data.Repositories.Interfaces;
using Together.Services.Interfaces;

namespace Together.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;

        public PostService(IPostRepository postRepository, IMapper mapper, ILogger<PostService> logger)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResultModel<PostDTO>> AddNewUserPost(Guid userAuthenticationGuid, PostModel post)
        {
            _logger.LogInformation($"Post Service: Adding a new Post for the user with id {userAuthenticationGuid}!");

            if (!CheckPostIsValid(post))
                return new ResultModel<PostDTO> { Success = false, Message = "Post description can't be empty"};

            post.PostId = Guid.NewGuid();           
            var resultInsert = await _postRepository.InsertUserPost(userAuthenticationGuid, post);

            return new ResultModel<PostDTO>
            {
                Success = resultInsert.Success,
                Exception = resultInsert.Exception,
                Message = resultInsert.Message,
                Result = (resultInsert.Result != null) ? _mapper.Map<PostDTO>(resultInsert.Result) : null
            };
        }

        public async Task<ResultModel<List<PostDTO>>> GetAllUsersPosts()
        {
            _logger.LogInformation("Post Service: Getting all Users Posts!");

            var allPostsResult = await _postRepository.GetAllUsersPosts();

            return new ResultModel<List<PostDTO>>
            {
                Success = allPostsResult.Success,
                Exception = allPostsResult.Exception,
                Message = allPostsResult.Message,
                Result = (allPostsResult.Result != null) ? _mapper.Map<List<PostModel>, List<PostDTO>>(allPostsResult.Result) : null
            };
        }

        public async Task<ResultModel<List<PostDTO>>> GetUserPostsById(Guid userAuthenticationGuid)
        {
            _logger.LogInformation($"Post Service: Getting all the posts for the user with the id {userAuthenticationGuid}");

            var specificPostResult = await _postRepository.GetUserPostsByGuid(userAuthenticationGuid);

            return new ResultModel<List<PostDTO>>
            {
                Success = specificPostResult.Success,
                Exception = specificPostResult.Exception,
                Message = specificPostResult.Message,
                Result = specificPostResult.Result != null ? _mapper.Map<List<PostModel>, List<PostDTO>>(specificPostResult.Result) : null
            };
        }


        public async Task<ResultModel<PostDTO>> UpdateExistingUserPost(Guid userAuthenticationGuid, Guid postId, PostModel post)
        {
            _logger.LogInformation("Post Service: Updating a existing Post!");

            if (post.PostDescription != null && post.PostDescription.Length == 0)
                return new ResultModel<PostDTO> { Success = false, Message = "Post description can't be empty" };

            var updateResult = await _postRepository.UpdateExistingPost(userAuthenticationGuid, postId, post);

            return new ResultModel<PostDTO> 
            { 
                Success = updateResult.Success, 
                Exception = updateResult.Exception, 
                Message = updateResult.Message 
            };
        }

        public async Task<ResultModel<PostDTO>> DeleteExistingUserPost(Guid userAuthenticationGuid, Guid postId)
        {
            _logger.LogInformation($"Post Service: Deleting the post with id {postId} from the user with id {userAuthenticationGuid}!");

            var deletingPostResult = await _postRepository.DeleteUserPost(userAuthenticationGuid, postId);

            return new ResultModel<PostDTO> { Success = deletingPostResult.Success, Message = deletingPostResult.Message, Exception = deletingPostResult.Exception };
        }

        public bool CheckPostIsValid(PostModel post)
        {
            if (post.PostDescription == null || post.PostDescription == "")
                return false;

            return true;
        }
    }
}
