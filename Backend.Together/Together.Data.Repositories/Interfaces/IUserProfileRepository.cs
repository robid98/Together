using System;
using System.Threading.Tasks;
using Together.Data.Models;

namespace Together.Data.Repositories.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<ResultModel<UserProfileModel>> GetUserProfileByGuid(Guid userAuthenticationGuid);

        Task<ResultModel<UserProfileModel>> InsertUserProfile(Guid userAuthenticationGuid, UserProfileModel userProfileModel);

        Task<ResultModel<UserProfileModel>> DeleteUserProfile(Guid userAuthenticationGuid);

        Task<ResultModel<UserProfileModel>> UpdateUserProfile(Guid userAuthenticationGuid, UserProfileModel userProfileModel);
    }
}
