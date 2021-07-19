﻿namespace TourPortal.Infrastructure.Storage.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Room
    {
        public Room()
        {
            Id = Guid.NewGuid().ToString();
            RoomInTypes = new HashSet<RoomInType>();
        }

        [Key]
        public string Id { get; set; }

        public string HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }

        public int NumberOfBeds { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<RoomInType> RoomInTypes { get; set; }
    }
}