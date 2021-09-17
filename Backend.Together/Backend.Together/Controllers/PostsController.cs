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
    [Route("api/v1")]
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

        [HttpGet("usersauth/usersprofile/allusersposts")]
        public async Task<ActionResult<List<PostDTO>>> GetAllUsersPosts()
        {
            _logger.LogInformation("PostsController: Getting all Posts from Database!");

            var postsResult = await _postService.GetAllUsersPosts();

            if (!postsResult.Success)
                return BadRequest(postsResult.Message);

            return Ok(postsResult.Result);
        }

        [HttpGet("usersauth/{id}/usersprofile/posts")]
        public async Task<ActionResult<List<PostDTO>>> GetUserPostsById(Guid id)
        {
            _logger.LogInformation($"PostsController: Getting the posts for the user with id {id}");

            var specificPostResult = await _postService.GetUserPostsById(id);

            if (specificPostResult.Exception)
                return BadRequest(specificPostResult.Message);

            if (!specificPostResult.Success)
                return NotFound(specificPostResult.Message);

            return Ok(specificPostResult.Result);
        }

        [HttpPost("usersauth/{id}/usersprofile/posts")]
        public async Task<IActionResult> PostNewUserTogetherPost(Guid id, [FromBody] PostDTO postDTO)
        {
            _logger.LogInformation("PostsController: A new post will be added in Database");

            var postResult = await _postService.AddNewUserPost(id, _mapper.Map<PostModel>(postDTO));

            if (!postResult.Success)
                return BadRequest(postResult.Message);

            return Ok(postResult.Result);
        }

        [HttpPut("usersauth/{id}/usersprofile/posts/{postid}")]
        public async Task<IActionResult> PutUserTogetherPost(Guid id, Guid postid, [FromBody] PostDTO postDTO)
        {
            _logger.LogInformation($"PostsController: The post with the {id} will be updated for the user with id {id}!");

            var postResult = await _postService.UpdateExistingUserPost(id, postid, _mapper.Map<PostModel>(postDTO));

            if (postResult.Exception)
                return BadRequest(postResult.Message);

            if (!postResult.Success)
                return NotFound(postResult.Message);

            return Ok();
        }

        [HttpDelete("usersauth/{id}/usersprofile/posts/{postid}")]
        public async Task<IActionResult> DeleteTogetherUserPost(Guid id, Guid postid)
        {
            _logger.LogInformation($"PostsController: The post with the {id} will be deleted!");

            var postResult = await _postService.DeleteExistingUserPost(id, postid);

            if (postResult.Exception)
                return BadRequest(postResult.Message);

            if (!postResult.Success)
                return NotFound(postResult.Message);

            return Ok();
        }
    }
}
