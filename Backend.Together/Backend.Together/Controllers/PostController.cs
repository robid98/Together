using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Data.DTOs;
using Together.Data.Models;
using Together.Services.Interfaces;

namespace Together.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostModel>>> GetPosts()
        {
            var posts = await _postService.GetAllPosts();
            return Ok(posts);
        }

        [HttpPost]
        public async Task<ActionResult<PostModel>> PostNewTogetherPost(PostDTO postDTO)
        {
            var result = await _postService.AddNewPost(_mapper.Map<PostModel>(postDTO));
            return Ok(result);
        }
    }
}
