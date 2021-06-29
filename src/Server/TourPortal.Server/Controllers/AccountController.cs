namespace TourPortal.Server.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            return Ok();
        }
    }

    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}