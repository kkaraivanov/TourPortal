﻿namespace TourPortal.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components.Authorization;

    using Infrastructure.Global;
    using Infrastructure.Services;
    using Infrastructure.Shared.Models.Authentication;
    using Infrastructure.Shared.Models.Response;

    class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public ApiService(
            HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<ApplicationResponse<LoginResponseModel>> Login(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsync(Global.Routes.Login,
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

        public async Task<ApplicationResponse<RegisterResponseModel>> Register(RegisterModel registerModel) =>
            await Post<RegisterModel, RegisterResponseModel>(Global.Routes.Register, registerModel);

        public async Task<ApplicationResponse<UserRolesRespons>> GetUserRoles() =>
        await _httpClient.GetFromJsonAsync<ApplicationResponse<UserRolesRespons>>(Global.Routes.GetRoles);

        public async Task<ApplicationResponse<UserInfoResponse>> GetUserInfo(string userEmail) =>
            await _httpClient.GetFromJsonAsync<ApplicationResponse<UserInfoResponse>>(Global.Routes.GetUserInfo);


        private async Task<ApplicationResponse<TResponse>> Post<TRequest, TResponse>(string url, TRequest request)
        {
            (_authenticationStateProvider as ApiAuthenticationStateProvider)?.GetAuthenticationStateAsync();

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, request);
                var responseObject = await response.Content.ReadFromJsonAsync<ApplicationResponse<TResponse>>();
                return responseObject;
            }
            catch (Exception ex)
            {
                return new ApplicationResponse<TResponse>(new ApplicationError("HTTP Client", ex.Message));
            }
        }
    }
}