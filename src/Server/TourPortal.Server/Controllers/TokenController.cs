namespace TourPortal.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class TokenController : ApiController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok();
        }
    }
}