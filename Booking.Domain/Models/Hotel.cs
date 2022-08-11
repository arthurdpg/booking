﻿namespace Booking.Domain.Models
{
    public class Hotel
    {
        public Hotel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IList<Room>? Rooms { get; private set; }
    }
}
