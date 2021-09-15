using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Together.Data.BlobStorage;
using Together.Data.DTOs;
using Together.Data.Models;
using Together.Services.Interfaces;

namespace Together.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UsersProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersProfileController> _logger;
        private readonly IBlobStorage _blobStorage;

        public UsersProfileController(IUserProfileService userProfileService, IMapper mapper, ILogger<UsersProfileController> logger, IBlobStorage blobStorage)
        {
            _userProfileService = userProfileService;
            _mapper = mapper;
            _logger = logger;
            _blobStorage = blobStorage;
        }

        [HttpPost("userAuthentication/{id}/userprofile")]
        public async Task<IActionResult> PostNewUserProfile(Guid id, [FromBody] UserProfileDTO userProfileDTO)
        {
            _logger.LogInformation("UsersProfileController: A new user profile will be added in Database");

            var userResult = await _userProfileService.AddNewUserProfile(id, _mapper.Map<UserProfileModel>(userProfileDTO));

            if (userResult.Exception)
                return BadRequest(userResult.Message);

            if (!userResult.Success)
                return NotFound(userResult.Message);

            return Ok(userResult.Result);
        }

        [HttpGet("userAuthentication/{id}/userprofile")]
        public async Task<ActionResult<UserProfileDTO>> GetUserProfileById(Guid id)
        {
            _logger.LogInformation($"UsersProfileController: Getting the user with the auth id {id}");

            var specificUserResult = await _userProfileService.GetUserProfileById(id);

            if (specificUserResult.Exception)
                return BadRequest(specificUserResult.Message);

            if (!specificUserResult.Success)
                return NotFound(specificUserResult.Message);

            return Ok(specificUserResult.Result);
        }

        [HttpDelete("userAuthentication/{id}/userprofile")]
        public async Task<IActionResult> DeleteTogetherUserProfile(Guid id)
        {
            _logger.LogInformation($"UsersProfileController: The user profile with the id {id} will be deleted!");

            var userDeleteResult = await _userProfileService.DeleteExistingUserProfile(id);

            if (userDeleteResult.Exception)
                return BadRequest(userDeleteResult.Message);

            if (!userDeleteResult.Success)
                return NotFound(userDeleteResult.Message);

            return Ok();
        }

        [HttpPut("userAuthentication/{id}/userprofile")]
        public async Task<IActionResult> PutTogetherUserProfile(Guid id, [FromBody] UserProfileDTO userProfileDTO)
        {
            _logger.LogInformation($"UsersProfileController: The user profile with the id {id} will be updated!");

            var userUpdateResult = await _userProfileService.UpdateExistingUserProfile(id, _mapper.Map<UserProfileModel>(userProfileDTO));

            if (userUpdateResult.Exception)
                return BadRequest(userUpdateResult.Message);

            if (!userUpdateResult.Success)
                return NotFound(userUpdateResult.Message);

            return Ok();
        }

        [HttpPut("userAuthentication/{id}/userprofile/profilepicture")]
        public async Task<IActionResult> PutTogetherUserProfilePicture(Guid id, [FromForm] FileModel model)
        {
            _logger.LogInformation($"UsersProfileController: The user profile picture with the id {id} will be updated!");

            var userUpdateResult = await _userProfileService.UpdateUserProfilePicture(id, model);

            if (userUpdateResult.Exception)
                return BadRequest(userUpdateResult.Message);

            if (!userUpdateResult.Success)
                return NotFound(userUpdateResult.Message);

            return Ok();
        }

        [HttpGet("userAuthentication/{id}/userprofile/profilepicture")]
        public async Task<IActionResult> GetTogetherUserProfilePicture(string fileName)
        {
            _logger.LogInformation($"UsersProfileController: Getting the file with the name {fileName}");

            var userGetImage = await _blobStorage.GetBlobContent(fileName);

            if (!userGetImage.Success)
                return BadRequest(userGetImage.Message);

            return File(userGetImage.Result, "image/webp");
        }
    }
}
