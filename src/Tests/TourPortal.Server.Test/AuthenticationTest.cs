using System;
using Xunit;

namespace TourPortal.Server.Test
{
    using Infrastructure.Shared.Models.Authentication;
    using MyTested.AspNetCore.Mvc;
    using MyTested.AspNetCore.Mvc.Utilities.Extensions;
    using Server.Controllers;

    public class AuthenticationTest
    {
        private const string loginJsonBody = @"{""email"":admin@test.com,""password"":""Admin!23""}";

        [Fact]
        public void LoginMiddlewareShouldAceptRouteWithMethodPost() =>
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/api/account/login")
                    .AndAlso()
                    .WithFormFields(loginJsonBody));

        [Fact]
        public void LoginMiddlewareShouldReturnNonExistingRouteWithMethodGet() =>
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/api/account/register")
                    .AndAlso()
                    .WithFormFields(model))
                .To<AccountController>(c => 
                    c.Register(With.Empty<RegisterModel>()));


        private RegisterModel model => new RegisterModel
        {
            Email = "test@test.com",
            Password = "Admin!23",
            ConfirmPassword = "Admin!23",
            FirstName = "Test",
            MidleName = "Test",
            LastName = "Test",
            PhoneNumber = "0888888888",
            RoleId = "lklsdklsakdlkasd",
            UserName = "testuser"
        };
    }
}
