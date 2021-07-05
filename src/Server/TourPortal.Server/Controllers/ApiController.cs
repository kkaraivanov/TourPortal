namespace TourPortal.Server.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Shared.Models.Response;

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiController : Controller
    {
        protected ApplicationResponse<T> Error<T>(string from, string message)
        {
            return new ApplicationResponse<T>(new ApplicationError(from, message));
        }

        protected ApplicationResponse<T> ModelStateErrors<T>()
        {
            if (ModelState == null || ModelState.Count == 0)
            {
                return new ApplicationResponse<T>(new ApplicationError("Model is invalid", "Model can't by null or empty."));
            }

            var errors = new List<ApplicationError>();

            foreach (var (from, modelErrors) in ModelState)
            {
                foreach (var error in modelErrors.Errors)
                {
                    errors.Add(new ApplicationError(from, error.ErrorMessage));
                }
            }

            return new ApplicationResponse<T>(errors);
        }
    }
}