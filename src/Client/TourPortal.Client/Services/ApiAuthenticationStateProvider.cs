namespace TourPortal.Client.Services
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components.Authorization;

    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}