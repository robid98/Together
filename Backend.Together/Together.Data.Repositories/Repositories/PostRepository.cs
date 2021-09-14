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

        public PostRepository(DatabaseContext databaseContext, ILogger<PostRepository> logger)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }

        public async Task<ResultModel<PostModel>> DeletePost(Guid postId)
        {
            try
            {
                var post = (await GetPostByGuid(postId)).Result;

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

        public async Task<ResultModel<List<PostModel>>> GetAllPosts()
        {
            try
            {   
                var allPosts = await _databaseContext.Posts.OrderBy(post => post.PostDate).ToListAsync();
                return new ResultModel<List<PostModel>> { Success = true, Result = allPosts };
            }
            catch(Exception exception)
            {
                _logger.LogError($"PostRepository: Exception when getting all posts {exception.Message}");

                return new ResultModel<List<PostModel>> { Success = false, Exception = true, Message = "A problem occured when trying get all existing Posts" };
            }
       
        }

        public async Task<ResultModel<PostModel>> GetPostByGuid(Guid postId)
        {
            try
            {
                var post = await _databaseContext.Posts.FirstOrDefaultAsync(post => post.PostId == postId);

                if(post == null)
                {
                    return new ResultModel<PostModel> { Success = false, Message = "Post not found" };
                }

                return new ResultModel<PostModel> { Success = true, Result = post };
            }
            catch(Exception exception)
            {
                _logger.LogError($"PostRepository: Exception when getting the post with id {postId} Exception - {exception.Message}");

                return new ResultModel<PostModel> { Success = false, Exception = true, Message = $"A problem occured when trying to get the post with id {postId}" };
            }
        }

        public async Task<ResultModel<PostModel>> InsertPost(PostModel post)
        {
            try
            {
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

        public async Task<ResultModel<PostModel>> UpdatePost(Guid id, PostModel post)
        {
            var getPost = await _databaseContext.Posts.FirstOrDefaultAsync(p => p.PostId == id);

            if (getPost != null)
            {
                try
                {
                    getPost.PostDescription = (post.PostDescription != null) ? post.PostDescription : getPost.PostDescription;
                    getPost.PostDate = (post.PostDate != null) ? post.PostDate : getPost.PostDate;
                    getPost.PostLikes = (post.PostLikes != null) ? post.PostLikes : getPost.PostLikes;
                    getPost.PostShares = (post.PostShares != null) ? post.PostShares : getPost.PostShares;
                    getPost.PostDeleted = (post.PostDeleted != null) ? post.PostDeleted : getPost.PostDeleted;

                    await _databaseContext.SaveChangesAsync();
                    return new ResultModel<PostModel> { Success = true, Message = "Updated with success in Database" };
                }
                catch(SqlException exception)
                {
                    _logger.LogError($"PostRepository: Exception when updating post {exception.Message}");
                    return new ResultModel<PostModel> { Success = false, Exception = true, Message = "A problem occured when trying to update the existing Post" };
                }
            }

            return new ResultModel<PostModel> { Success = false, Message = "Post not found!" };
        }
    }
}
