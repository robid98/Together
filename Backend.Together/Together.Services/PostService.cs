using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Together.Data.Models;
using Together.Data.Repositories.Interfaces;
using Together.Services.Interfaces;

namespace Together.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<PostModel> AddNewPost(PostModel post)
        {
            post.PostId = Guid.NewGuid();
            await _postRepository.InsertPost(post);

            return post;
        }

        public async Task<List<PostModel>> GetAllPosts()
        {
            return await _postRepository.GetAllPosts();
        }
    }
}
