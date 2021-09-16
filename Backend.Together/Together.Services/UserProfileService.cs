using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Together.Data;
using Together.Data.BlobStorage;
using Together.Data.DTOs;
using Together.Data.Models;
using Together.Data.Repositories.Interfaces;
using Together.Services.Interfaces;

namespace Together.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserProfileService> _logger;
        private readonly IBlobStorage _blobStorage;

        public UserProfileService(IUserProfileRepository userProfileRepository, 
            IMapper mapper, 
            ILogger<UserProfileService> logger,
            IBlobStorage blobStorage)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
            _logger = logger;
            _blobStorage = blobStorage;
        }

        public async Task<ResultModel<UserProfileDTO>> AddNewUserProfile(Guid userAuthenticationGuid, UserProfileModel user)
        {
            _logger.LogInformation("UserProfileService: Adding a new User Profile!");

            user.UserProfileId = Guid.NewGuid();
            var resultInsert = await _userProfileRepository.InsertUserProfile(userAuthenticationGuid, user);

            return new ResultModel<UserProfileDTO>
            {
                Success = resultInsert.Success,
                Exception = resultInsert.Exception,
                Message = resultInsert.Message,
                Result = (resultInsert.Result != null) ? _mapper.Map<UserProfileDTO>(resultInsert.Result) : null
            };
        }

        public async Task<ResultModel<UserProfileDTO>> DeleteExistingUserProfile(Guid userAuthenticationGuid)
        {
            _logger.LogInformation($"UserAuth Service: Deleting the user profile with Id {userAuthenticationGuid}!");

            var deletingUserResult = await _userProfileRepository.DeleteUserProfile(userAuthenticationGuid);

            return new ResultModel<UserProfileDTO> { Success = deletingUserResult.Success, Message = deletingUserResult.Message, Exception = deletingUserResult.Exception };
        }

        public async Task<ResultModel<UserProfileDTO>> GetUserProfileById(Guid userAuthenticationGuid)
        {
            _logger.LogInformation($"UserProfileService: Getting the user profile with the Auth Id {userAuthenticationGuid}");

            var specificUserResult = await _userProfileRepository.GetUserProfileByGuid(userAuthenticationGuid);

            return new ResultModel<UserProfileDTO>
            {
                Success = specificUserResult.Success,
                Exception = specificUserResult.Exception,
                Message = specificUserResult.Message,
                Result = specificUserResult.Result != null ?
                                _mapper.Map<UserProfileModel, UserProfileDTO>(specificUserResult.Result) : null
            };
        }

        public async Task<ResultModel<UserProfileDTO>> UpdateExistingUserProfile(Guid userAuthenticationGuid, UserProfileModel userProfile)
        {
            _logger.LogInformation("UserProfileService Service: Updating a User Profile!");

            var updateResult = await _userProfileRepository.UpdateUserProfile(userAuthenticationGuid, userProfile);

            return new ResultModel<UserProfileDTO>
            {
                Success = updateResult.Success,
                Exception = updateResult.Exception,
                Message = updateResult.Message
            };
        }

        public async Task<ResultModel<UserProfileDTO>> UpdateUserProfilePicture(Guid userAuthenticationGuid, FileModel fileModel)
        {
            var userProfile = await GetUserProfileById(userAuthenticationGuid);

            if (!userProfile.Success)
                return userProfile;

            if (fileModel.ImageFile == null)
            {
                return new ResultModel<UserProfileDTO> { Success = false, Exception = true, Message = "Invalid image" };
            }

            /* Verify if user already have a profile Photo - and delete it from Blob */
            if(userProfile.Result.UserProfileImgBlobLink.Length > 0)
            {
                var splitBlobLink = userProfile.Result.UserProfileImgBlobLink.Split('/');

                var deleteBlobResult = await _blobStorage.DeleteBlobAsync(splitBlobLink[splitBlobLink.Length - 1]);

                if(!deleteBlobResult.Success)
                {
                    return new ResultModel<UserProfileDTO>
                    {
                        Success = deleteBlobResult.Success,
                        Exception = deleteBlobResult.Exception,
                        Message = deleteBlobResult.Message
                    };
                }
            }

            var userProfilePictureGuid = Guid.NewGuid();

            var blobName = $"{userProfilePictureGuid}_{fileModel.ImageFile.FileName}";

            var blobStorageUpdateResult = await _blobStorage.UploadContentBlobAsync(fileModel.ImageFile, blobName);

            if (blobStorageUpdateResult.Success)
            {
                /* Get blob link */
                var blobLinkResult = _blobStorage.GetBlobLink(blobName);

                if(!blobLinkResult.Success)
                {
                    return new ResultModel<UserProfileDTO>
                    {
                        Success = blobStorageUpdateResult.Success,
                        Exception = blobStorageUpdateResult.Exception,
                        Message = blobStorageUpdateResult.Message
                    };
                }

                var userProfileUpdated = new UserProfileModel { UserProfileImgBlobLink = blobLinkResult.Result };

                var updateResult = await UpdateExistingUserProfile(userAuthenticationGuid, userProfileUpdated);

                if(!updateResult.Success)
                {
                    return new ResultModel<UserProfileDTO>
                    {
                        Success = blobStorageUpdateResult.Success,
                        Exception = blobStorageUpdateResult.Exception,
                        Message = blobStorageUpdateResult.Message
                    };
                }

                return new ResultModel<UserProfileDTO> { Success = true };
            }

            return new ResultModel<UserProfileDTO>
            {
                Success = blobStorageUpdateResult.Success,
                Exception = blobStorageUpdateResult.Exception,
                Message = blobStorageUpdateResult.Message
            };
        }
    }
}
