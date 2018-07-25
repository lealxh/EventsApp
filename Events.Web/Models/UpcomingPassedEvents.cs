using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Web.Models
{
    public class UpcomingPassedEvents
    {
       public IEnumerable<EventViewModel> Upcoming { get; set; }
       public IEnumerable<EventViewModel> Passed { get; set; }
    }
}