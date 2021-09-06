using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        public async Task<PostDTO> AddNewPost(PostModel post)
        {
            post.PostId = Guid.NewGuid();
            await _postRepository.InsertPost(post);

            return _mapper.Map<PostDTO>(post);
        }

        public async Task<List<PostDTO>> GetAllPosts()
        {
            var allPosts = await _postRepository.GetAllPosts();
            return _mapper.Map<List<PostModel>, List<PostDTO>>(allPosts);
        }
    }
}
