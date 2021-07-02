namespace TourPortal.Infrastructure.Storage
{
    using System.Threading.Tasks;

    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}