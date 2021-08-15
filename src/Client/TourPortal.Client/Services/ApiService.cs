namespace TourPortal.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components.Authorization;
    using Blazored.LocalStorage;

    using Infrastructure.Global;
    using Infrastructure.Services;
    using Infrastructure.Shared.Models;
    using Infrastructure.Shared.Models.Authentication;
    using Infrastructure.Shared.Models.Hotel;
    using Infrastructure.Shared.Models.Response;
    using static Infrastructure.Global.Global;

    class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly ILogedUserService _loginUserService;

        public ApiService(
            HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage,
            ILogedUserService loginUserService)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _loginUserService = loginUserService;
        }

        public async Task<ApplicationResponse<LoginResponseModel>> Login(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsync(Route.Login,
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

        public async Task<bool> ValidateToken()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var token = await _localStorage.GetItemAsync<string>(ApplicationConstants.AuthenticatedTokenString);

            if (state.User.Identity.IsAuthenticated)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.GetAsync("api/token");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized && response.Headers.Contains("Token-Expired"))
            {
                return false;
            }

            return false;
        }

        #region GET requests

        public async Task<ApplicationResponse<UserRolesRespons>> GetUserRoles() =>
            await Get<UserRolesRespons>(Route.GetRoles);

        public async Task<ApplicationResponse<UserInfoResponse>> GetUserInfo(string userEmail) =>
            await Get<UserInfoResponse>(Route.GetUserInfo + userEmail);

        public async Task<ApplicationResponse<IEnumerable<RoomResponse>>> GetRooms(string hotelId, int skip, int take) =>
            await Get<IEnumerable<RoomResponse>>($"{Route.GetRooms}hotelId={hotelId}&skip={skip}&take={take}");

        public async Task<ApplicationResponse<GetRoomByIdResponse>> GetRoomById(string roomId) =>
            await Get<GetRoomByIdResponse>(Route.GetRoomById + roomId);
        
        public async Task<ApplicationResponse<List<RoomResponse>>> GetRoomByRoomNumber(string hotelId, string roomNumber) =>
            await Get<List<RoomResponse>>($"{Route.GetRoomByRoomNumber}hotelId={hotelId}&roomNumber={roomNumber}");

        public async Task<ApplicationResponse<ICollection<string>>> GetRoomTypes() =>
            await Get<ICollection<string>>(Route.GetRoomTypes);

        public async Task<ApplicationResponse<int>> GetRoomsCount(string hotelId) =>
            await Get<int>(Route.GetRoomsCount + hotelId);

        public async Task<ApplicationResponse<HotelInfoResponse>> GetHotelInfo() =>
            await Get<HotelInfoResponse>(Route.GetHotelInfo);

        public async Task<ApplicationResponse<HotelInfoResponse>> GetHotelCardInfo(string hotelId) =>
            await Get<HotelInfoResponse>(Route.GetHotelCardInfo + hotelId);

        public async Task<ApplicationResponse<EmployeInfoResponse>> GetEmployeInfo() =>
            await Get<EmployeInfoResponse>(Route.GetEmployeInfo);
        
        public async Task<ApplicationResponse<IEnumerable<GetEmployeResponse>>> GetEmployes() =>
            await Get<IEnumerable<GetEmployeResponse>>(Route.GetEmployes);
        
        public async Task<ApplicationResponse<FullUserDataModel>> GetEmploye(string employeId) =>
            await Get<FullUserDataModel>(Route.GetEmploye + employeId);

        public async Task<ApplicationResponse<FullUserDataModel>> GetUserData() =>
            await Get<FullUserDataModel>(Route.GetUserData);

        #endregion

        #region POST requests

        public async Task<ApplicationResponse<RegisterResponseModel>> Register(RegisterModel registerModel) =>
            await Post<RegisterModel, RegisterResponseModel>(Route.Register, registerModel);

        public async Task<ApplicationResponse<RegisterResponseModel>> RegisterEmploye(RegisterModel registerModel) =>
            await Post<RegisterModel, RegisterResponseModel>(Route.RegisterEmploye, registerModel);

        public async Task<ApplicationResponse<HotelInfoResponse>> AddNewHotel(AddHotelModel hotelModel) =>
            await Post<AddHotelModel, HotelInfoResponse>(Route.AddNewHotel, hotelModel);

        public async Task<ApplicationResponse<bool>> ChangeHotel(ChangeHotelModel hotelModel) =>
            await Post<ChangeHotelModel, bool>(Route.ChangeHotel, hotelModel);

        public async Task<ApplicationResponse<bool>> AddNewRoom(RoomModel roomModel) =>
            await Post<RoomModel, bool>(Route.AddNewRoom, roomModel);
        
        public async Task<ApplicationResponse<bool>> ChangedRoom(RoomModel roomModel) =>
            await Post<RoomModel, bool>(Route.ChangedRoom, roomModel);

        public async Task<ApplicationResponse<bool>> ChangeUserData(UserSettingModel model) =>
            await Post<UserSettingModel, bool>(Route.ChangeUserData, model);

        public async Task<ApplicationResponse<bool>> DeleteUserData(UserSettingModel model) =>
            await Post<UserSettingModel, bool>(Route.ChangeUserData, model);
        
        public async Task<ApplicationResponse<bool>> DeleteEmploye(DeletableUserModel model) =>
            await Post<DeletableUserModel, bool>(Route.DeleteEmployeData, model);
        
        public async Task<ApplicationResponse<bool>> DeleteUser(DeletableUserModel model) =>
            await Post<DeletableUserModel, bool>(Route.DeleteUser, model);

        #endregion

        #region Helper private methods

        private async Task<ApplicationResponse<T>> Get<T>(string url)
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var token = await _localStorage.GetItemAsync<string>(ApplicationConstants.AuthenticatedTokenString);
            
            if (state.User.Identity.IsAuthenticated)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApplicationResponse<T>>(url);

                ;

                return response;
            }
            catch (Exception ex)
            {
                return new ApplicationResponse<T>(new ApplicationError("HTTP Client", ex.Message));
            }
        }

        private async Task<ApplicationResponse<TResponse>> Post<TRequest, TResponse>(string url, TRequest request)
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var token = await _localStorage.GetItemAsync<string>(ApplicationConstants.AuthenticatedTokenString);

            if (state.User.Identity.IsAuthenticated)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

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

        #endregion
    }
}