﻿using AutoMapper;
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
    public class UsersAuthenticationController : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersAuthenticationController> _logger;

        public UsersAuthenticationController(IUserAuthenticationService userAuthenticationService, IMapper mapper, ILogger<UsersAuthenticationController> logger)
        {
            _userAuthenticationService = userAuthenticationService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("usersauth")]
        public async Task<ActionResult<List<UserAuthenticationDTO>>> GetUserAuthInfos()
        {
            _logger.LogInformation("UsersAuthenticationController: Getting all Users from Database!");

            var usersResult = await _userAuthenticationService.GetAllUsersAuthInfos();

            if (!usersResult.Success)
                return BadRequest(usersResult.Message);

            return Ok(usersResult.Result);
        }

        [HttpGet("usersauth/{id}")]
        public async Task<ActionResult<UserAuthenticationDTO>> GetUserById(Guid id)
        {
            _logger.LogInformation($"UsersAuthenticationController: Getting the user with the id {id}");

            var specificUserResult = await _userAuthenticationService.GetUserById(id);

            if (specificUserResult.Exception)
                return BadRequest(specificUserResult.Message);

            if (!specificUserResult.Success)
                return NotFound(specificUserResult.Message);

            return Ok(specificUserResult.Result);
        }


        [HttpPost("usersauth")]
        public async Task<IActionResult> PostNewUser([FromBody] UserAuthenticationDTO userAuthenticationDTO)
        {
            _logger.LogInformation("UsersAuthenticationController: A new user will be added in Database");

            var userResult = await _userAuthenticationService.AddNewUser(_mapper.Map<UserAuthenticationModel>(userAuthenticationDTO));

            if (!userResult.Success)
                return BadRequest(userResult.Message);

            return Ok(userResult.Result);
        }

        [HttpPut("usersauth/{id}")]
        public async Task<IActionResult> PutTogetherUser(Guid id, [FromBody] UserAuthenticationDTO userAuthenticationDTO)
        {
            _logger.LogInformation($"UsersAuthenticationController: The user with the id {id} will be updated!");

            var userUpdateResult = await _userAuthenticationService.UpdateExistingUser(id, _mapper.Map<UserAuthenticationModel>(userAuthenticationDTO));

            if (userUpdateResult.Exception)
                return BadRequest(userUpdateResult.Message);

            if (!userUpdateResult.Success)
                return NotFound(userUpdateResult.Message);

            return Ok();
        }

        [HttpDelete("usersauth/{id}")]
        public async Task<IActionResult> DeleteTogetherUser(Guid id)
        {
            _logger.LogInformation($"UsersAuthenticationController: The user with the id {id} will be deleted!");

            var userDeleteResult = await _userAuthenticationService.DeleteExistingUser(id);

            if (userDeleteResult.Exception)
                return BadRequest(userDeleteResult.Message);

            if (!userDeleteResult.Success)
                return NotFound(userDeleteResult.Message);

            return Ok();
        }
    }
}
