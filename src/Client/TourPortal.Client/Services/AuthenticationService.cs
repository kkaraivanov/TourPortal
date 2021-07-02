﻿namespace TourPortal.Client.Services
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;
    using Infrastructure.GlobalTypes;
    using Infrastructure.Models.Authentication;
    using Infrastructure.Models.Response;
    using Microsoft.AspNetCore.Components.Authorization;

    class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        
        public AuthenticationService(HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<ApplicationResponse<LoginResponseModel>> Login(LoginModel loginModel)
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PostAsync(ApplicationConstants.LoginUrl, new StringContent(loginAsJson, Encoding.UTF8, ApplicationConstants.JsonContentType));
            var loginResult = JsonSerializer.Deserialize<ApplicationResponse<LoginResponseModel>>(await response.Content.ReadAsStringAsync(), 
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorage.SetItemAsync(ApplicationConstants.TokenString, loginResult.ResponseData.Token);
            var provaider = _authenticationStateProvider as ApiAuthenticationStateProvider;

            provaider?.SetUserAsAuthenticated(loginModel.Email);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ApplicationConstants.TokenType, loginResult.ResponseData.Token);

            return loginResult;
        }

        public Task Logout()
        {
            throw new System.NotImplementedException();
        }

        public Task<ApplicationResponse<RegisterResponseModel>> Register(RegisterModel registerModel)
        {
            throw new System.NotImplementedException();
        }
    }
}