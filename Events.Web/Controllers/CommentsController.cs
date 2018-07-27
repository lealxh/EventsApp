using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events.Web.Models;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;


namespace Events.Web.Controllers
{
    public class CommentsController : BaseController
    {
        // GET: Comments

        public PartialViewResult Index(int? Id)
        {
            List<CommentViewModel> list = new List<CommentViewModel>();
            if (Id != null)
                list = db.Comments.Where(x => x.EventId == Id.Value).Select(CommentViewModel.ViewModel).ToList();
            return PartialView(list);
        }

        public PartialViewResult Create(int? Id)
        {
            if (Id == null)
                Id = 0;

            return PartialView(Id.Value);
        }

        [HttpPost]
        public PartialViewResult SaveComment(int? Id, String CommentText)
        {
            if (Id != null)
            {
                String UserId = this.User.Identity.GetUserId();
                db.Comments.Add(new Data.Comment()
                {
                    Date = DateTime.Now,
                    Text = CommentText,
                    AuthorId = UserId,
                    EventId = Id.Value

                });
                db.SaveChanges();
            }
            List<CommentViewModel> list = new List<CommentViewModel>();
            list = db.Comments.Where(x => x.EventId == Id.Value).Select(CommentViewModel.ViewModel).ToList();

            return PartialView("Index", list);
        }


    }
}