using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Configuration
{
    public class RulesConfig
    {
        public int MaximumStayDuration { get; private set; }
        public int MaximumDaysInAdvance { get; private set; }
    }
}
