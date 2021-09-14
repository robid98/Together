using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Together.Data.Models;

namespace Together.Data.Repositories.Interfaces
{
    public interface IUserAuthenticationRepository
    {
        Task<ResultModel<List<UserAuthenticationModel>>> GetAllUsers();

        Task<ResultModel<UserAuthenticationModel>> GetUserAuthInfoByEmail(String userEmail);

        Task<ResultModel<UserAuthenticationModel>> InsertUser(UserAuthenticationModel userAuthModel);

        Task<ResultModel<UserAuthenticationModel>> DeleteUser(String userEmail);

        Task<ResultModel<UserAuthenticationModel>> UpdateUser(String userEmail, UserAuthenticationModel userAuthModel);
    }
}
