namespace TourPortal.Infrastructure.Shared.Models.Response
{
    public class ApplicationError
    {
        public string From { get; set; }

        public string Error { get; set; }

        public ApplicationError(string from, string error)
        {
            From = from;
            Error = error;
        }

        public override string ToString()
        {
            return $"{From}: {Error}";
        }
    }
}