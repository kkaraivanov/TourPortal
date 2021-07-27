namespace TourPortal.Infrastructure.Shared.Models.Hotel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddNewRoomModel
    {
        [Required]
        public int RoomNumber { get; set; }

        [Required]
        [Range(1, 6, ErrorMessage = "Can only be between 1 .. 6")]
        public int CountOfBeds { get; set; }

        [Required]
        [Range(1, 6, ErrorMessage = "Can only be between 1 .. 6")]
        public int CountOfPersons { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string RoomType { get; set; }

        public List<string> RoomImages { get; set; } = new List<string>();
    }
}