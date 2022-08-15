using System.ComponentModel;

namespace Booking.Domain.Enums
{
    public enum RoomType
    {
        [Description("Standard room - twin bed")]
        StandardTwinBed = 1,
        [Description("Standard room - double bed")]
        StandardDoubleBed
    }
}
