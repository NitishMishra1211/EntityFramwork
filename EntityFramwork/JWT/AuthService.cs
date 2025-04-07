using Models;
using NuGet.Common;
using System.Drawing.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using web.JWT;

public class AuthService : IAuthService
{

    private readonly IHttpClientFactory httpClientFactory;
    private readonly string apiUrl = "http://localhost:5095/api/Auth/login";
    private readonly IHttpContextAccessor httpContextAccessor;
    public AuthService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> LoginAsync(LoginViewModel model)
    {

        var client = httpClientFactory.CreateClient();
        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(apiUrl, content);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var tokenResult = await response.Content.ReadFromJsonAsync<TokenResponse>();
        httpContextAccessor.HttpContext.Session.SetString("JwtToken", tokenResult.Token);
        return tokenResult.Token;
    }

}