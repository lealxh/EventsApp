using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Events.Web.Models
{
    public class EventViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDateTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public String Author { get; set; }
        public String Location { get; set; }

        public static Expression<Func<Events.Data.Event, EventViewModel>> ViewModel {

            get {
                return e => new EventViewModel()
                {
                    Author = e.Author.FullName,
                    Duration = e.Duration,
                    Id = e.Id,
                    Location = e.Location,
                    StartDateTime = e.StartDateTime,
                    Title = e.Title

                };
            }
        }
    }
}

