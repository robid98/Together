using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Together.Data.Models;
using Together.Data.Repositories.Interfaces;
using Together.Data.SQL;

namespace Together.Data.Repositories.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<UserProfileRepository> _logger;
        private readonly IUserAuthenticationRepository _userAuthenticationRepository;
        private readonly IMapper _mapper;

        public UserProfileRepository(DatabaseContext databaseContext, ILogger<UserProfileRepository> logger, IUserAuthenticationRepository userAuthenticationRepository, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _logger = logger;
            _userAuthenticationRepository = userAuthenticationRepository;
            _mapper = mapper;
        }

        public async Task<ResultModel<UserProfileModel>> GetUserProfileByGuid(Guid userAuthenticationGuid)
        {
            var userAuthResult = await _userAuthenticationRepository.GetUserAuthInfoById(userAuthenticationGuid);

            if (userAuthResult.Exception)
            {
                _logger.LogError($"UserProfileRepository: A problem occured when trying to get the user auth with id {userAuthenticationGuid} - {userAuthResult.Message}");
                return new ResultModel<UserProfileModel> { Success = false, Message = $"A problem occured when trying to get the user auth with id {userAuthenticationGuid}" };
            }

            if (!userAuthResult.Success)
                return new ResultModel<UserProfileModel> { Success = false, Message = "User not found" };
            
            if(userAuthResult.Result.UserProfileModel == null)
                return new ResultModel<UserProfileModel> { Success = false, Message = "Profile not found" };

            return new ResultModel<UserProfileModel> { Success = true, Result = userAuthResult.Result.UserProfileModel };
        }

        public async Task<ResultModel<UserProfileModel>> InsertUserProfile(Guid userAuthenticationGuid, UserProfileModel userProfileModel)
        {
            var userAuthResult = await _userAuthenticationRepository.GetUserAuthInfoById(userAuthenticationGuid);

            if(userAuthResult.Exception)
            {
                _logger.LogError($"UserProfileRepository: Exception when creating a new user profile {userAuthResult.Message}");
                return new ResultModel<UserProfileModel> { Success = false, Exception = true, Message = userAuthResult.Message };
            }

            if(!userAuthResult.Success)
                return new ResultModel<UserProfileModel> { Success = false, Message = userAuthResult.Message };

            userProfileModel.UserId = userAuthResult.Result.UserId;

            if(userAuthResult.Result.UserProfileModel != null)
                return new ResultModel<UserProfileModel> { Success = false, Exception = true, Message = "A profile for that user is already created" };

            try
            {
                await _databaseContext.AddAsync(userProfileModel);
                await _databaseContext.SaveChangesAsync();
                return new ResultModel<UserProfileModel> { Success = true, Result = userProfileModel };
            }
            catch (SqlException exception)
            {
                _logger.LogError($"UserProfileRepository: Exception when creating a new user {exception.Message}");
                return new ResultModel<UserProfileModel> { Success = false, Exception = true, Message = "A problem occured when trying to add a new User Profile" };
            }

        }

        public async Task<ResultModel<UserProfileModel>> DeleteUserProfile(Guid userAuthenticationGuid)
        {
            try
            {
                var userProfileResult = (await GetUserProfileByGuid(userAuthenticationGuid));
                var userProfile = userProfileResult.Result;

                if (userProfile == null)
                {
                    return userProfileResult;
                }

                _databaseContext.Remove(userProfile);
                _databaseContext.SaveChanges();

                return new ResultModel<UserProfileModel> { Success = true, Message = "User profile deleted" };
            }
            catch (SqlException exception)
            {
                _logger.LogError($"UserProfileRepository: Exception when deleting the User Profile with id {userAuthenticationGuid} Exception - {exception.Message}");

                return new ResultModel<UserProfileModel> { Success = false, Exception = true, Message = $"A problem occured when trying to delete the User Profile with Id {userAuthenticationGuid}" };
            }
        }

        public async Task<ResultModel<UserProfileModel>> UpdateUserProfile(Guid userAuthenticationGuid, UserProfileModel userProfileModel)
        {
            try
            {
                var userProfileResult = (await GetUserProfileByGuid(userAuthenticationGuid));
                var userProfile = userProfileResult.Result;

                if (userProfile == null)
                {
                    return userProfileResult;
                }

                _mapper.Map(userProfileModel, userProfile);

                _databaseContext.SaveChanges();

                return new ResultModel<UserProfileModel> { Success = true, Message = "User profile updated" };
            }
            catch (SqlException exception)
            {
                _logger.LogError($"UserProfileRepository: Exception when updating the User Profile with id {userAuthenticationGuid} Exception - {exception.Message}");

                return new ResultModel<UserProfileModel> { Success = false, Exception = true, Message = $"A problem occured when trying to update the User Profile with Id {userAuthenticationGuid}" };
            }
        }
    }
}
