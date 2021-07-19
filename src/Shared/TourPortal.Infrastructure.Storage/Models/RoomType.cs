namespace TourPortal.Infrastructure.Storage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RoomType
    {
        public RoomType()
        {
            Id = Guid.NewGuid().ToString();
            RoomInTypes = new HashSet<RoomInType>();
        }

        [Key]
        public string Id { get; set; }

        public string Type { get; set; }

        public virtual ICollection<RoomInType> RoomInTypes { get; set; }
    }
}