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
        public string AuthorId { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }

        public static Expression<Func<Events.Data.Event, EventDetailsViewModel>> ViewModel {

            get {
                return e => new EventDetailsViewModel()
                {
                    Id = e.Id,
                    AuthorId=e.AuthorId,
                    Description=e.Description,
                    Comments=e.Comments.AsQueryable().Select(CommentViewModel.ViewModel)
                
                };
            }
        }
    }
}

