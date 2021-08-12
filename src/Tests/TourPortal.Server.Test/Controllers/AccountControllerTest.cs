namespace TourPortal.Server.Test.Controllers
{
    using Infrastructure.Shared.Models;
    using Infrastructure.Shared.Models.Authentication;
    using Infrastructure.Shared.Models.Response;
    using MyTested.AspNetCore.Mvc;
    using Server.Controllers;
    using Xunit;

    public class AccountControllerTest
    {
        [Fact]
        public void RegisterActionShouldAllowedAnonymous() => MyController<AccountController>
            .Instance()
            .Calling(c => c.Register(With.Default<RegisterModel>()))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .AllowingAnonymousRequests()
                .RestrictingForHttpMethod(HttpMethod.Post))
            .AndAlso()
            .ShouldHave()
            .InvalidModelState()
            .AndAlso()
            .ShouldReturn()
            .ResultOfType<ApplicationResponse<RegisterResponseModel>>();

        [Theory]
        [InlineData(null)]
        public void RegisterActionShouldValidateModel(RegisterModel model) => 
            MyController<AccountController>
            .Instance()
            .Calling(c => c.Register(model))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .AllowingAnonymousRequests()
                .RestrictingForHttpMethod(HttpMethod.Post))
            .AndAlso()
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void RegisterEmployeShouldBeAllowedOnlyPostRequestForAuthorizedUser() => 
            MyController<AccountController>
            .Instance()
            .Calling(c => c.RegisterEmploye(With.Default<RegisterModel>()))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests()
                .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
            .AndAlso()
            .ShouldHave()
            .InvalidModelState()
            .AndAlso()
            .ShouldReturn()
            .ResultOfType<ApplicationResponse<RegisterResponseModel>>();

        [Fact]
        public void ChangeUserDataShouldBeAllowedOnlyPostRequestForAuthorizedUser() => 
            MyController<AccountController>
            .Instance()
            .Calling(c => c.ChangeUserData(With.Default<UserSettingModel>()))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
            .AndAlso()
            .ShouldHave()
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .ResultOfType<ApplicationResponse<bool>>();

        [Fact]
        public void GetUserRolesShouldByAlowedAnonimousUserGetData() => 
            MyController<AccountController>
            .Instance()
            .Calling(c => c.GetUserRoles())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .AllowingAnonymousRequests()
                .RestrictingForHttpMethod(HttpMethod.Get))
            .AndAlso()
            .ShouldReturn()
            .ResultOfType<ApplicationResponse<UserRolesRespons>>();

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public void UserInfoTestWithNullableAndEmptyDataShouldReturnErrorMessage(string data) =>
            MyController<AccountController>
                .Instance()
                .Calling(c => c.GetUserInfo(data))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ApplicationResponse<UserInfoResponse>>();
    }                                                                
}                                                 