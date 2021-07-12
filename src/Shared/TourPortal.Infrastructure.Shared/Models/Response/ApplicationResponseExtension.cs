namespace TourPortal.Infrastructure.Shared.Models.Response
{
    public static class ApplicationResponseExtension
    {
        public static ApplicationResponse<T> ToResponse<T>(this T data)
        {
            return new ApplicationResponse<T>(data);
        }
    }
}