﻿namespace TourPortal.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;

    using Infrastructure.Global;
    using Infrastructure.Models.Authentication;
    using Infrastructure.Models.Response;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.WebAssembly.Http;

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
            var response = await _httpClient.PostAsync(ApplicationConstants.LoginUrl,
                new FormUrlEncodedContent(
                    new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("email", loginModel.Email),
                        new KeyValuePair<string, string>("password", loginModel.Password),
                    }));

            var request = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new ApplicationResponse<LoginResponseModel>(new ApplicationError("Login failed", request));
            }

            var loginResult = JsonSerializer
                .Deserialize<LoginResponseModel>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            
            await _localStorage.SetItemAsync(ApplicationConstants.AuthenticatedTokenString, loginResult.access_token);

            // TODO: if have the error, can make ((ApiAuthenticationStateProvider)_authenticationStateProvider).SetUserAsAuthenticated(loginModel.Email);
            (_authenticationStateProvider as ApiAuthenticationStateProvider)?.SetUserAsAuthenticated(loginResult.access_token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                ApplicationConstants.TokenType,
                loginResult.access_token);

            return new ApplicationResponse<LoginResponseModel>(loginResult);
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(ApplicationConstants.AuthenticatedTokenString);
            (_authenticationStateProvider as ApiAuthenticationStateProvider)?.UserAsLoggedOut();

            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<ApplicationResponse<RegisterResponseModel>> Register(RegisterModel registerModel)
        {
            var response = await _httpClient.PostAsJsonAsync(ApplicationConstants.RegisterUrl, registerModel);
            var responseResult = await response.Content.ReadFromJsonAsync<RegisterResponseModel>();

            return new ApplicationResponse<RegisterResponseModel>(responseResult);
        }
    }
}