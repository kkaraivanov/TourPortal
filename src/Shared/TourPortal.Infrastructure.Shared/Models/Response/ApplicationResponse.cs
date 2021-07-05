namespace TourPortal.Infrastructure.Shared.Models.Response

{
    using System.Collections.Generic;
    using System.Linq;

    public class ApplicationResponse<T>
    {
        public IEnumerable<ApplicationError> Errors { get; set; }

        public T ResponseData { get; set; }

        public bool IsOk => !this.Errors?.Any() ?? true;

        public ApplicationResponse() { }

        public ApplicationResponse(T responseData) =>
            ResponseData = responseData;

        public ApplicationResponse(ApplicationError error) =>
            Errors = new List<ApplicationError> { error };

        public ApplicationResponse(IEnumerable<ApplicationError> errors)
        {
            if (errors == null || !errors.Any())
            {
                Errors = new List<ApplicationError> { new ApplicationError("ApplicationResponse", "Unspecified error.") };
            }
            else
            {
                Errors = errors;
            }
        }
    }
}