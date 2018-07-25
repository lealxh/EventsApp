using Events.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Events.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var events = db.Events.
                Where(x => x.IsPublic).
                OrderBy(x => x.StartDateTime).
                Select(EventViewModel.ViewModel);

              return View(
                new UpcomingPassedEvents()
                { Passed = events.Where(x => x.StartDateTime <= DateTime.Now),
                    Upcoming = events.Where(x => x.StartDateTime > DateTime.Now)
        });


        }


        public ActionResult EventDetailsById(int Id)
        {
            bool isAdmin = this.isAdmin();
            String CurrentUserId = this.User.Identity.GetUserId();

            var eventDetails = db.Events.
                Where(x=> x.Id == Id ).Select(EventDetailsViewModel.ViewModel).FirstOrDefault();

            bool isOwner = (eventDetails != null && eventDetails.AuthorId != null && eventDetails.AuthorId == CurrentUserId);

            ViewBag.CanEdit = isOwner || isAdmin;

            return PartialView("_EventDetails", eventDetails);
        }

        }
}