namespace TourPortal.Infrastructure.Storage.Templates
{
    using System;

    public interface IAuditable
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}