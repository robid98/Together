using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Together.Data;
using Together.Data.DTOs;
using Together.Data.Models;
using Together.Services.Enums;

namespace Together.Services.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<ResultModel<UserAuthenticationDTO>> AddNewUser(UserAuthenticationModel user);

        Task<ResultModel<UserAuthenticationDTO>> GetUserById(Guid userId);

        Task<ResultModel<UserAuthenticationDTO>> UpdateExistingUser(Guid userId, UserAuthenticationModel user);

        Task<ResultModel<UserAuthenticationDTO>> DeleteExistingUser(Guid userId);

        Task<ResultModel<List<UserAuthenticationDTO>>> GetAllUsersAuthInfos();

        Task<bool> UserAlreadyExists(String userEmail);

        PasswordScore CheckUserStrenghtPassword(String password);

        Task<ResultModel<UserAuthenticationDTO>> ValidUserChecks(UserAuthenticationModel user);
    }
}
