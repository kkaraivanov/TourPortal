namespace TourPortal.Server.Test.Mocks
{
    using Infrastructure.Services;
    using Infrastructure.Shared.Models.Response;
    using Moq;

    public class AccountServiceMock
    {
        public static IAccountService AddUserProfileInstace
        {
            get
            {
                var mock = new Mock<IAccountService>();
                var userId = "lskdjflsdkfj";
                var userName = "testName";
                mock.Setup(x => x.AddUserProfile(userId, userName))
                    .ToResponse();

                return mock.Object;
            }
        }
    }
}