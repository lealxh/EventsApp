using Events.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Events.Web.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class HomeController : BaseController
    {
        public const int DefaultPageSize = 3;
        public ActionResult Index()
        {

            int PageSize = HomeController.DefaultPageSize;
            int? page = 1;
            var EventsPassed = new PagedData<EventViewModel>();

            EventsPassed.Data = db.Events.Where(x => x.IsPublic && x.StartDateTime <= DateTime.Now).OrderByDescending(x => x.StartDateTime).Skip(PageSize * (page.Value - 1)).Take(PageSize).Select(EventViewModel.ViewModel).ToList();
            EventsPassed.Pages = Convert.ToInt32(Math.Ceiling((double)db.Events.Count(x => x.IsPublic && x.StartDateTime <= DateTime.Now) / PageSize));
            EventsPassed.CurrentPage = page.Value;

            var EventsUpcoming = new PagedData<EventViewModel>();

            EventsUpcoming.Data = db.Events.Where(x => x.IsPublic && x.StartDateTime > DateTime.Now).OrderByDescending(x => x.StartDateTime).Skip(PageSize * (page.Value - 1)).Take(PageSize).Select(EventViewModel.ViewModel).ToList();
            EventsUpcoming.Pages = Convert.ToInt32(Math.Ceiling((double)db.Events.Count(x => x.IsPublic && x.StartDateTime > DateTime.Now) / PageSize));
            EventsUpcoming.CurrentPage = page.Value;
            ViewBag.Controller = "Home";
            return View(new UpcomingPassedEventsPaged() { Passed = EventsPassed, Upcoming = EventsUpcoming });


        }

        public ActionResult PassedEvents(int? page)
        {
            page = page ?? 1;

            int PageSize = HomeController.DefaultPageSize;
            var EventsPassed = new PagedData<EventViewModel>();
            
            EventsPassed.Data = db.Events.Where(x => x.IsPublic && x.StartDateTime <= DateTime.Now).OrderByDescending(x => x.StartDateTime).Skip(PageSize * (page.Value - 1)).Take(PageSize).Select(EventViewModel.ViewModel).ToList();
            EventsPassed.Pages = Convert.ToInt32(Math.Ceiling((double)db.Events.Count(x => x.IsPublic && x.StartDateTime <= DateTime.Now) / PageSize));
            EventsPassed.CurrentPage = page.Value;

            ViewBag.Controller = "Home";

            return PartialView("_EventsPassed", EventsPassed);
          
        }

        public ActionResult UpcomingEvents(int? page)
        {
            page = page ?? 1;

            int PageSize = HomeController.DefaultPageSize;
            var EventsUpcoming = new PagedData<EventViewModel>();

            EventsUpcoming.Data = db.Events.Where(x => x.IsPublic && x.StartDateTime > DateTime.Now).OrderByDescending(x => x.StartDateTime).Skip(PageSize * (page.Value - 1)).Take(PageSize).Select(EventViewModel.ViewModel).ToList();
            EventsUpcoming.Pages = Convert.ToInt32(Math.Ceiling((double)db.Events.Count(x => x.IsPublic && x.StartDateTime > DateTime.Now) / PageSize));
            EventsUpcoming.CurrentPage = page.Value;
            ViewBag.Controller= "Home";

            return PartialView("_EventsUpcoming", EventsUpcoming);

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