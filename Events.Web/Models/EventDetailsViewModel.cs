using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Events.Web.Models
{
    public class EventDetailsViewModel
    {

        public int Id { get; set; }

        public string Description { get; set; }
       
        public TimeSpan? Duration { get; set; }

        public String Author { get; set; }

        public String AuthorId { get; set; }

        public String Location { get; set; }


        public static Expression<Func<Events.Data.Event, EventDetailsViewModel>> ViewModel {

            get {
                return e => new EventDetailsViewModel()
                {
                    Id = e.Id,
                    Author=e.Author.FullName,
                    AuthorId=e.Author.Id,
                    Description=e.Description,
                    Location=e.Location,
                    Duration=e.Duration
                
                };
            }
        }
    }
}

