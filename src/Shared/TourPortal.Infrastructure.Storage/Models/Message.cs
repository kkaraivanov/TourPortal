namespace TourPortal.Infrastructure.Storage.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Templates;

    public class Message : IAuditable, ISoftDelete
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string TextMessage { get; set; }

        public DateTime When { get; set; }

        public string UserID { get; set; }

        public ApplicationUser Sender { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}