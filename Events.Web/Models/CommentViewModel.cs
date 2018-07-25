using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Events.Web.Models
{
    public class CommentViewModel
    {

        public String Text { get; set; }
        public String Author { get; set; }
        public static Expression<Func<Events.Data.Comment, CommentViewModel>> ViewModel {

            get {
                return e => new CommentViewModel()
                {
                    Author = e.Author.FullName,
                    Text = e.Text
                };
            }
        }
    }
}

