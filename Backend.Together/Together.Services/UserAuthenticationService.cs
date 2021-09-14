using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Together.Data;
using Together.Data.DTOs;
using Together.Data.Models;
using Together.Data.Repositories.Interfaces;
using Together.Services.Enums;
using Together.Services.Helpers;
using Together.Services.Interfaces;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Together.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserAuthenticationRepository _userAuthenticationRepository;
        private readonly ILogger<UserAuthenticationService> _logger;
        private readonly IMapper _mapper;

        public UserAuthenticationService(
              IUserAuthenticationRepository userAuthenticationRepository,
              ILogger<UserAuthenticationService> logger,
              IMapper mapper)
        {
            _userAuthenticationRepository = userAuthenticationRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResultModel<UserAuthenticationDTO>> AddNewUser(UserAuthenticationModel user)
        {
            _logger.LogInformation("UserAuth Service: Adding a new User!");

            var validUserChecksResult = await ValidUserChecks(user);
            if (!validUserChecksResult.Success)
                return validUserChecksResult;

            user.UserId = Guid.NewGuid();
            user.Password = BCryptNet.HashPassword(user.Password);
            var resultInsert = await _userAuthenticationRepository.InsertUser(user);

            return new ResultModel<UserAuthenticationDTO>
            {
                Success = resultInsert.Success,
                Exception = resultInsert.Exception,
                Message = resultInsert.Message,
                Result = (resultInsert.Result != null) ? _mapper.Map<UserAuthenticationDTO>(resultInsert.Result) : null
            };
        }

        public async Task<ResultModel<List<UserAuthenticationDTO>>> GetAllUsersAuthInfos()
        {
            _logger.LogInformation("UserAuth Service: Getting all Users Auth Infos!");

            var allUsersResult = await _userAuthenticationRepository.GetAllUsers();

            return new ResultModel<List<UserAuthenticationDTO>>
            {
                Success = allUsersResult.Success,
                Exception = allUsersResult.Exception,
                Message = allUsersResult.Message,
                Result = (allUsersResult.Result != null) ? _mapper.Map<List<UserAuthenticationModel>, List<UserAuthenticationDTO>>(allUsersResult.Result) : null
            };
        }

        public async Task<ResultModel<UserAuthenticationDTO>> DeleteExistingUser(Guid userId)
        {
            _logger.LogInformation($"UserAuth Service: Deleting the user with Id {userId}!");

            var deletingUserResult = await _userAuthenticationRepository.DeleteUser(userId);

            return new ResultModel<UserAuthenticationDTO> { Success = deletingUserResult.Success, Message = deletingUserResult.Message, Exception = deletingUserResult.Exception };
        }

        public async Task<ResultModel<UserAuthenticationDTO>> GetUserById(Guid userId)
        {
            _logger.LogInformation($"UserAuth Service: Getting the user with the Id {userId}");

            var specificUserResult = await _userAuthenticationRepository.GetUserAuthInfoById(userId);

            return new ResultModel<UserAuthenticationDTO>
            {
                Success = specificUserResult.Success,
                Exception = specificUserResult.Exception,
                Message = specificUserResult.Message,
                Result = specificUserResult.Result != null ? 
                                _mapper.Map<UserAuthenticationModel, UserAuthenticationDTO>(specificUserResult.Result) : null
            };
        }

        public async Task<ResultModel<UserAuthenticationDTO>> UpdateExistingUser(Guid userId, UserAuthenticationModel user)
        {
            _logger.LogInformation("UserAuth Service: Updating a User!");

            var validUserChecksResult = await ValidUserChecks(user);
            if (!validUserChecksResult.Success)
                return validUserChecksResult;

            if(user.Password != null)
                user.Password = BCryptNet.HashPassword(user.Password);

            var updateResult = await _userAuthenticationRepository.UpdateUser(userId, user);
            return new ResultModel<UserAuthenticationDTO>
            {
                Success = updateResult.Success,
                Exception = updateResult.Exception,
                Message = updateResult.Message
            };
        }

        public async Task<bool> UserAlreadyExists(String userEmail)
        {
            var getUser = await _userAuthenticationRepository.GetUserAuthInfoByEmail(userEmail);

            if (!getUser.Success && !getUser.Exception) /* Not found */
                return false;

            return true;

        }

        public PasswordScore CheckUserStrenghtPassword(string password)
        {
            int score = 1;

            if (password.Length < 1)
                return PasswordScore.Blank;
            if (password.Length < 4)
                return PasswordScore.VeryWeak;

            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;
            if (Regex.Match(password, @"\d+", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"[a-z]", RegexOptions.ECMAScript).Success &&
                Regex.Match(password, @"[A-Z]", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @".[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript).Success)
                score++;

            return (PasswordScore)score;
        }

        public async Task<ResultModel<UserAuthenticationDTO>> ValidUserChecks(UserAuthenticationModel user)
        {
            /* Check if user already exists in Database */
            if (user.Email != null)
            {
                if ((await UserAlreadyExists(user.Email)))
                    return new ResultModel<UserAuthenticationDTO> { Success = false, Message = "User already exists!" };

                /* Check if Email is invalid */
                if (!EmailValidator.EmailIsValid(user.Email))
                    return new ResultModel<UserAuthenticationDTO> { Success = false, Message = "Invalid email!" };
            }

            if (user.Password != null)
            {
                /* Check if Password is strong enough */
                if (CheckUserStrenghtPassword(user.Password) < PasswordScore.Strong)
                    return new ResultModel<UserAuthenticationDTO> { Success = false, Message = "Password not strong enough!" };
            }

            if (user.FirstName != null)
            {
                if (user.FirstName.Length <= 1)
                    return new ResultModel<UserAuthenticationDTO> { Success = false, Message = "Invalid first name!" };
            }

            if (user.LastName != null)
            {
                if (user.LastName.Length <= 1)
                    return new ResultModel<UserAuthenticationDTO> { Success = false, Message = "Invalid last name!" };
            }

            return new ResultModel<UserAuthenticationDTO> { Success = true };
        }
    }
}
