using Microsoft.EntityFrameworkCore;
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

        public PostRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task DeletePost(PostModel post)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PostModel>> GetAllPosts()
        {
            return await _databaseContext.Posts.OrderBy(post => post.PostDate).ToListAsync();
        }

        public async Task<PostModel> GetPostByGuid(Guid postId)
        {
            return await _databaseContext.Posts.FirstOrDefaultAsync(post => post.PostId == postId);
        }

        public async Task InsertPost(PostModel post)
        {
            await _databaseContext.AddAsync(post);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task UpdatePost(PostModel post)
        {
            var getPost = await GetPostByGuid(post.PostId);

            if (getPost != null)
            {
                getPost = post;
                await _databaseContext.SaveChangesAsync();
            }
        }
    }
}
