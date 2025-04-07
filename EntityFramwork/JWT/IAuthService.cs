using Models;

namespace web.JWT
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginViewModel model);
        //Task<Tokens> GetUserDetailsAsync(string email);
        //Task<string> RefreshTokenAsync(string token, string refreshToken);
        //Task<string> MakeAuthenticatedApiCallAsync(string endpoint, string token);
    }
}