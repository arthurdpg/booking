﻿namespace Booking.Domain.Models
{
    public class Facility : IDomainModel
    {
        public Facility(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        // Empty constructor for EF
        protected Facility() { }

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public IList<Room> Rooms { get; private set; }
    }
}
