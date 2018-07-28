using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Events.Web.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public String Text { get; set; }
        public String Author { get; set; }
        public String AuthorId { get; set; }
        public static Expression<Func<Events.Data.Comment, CommentViewModel>> ViewModel {

            get {
                return e => new CommentViewModel()
                {
                    Id=e.Id,
                    Author = e.Author.FullName,
                    AuthorId=e.AuthorId,
                    Text = e.Text
                };
            }
        }
    }
}

