using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Data.DTOs;
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

        public Task DeletePost(PostModel post)
        {
            throw new NotImplementedException();
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
                _logger.LogError($"PostController: Exception when getting all posts {exception.Message}");

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
                _logger.LogError($"PostController: Exception when getting the post with id {postId} Exception - {exception.Message}");

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
                _logger.LogError($"PostController: Exception when creating a new post {exception.Message}");
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
                    getPost.PostDescription = post.PostDescription;
                    getPost.PostDate = post.PostDate;
                    getPost.PostLikes = post.PostLikes;
                    getPost.PostShares = post.PostShares;
                    getPost.IsPostDeleted = post.IsPostDeleted;

                    await _databaseContext.SaveChangesAsync();
                    return new ResultModel<PostModel> { Success = true, Message = "Added with success in Database" };
                }
                catch(SqlException exception)
                {
                    _logger.LogError($"PostController: Exception when updating post {exception.Message}");
                    return new ResultModel<PostModel> { Success = false, Exception = true, Message = "A problem occured when trying to update the existing Post" };
                }
            }

            return new ResultModel<PostModel> { Success = false, Message = "Post not found!" };
        }
    }
}
