namespace TourPortal.Infrastructure.Shared.Models
{
    using System;

    public class UserDataModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string MidleName { get; set; }

        public string LastName { get; set; }

        public string Sity { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}