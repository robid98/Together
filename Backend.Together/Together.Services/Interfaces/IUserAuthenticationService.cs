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

        Task<ResultModel<UserAuthenticationDTO>> GetUserByEmail(String userEmail);

        Task<ResultModel<UserAuthenticationDTO>> UpdateExistingUser(String email, UserAuthenticationModel user);

        Task<ResultModel<UserAuthenticationDTO>> DeleteExistingUser(String email);

        Task<ResultModel<List<UserAuthenticationDTO>>> GetAllUsersAuthInfos();

        Task<bool> UserAlreadyExists(String email);

        PasswordScore CheckUserStrenghtPassword(String password);

        Task<ResultModel<UserAuthenticationDTO>> ValidUserChecks(UserAuthenticationModel user);
    }
}
