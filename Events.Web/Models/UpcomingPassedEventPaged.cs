using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Web.Models
{
    public class UpcomingPassedEventsPaged
    {
       public PagedData<EventViewModel> Upcoming { get; set; }
       public PagedData<EventViewModel> Passed { get; set; }
    }
}