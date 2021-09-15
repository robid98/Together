using System;
using System.IO;
using System.Threading.Tasks;
using Together.Data;
using Together.Data.DTOs;
using Together.Data.Models;

namespace Together.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<ResultModel<UserProfileDTO>> AddNewUserProfile(Guid userAuthenticationGuid, UserProfileModel user);

        Task<ResultModel<UserProfileDTO>> GetUserProfileById(Guid userAuthenticationGuid);

        Task<ResultModel<UserProfileDTO>> DeleteExistingUserProfile(Guid userAuthenticationGuid);

        Task<ResultModel<UserProfileDTO>> UpdateExistingUserProfile(Guid userAuthenticationGuid, UserProfileModel userProfile);

        Task<ResultModel<UserProfileDTO>> UpdateUserProfilePicture(Guid userAuthenticationGuid, FileModel fileModel);
    }
}
