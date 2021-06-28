namespace TourPortal.Storage
{
    using Microsoft.EntityFrameworkCore.Design;

    // used documentation https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            throw new System.NotImplementedException();
        }
    }
}