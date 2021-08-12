namespace TourPortal.Client.Areas.Shared
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Components;
    using Data;
    using Infrastructure.Global.Types;
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;
    using Newtonsoft.Json;

    public partial class AreaLayout
    {
        private string[] scriptPaths = new string[]
        {
            "css/area-style/js/animate.js",
            "css/area-style/js/bootstrap-select.js",
            "css/area-style/js/bootstrap-datepicker.js",
            "css/area-style/js/owl.carousel.js",
            "css/area-style/js/utils.js",
            "css/area-style/js/perfect-scrollbar.min.js",
            "css/area-style/js/site.js"
        };
        private string[] cssPaths = new string[]
        {
            "css/area-style/css/bootstrap.min.css",
            "css/area-style/css/bootstrap-datepicker.css",
            "css/area-style/style.css",
            "css/area-style/css/responsive.css",
            "css/area-style/css/perfect-scrollbar.css",
            "css/area-style/css/custom.css"
        };
        private bool areaIsReady = false;
        private byte[] ProfileImage;
        private string ProfileName = string.Empty;
        private string UserRole = string.Empty;
        private string Id = string.Empty;

        public EventCallback SetUserInfo => EventCallback.Factory.Create(this, SetUserInfoProparties);

        public async Task SetUserInfoProparties()
        {
            await LoadUserData();
        }

        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine("Hallo from OnInitialized!");
            var module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/site.js");

            if (!areaIsReady)
            {
                await RemoveElementsFromTemplate(module);
                Thread.Sleep(500);
                await AddStyles(module);
                Thread.Sleep(500);
                await AddScripts(module);
                areaIsReady = true;

                StateHasChanged();
            }

            var state = await _state.GetAuthenticationStateAsync();

            if (!state.User.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/error/notauthorized", true);
            }

            await LoadUserData();

            //if (state.User.IsInRole(Security.Role.Owner))
            //{
            //    var hotelInfoRequest = await ApiService.GetHotelInfo();
            //
            //    if (hotelInfoRequest.IsOk)
            //    {
            //        var responseData = hotelInfoRequest.ResponseData;
            //
            //        if (responseData != null)
            //        {
            //            var serialize = JsonConvert.SerializeObject(responseData);
            //            var hotel = JsonConvert.DeserializeObject<HotelInfoModel>(serialize);
            //
            //            User.AddHotel(hotel);
            //            StateHasChanged();
            //        }
            //    }
            //}
            //
            //if (state.User.IsInRole(Security.Role.Employe))
            //{
            //    var employeInfoRequest = await ApiService.GetEmployeInfo();
            //    var responseData = employeInfoRequest.ResponseData;
            //
            //    if (responseData != null)
            //    {
            //        var hoteCardlInfo = await ApiService.GetHotelCardInfo(responseData.HotelId);
            //        var hotelResponseData = hoteCardlInfo.ResponseData;
            //
            //        if (hotelResponseData != null)
            //        {
            //            var serialize = JsonConvert.SerializeObject(hotelResponseData);
            //            var hotel = JsonConvert.DeserializeObject<HotelInfoModel>(serialize);
            //
            //            User.AddHotel(hotel);
            //            StateHasChanged();
            //        }
            //    }
            //}
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                var checkValidateToken = await ApiService.ValidateToken();
                if (!checkValidateToken)
                {
                    await ApiService.Logout();
                    StateHasChanged();
                    NavigationManager.NavigateTo("/", true);
                }
            }
            else
            {
                var state = await _state.GetAuthenticationStateAsync();

                if (state.User.IsInRole(Security.Role.Owner))
                {
                    await LoadOwnerData();
                }

                if (state.User.IsInRole(Security.Role.Employe))
                {
                    await LoadEmplyeData();
                }
            }
        }

        private async Task LoadUserData()
        {
            var state = await _state.GetAuthenticationStateAsync();
            var userData = state.User.Identity.Name;
            var request = await ApiService.GetUserInfo(userData);

            if (request.IsOk)
            {
                var responseData = request.ResponseData;
                var user = new LogedUserModel
                {
                    Id = responseData.Id,
                    ProfileName = responseData.ProfileName,
                    ProfileImage = responseData.ProfileImage,
                    UserRole =
                        responseData.UserRole.Contains(Security.Role.Administrator) ? "Администратор" :
                        responseData.UserRole.Contains(Security.Role.Owner) ? "Собственик хотел" :
                        responseData.UserRole.Contains(Security.Role.Employe) ? "Служител хотел" :
                        "Потребител"
                };

                User.LogedInUser(user);
                Id = User.User.Id;
                ProfileName = User.User.ProfileName;
                ProfileImage = User.User.ProfileImage;
                UserRole = User.User.UserRole;

                StateHasChanged();
            }
        }

        private async Task LoadOwnerData()
        {
            var hotelInfoRequest = await ApiService.GetHotelInfo();

            if (hotelInfoRequest.IsOk)
            {
                var responseData = hotelInfoRequest.ResponseData;

                if (responseData != null)
                {
                    var serialize = JsonConvert.SerializeObject(responseData);
                    var hotel = JsonConvert.DeserializeObject<HotelInfoModel>(serialize);

                    User.AddHotel(hotel);
                    StateHasChanged();
                }
            }
        }

        private async Task LoadEmplyeData()
        {
            var employeInfoRequest = await ApiService.GetEmployeInfo();
            var responseData = employeInfoRequest.ResponseData;

            if (responseData != null)
            {
                var hoteCardlInfo = await ApiService.GetHotelCardInfo(responseData.HotelId);
                var hotelResponseData = hoteCardlInfo.ResponseData;

                if (hotelResponseData != null)
                {
                    var serialize = JsonConvert.SerializeObject(hotelResponseData);
                    var hotel = JsonConvert.DeserializeObject<HotelInfoModel>(serialize);

                    User.AddHotel(hotel);
                    StateHasChanged();
                }
            }
        }

        private async Task AddStyles(IJSObjectReference module)
        {
            foreach (var cssPath in cssPaths)
            {
                await module.InvokeVoidAsync("addCss", cssPath);
            }
        }

        private async Task AddScripts(IJSObjectReference module)
        {
            foreach (var scriptPath in scriptPaths)
            {
                await module.InvokeVoidAsync("addScripts", scriptPath);
            }
        }

        private async Task RemoveElementsFromTemplate(IJSObjectReference module)
        {
            await module.InvokeVoidAsync("removeScripts");
            await module.InvokeVoidAsync("removeCss");
        }
    }
}