namespace TourPortal.Infrastructure.Models.Authentication
{
    using System.ComponentModel.DataAnnotations;

    // TODO: add more validation properties
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}