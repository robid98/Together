﻿using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Data.Models;
using Together.Data.Repositories.Interfaces;
using Together.Data.SQL;

namespace Together.Data.Repositories.Repositories
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<UserAuthenticationRepository> _logger;
        private readonly IMapper _mapper;

        public UserAuthenticationRepository(DatabaseContext databaseContext, ILogger<UserAuthenticationRepository> logger, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResultModel<UserAuthenticationModel>> DeleteUser(Guid userId)
        {
            try
            {
                var userAuthInfo = (await GetUserAuthInfoById(userId)).Result;

                if (userAuthInfo == null)
                {
                    return new ResultModel<UserAuthenticationModel> { Success = false, Message = "User not found" };
                }

                _databaseContext.Remove(userAuthInfo);
                _databaseContext.SaveChanges();

                return new ResultModel<UserAuthenticationModel> { Success = true, Message = "User deleted" };
            }
            catch (SqlException exception)
            {
                _logger.LogError($"UserAuthRepository: Exception when deleting the User with id {userId} Exception - {exception.Message}");

                return new ResultModel<UserAuthenticationModel> { Success = false, Exception = true, Message = $"A problem occured when trying to delete the User with id {userId}" };
            }
        }

        public async Task<ResultModel<List<UserAuthenticationModel>>> GetAllUsers()
        {
            try
            {
                var allUsers = await _databaseContext.UsersAuthInfos.Include(x => x.UserProfileModel.UserPosts).OrderBy(user => user.Email).ToListAsync();
                return new ResultModel<List<UserAuthenticationModel>> { Success = true, Result = allUsers };
            }
            catch (Exception exception)
            {
                _logger.LogError($"UserAuthRepository: Exception when getting all users auth info's {exception.Message}");

                return new ResultModel<List<UserAuthenticationModel>> { Success = false, Exception = true, Message = "A problem occured when trying get all existing Users Auth Info's" };
            }
        }

        public async Task<ResultModel<UserAuthenticationModel>> GetUserAuthInfoByEmail(string userEmail)
        {
            try
            {
                var userAuthInfo = await _databaseContext.UsersAuthInfos.FirstOrDefaultAsync(user => user.Email == userEmail);

                if (userAuthInfo == null)
                {
                    return new ResultModel<UserAuthenticationModel> { Success = false, Message = "User not found" };
                }

                return new ResultModel<UserAuthenticationModel> { Success = true, Result = userAuthInfo };
            }
            catch (Exception exception)
            {
                _logger.LogError($"UserAuthRepository: Exception when getting the user with Email {userEmail} Exception - {exception.Message}");

                return new ResultModel<UserAuthenticationModel> { Success = false, Exception = true, Message = $"A problem occured when trying to get the user with Email {userEmail}" };
            }
        }

        public async Task<ResultModel<UserAuthenticationModel>> GetUserAuthInfoById(Guid userId)
        {
            try
            {
                var userAuthInfo = await _databaseContext.UsersAuthInfos.Include("UserProfileModel.UserPosts").FirstOrDefaultAsync(user => user.UserId == userId);

                if (userAuthInfo == null)
                {
                    return new ResultModel<UserAuthenticationModel> { Success = false, Message = "User not found" };
                }

                return new ResultModel<UserAuthenticationModel> { Success = true, Result = userAuthInfo };
            }
            catch (Exception exception)
            {
                _logger.LogError($"UserAuthRepository: Exception when getting the user with id {userId} Exception - {exception.Message}");

                return new ResultModel<UserAuthenticationModel> { Success = false, Exception = true, Message = $"A problem occured when trying to get the user with id {userId}" };
            }
        }

        public async Task<ResultModel<UserAuthenticationModel>> InsertUser(UserAuthenticationModel userAuthModel)
        {
            try
            {
                await _databaseContext.AddAsync(userAuthModel);
                await _databaseContext.SaveChangesAsync();
                return new ResultModel<UserAuthenticationModel> { Success = true, Result = userAuthModel };
            }
            catch (SqlException exception)
            {
                _logger.LogError($"UserAuthRepository: Exception when creating a new user {exception.Message}");
                return new ResultModel<UserAuthenticationModel> { Success = false, Exception = true, Message = "A problem occured when trying to add a new User" };
            }
        }

        public async Task<ResultModel<UserAuthenticationModel>> UpdateUser(Guid userId, UserAuthenticationModel userAuthModel)
        {
            var getUser = await _databaseContext.UsersAuthInfos.FirstOrDefaultAsync(user => user.UserId == userId);

            if (getUser != null)
            {
                try
                {
                    _mapper.Map(userAuthModel, getUser);

                    await _databaseContext.SaveChangesAsync();

                    return new ResultModel<UserAuthenticationModel> { Success = true, Message = "Updated with success in Database" };
                }
                catch (SqlException exception)
                {
                    _logger.LogError($"UserAuthRepository: Exception when updating post {exception.Message}");
                    return new ResultModel<UserAuthenticationModel> { Success = false, Exception = true, Message = "A problem occured when trying to update the existing User" };
                }
            }

            return new ResultModel<UserAuthenticationModel> { Success = false, Message = "User not found!" };
        }
    }
}
