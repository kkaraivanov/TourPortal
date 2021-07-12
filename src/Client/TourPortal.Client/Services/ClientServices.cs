namespace TourPortal.Client.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Infrastructure.Services;
    using Infrastructure.Shared.Models.Authentication;
    using Infrastructure.Shared.Models.Response;

    public class ClientServices : IClientServices
    {
        private readonly HttpClient _client;

        public ClientServices(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApplicationResponse<UserRolesRespons>> GetUserRoles()
        {
            var response = await _client.GetFromJsonAsync<ApplicationResponse<UserRolesRespons>>("api/account");
            
            return response;
        }
    }
}