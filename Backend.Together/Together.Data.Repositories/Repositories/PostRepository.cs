using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Data.Models;
using Together.Data.Repositories.Interfaces;
using Together.Data.SQL;

namespace Together.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<PostRepository> _logger;
        private readonly IMapper _mapper;
        private readonly IUserAuthenticationRepository _userAuthRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public PostRepository(DatabaseContext databaseContext, ILogger<PostRepository> logger, IMapper mapper, IUserAuthenticationRepository userAuthRepository, IUserProfileRepository userProfileRepository)
        {
            _databaseContext = databaseContext;
            _logger = logger;
            _mapper = mapper;
            _userAuthRepository = userAuthRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<ResultModel<PostModel>> DeleteUserPost(Guid userAuthenticationGuid, Guid postId)
        {
            try
            {
                var post = (await GetUserSpecificPost(userAuthenticationGuid, postId)).Result;

                if (post == null)
                {
                    return new ResultModel<PostModel> { Success = false, Message = "Post not found" };
                }

                _databaseContext.Remove(post);
                _databaseContext.SaveChanges();

                return new ResultModel<PostModel> { Success = true, Message = "Post deleted" };
            }
            catch (SqlException exception)
            {
                _logger.LogError($"PostRepository: Exception when deleting the post with id {postId} Exception - {exception.Message}");

                return new ResultModel<PostModel> { Success = false, Exception = true, Message = $"A problem occured when trying to delete the post with id {postId}" };
            }
        }

        public async Task<ResultModel<List<PostModel>>> GetAllUsersPosts()
        {
            /* Get users auth profiles */
            var allAuthResult = await _userAuthRepository.GetAllUsers();
            if(!allAuthResult.Success)
                return new ResultModel<List<PostModel>> { Success = allAuthResult.Success, Exception = allAuthResult.Exception, Message = allAuthResult.Message };

            /* Iterate over every profile*/
            var allPosts = new List<PostModel>();
            foreach(var userAuth in allAuthResult.Result)
            {
                if(userAuth.UserProfileModel == null)
                    return new ResultModel<List<PostModel>> { Success = false, Message = $"Profile not found for the user with id {userAuth.UserId}" };

                foreach (var userPost in userAuth.UserProfileModel.UserPosts)
                    allPosts.Add(userPost);
            }

            allPosts.OrderBy(post => post.PostDate).ToList();

            return new ResultModel<List<PostModel>> { Success = true, Result = allPosts };      
        }

        public async Task<ResultModel<List<PostModel>>> GetUserPostsByGuid(Guid userAuthenticationGuid)
        {
            var userProfileResult = await _userProfileRepository.GetUserProfileByGuid(userAuthenticationGuid);

            if (!userProfileResult.Success)
            {
                return new ResultModel<List<PostModel>> { Success = userProfileResult.Success, Exception = userProfileResult.Exception, Message = userProfileResult.Message };
            }

            return new ResultModel<List<PostModel>> { Success = true, Result = userProfileResult.Result.UserPosts };
        }

        public async Task<ResultModel<PostModel>> GetUserSpecificPost(Guid userAuthenticationGuid, Guid postId)
        {
            var userPostsResult = await GetUserPostsByGuid(userAuthenticationGuid);

            if (!userPostsResult.Success)
            {
                return new ResultModel<PostModel> { Success = userPostsResult.Success, Exception = userPostsResult.Exception, Message = userPostsResult.Message };
            }

            var specificPost = userPostsResult.Result.Where(post => post.PostId == postId).FirstOrDefault();

            if (specificPost == null)
            {
                return new ResultModel<PostModel> { Success = false, Message = "Post not found" };
            }

            return new ResultModel<PostModel> { Success = true, Result = specificPost };
        }

        public async Task<ResultModel<PostModel>> InsertUserPost(Guid userAuthenticationGuid, PostModel post)
        {
            var userProfileResult = await _userProfileRepository.GetUserProfileByGuid(userAuthenticationGuid);

            if (!userProfileResult.Success)
            {
                return new ResultModel<PostModel> { Success = userProfileResult.Success, Exception = userProfileResult.Exception, Message = userProfileResult.Message };
            }

            try
            {
                post.UserProfileId = userProfileResult.Result.UserProfileId;
                await _databaseContext.AddAsync(post);
                await _databaseContext.SaveChangesAsync();
                return new ResultModel<PostModel> { Success = true, Result = post};
            }
            catch(SqlException exception)
            {
                _logger.LogError($"PostRepository: Exception when creating a new post {exception.Message}");
                return new ResultModel<PostModel> { Success = false, Exception = true, Message = "A problem occured when trying to add a new Post" };
            }
        }

        public async Task<ResultModel<PostModel>> UpdateExistingPost(Guid userAuthenticationGuid, Guid postId, PostModel post)
        {
            var userPostResult = await GetUserSpecificPost(userAuthenticationGuid, postId);

            if (!userPostResult.Success)
            {
                return new ResultModel<PostModel> { Success = userPostResult.Success, Exception = userPostResult.Exception, Message = userPostResult.Message };
            }

            try
            {
                _mapper.Map(post, userPostResult.Result);

                await _databaseContext.SaveChangesAsync();
                return new ResultModel<PostModel> { Success = true, Message = "Updated with success in Database" };
            }
            catch(SqlException exception)
            {
                _logger.LogError($"PostRepository: Exception when updating post {exception.Message} for user with id {userAuthenticationGuid}");

                return new ResultModel<PostModel> { Success = false, Exception = true, Message = "A problem occured when trying to update the existing Post" };
            }
        }
    }
}
