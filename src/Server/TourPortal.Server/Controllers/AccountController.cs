namespace TourPortal.Server.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    using Infrastructure.Shared.Models.Authentication;
    using Infrastructure.Shared.Models.Response;

    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AccountController : ApiController
    {
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Administrator")]
        //[Authorize(Roles = "Employe")]
        //[Authorize(Policy = "IsUser")]
        //[Authorize(Policy = "IsAdministrator")]
        public async Task<IActionResult> TestToken()
        {
            return Ok("Hallo from account controller");
        }

        [HttpPost]
        public async Task<ApplicationResponse<RegisterResponseModel>> Register([FromBody] RegisterModel model)
        {
            if (model is null)
            {
                
            }
            return new ApplicationResponse<RegisterResponseModel>();
        }
    }
}