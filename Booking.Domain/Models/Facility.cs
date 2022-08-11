namespace Booking.Domain.Models
{
    public class Facility
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
    }
}
