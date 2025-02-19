using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient httpClient, NavigationManager navigationManager, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _localStorage = localStorage;
    }

    public async Task<bool> ValidateTokenAsync()
    {
        var token = await _localStorage.GetItemAsStringAsync("authToken");

        if (string.IsNullOrEmpty(token))
        {
            _navigationManager.NavigateTo("/"); // Rediriger vers la page d'accueil
            return false;
        }

        var request = new HttpRequestMessage(HttpMethod.Get, "api/auth/validateToken");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            await LogoutAsync();
            return false;
        }

        return true;
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken"); // Supprime le token
        _navigationManager.NavigateTo("/"); // Rediriger vers la page d'accueil
    }
}
