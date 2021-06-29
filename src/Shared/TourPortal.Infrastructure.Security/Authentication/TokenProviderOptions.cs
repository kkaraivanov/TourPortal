namespace TourPortal.Infrastructure.Security.Authentication
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.IdentityModel.Tokens;

    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/token";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; }

        public Func<Task<string>> SomeString { get; set; } = () => Task.FromResult(Guid.NewGuid().ToString());

        public SigningCredentials SigningCredentials { get; set; }
    }
}