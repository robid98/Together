using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Together.Data.DTOs;
using Together.Data.Models;
using Together.Services.Interfaces;

namespace Together.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IPostService postService, IMapper mapper, ILogger<PostsController> logger)
        {
            _postService = postService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostModel>>> GetPosts()
        {
            _logger.LogInformation("PostsController: Getting all Posts from Database!");

            var postsResult = await _postService.GetAllPosts();

            if (!postsResult.Success)
                return BadRequest(postsResult.Message);

            return Ok(postsResult.Result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<PostModel>>> GetPostById(Guid id)
        {
            _logger.LogInformation($"PostsController: Getting the post with the id {id}");

            var specificPostResult = await _postService.GetPostById(id);

            if (specificPostResult.Exception)
                return BadRequest(specificPostResult.Message);

            if (!specificPostResult.Success)
                return NotFound(specificPostResult.Message);

            return Ok(specificPostResult.Result);
        }

        [HttpPost]
        public async Task<IActionResult> PostNewTogetherPost([FromBody] PostDTO postDTO)
        {
            _logger.LogInformation("PostsController: A new post will be added in Database");

            var postResult = await _postService.AddNewPost(_mapper.Map<PostModel>(postDTO));

            if (!postResult.Success)
                return BadRequest(postResult.Message);

            return Ok(postResult.Result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTogetherPost(Guid id, [FromBody] PostDTO postDTO)
        {
            _logger.LogInformation($"PostsController: The post with the {postDTO.PostId} will be updated!");

            var postResult = await _postService.UpdateExistingPost(id, _mapper.Map<PostModel>(postDTO));

            if (postResult.Exception)
                return BadRequest(postResult.Message);

            if (!postResult.Success)
                return NotFound(postResult.Message);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTogetherPost(Guid id)
        {
            _logger.LogInformation($"PostsController: The post with the {id} will be deleted!");

            var postResult = await _postService.DeleteExistingPost(id);

            if (postResult.Exception)
                return BadRequest(postResult.Message);

            if (!postResult.Success)
                return NotFound(postResult.Message);

            return Ok();
        }
    }
}
