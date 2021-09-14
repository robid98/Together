using Together.Data.Repositories.Interfaces;
using Together.Services.Interfaces;

namespace Together.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserAuthenticationRepository _userAuthenticationRepository;

        public LoginService(IUserAuthenticationRepository userAuthenticationRepository)
        {
            _userAuthenticationRepository = userAuthenticationRepository;
        }
    }
}
