namespace TourPortal.Infrastructure.Storage.Templates
{
    using System;

    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}