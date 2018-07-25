using Events.Data;
using Events.Web.Extensions;
using Events.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Events.Web.Controllers
{
    public class EventsController : BaseController
    {
        // GET: Events
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventInputModel m)
        {
            if(m!=null && ModelState.IsValid)
            { 
                Event e = new Event() {
                 
                   AuthorId = User.Identity.GetUserId(),
                   StartDateTime = m.StartDateTime,
                   Duration = m.Duration,
                   Description = m.Description,
                   Title = m.Title,
                   IsPublic = m.IsPublic,
                   Location = m.Location
            
                };
                db.Events.Add(e);
                db.SaveChanges();
                this.AddNotification("Event created succesfully", NotificationType.INFO);
                return RedirectToAction("My");
            }
            this.AddNotification("Event not created", NotificationType.WARNING);
            return View(m);
        }

        [Authorize]
        public ActionResult My()
        {
            String userId = User.Identity.GetUserId();
            var events = db.Events.
               Where(x => x.AuthorId == userId).
               OrderByDescending(x => x.StartDateTime).
               Select(EventViewModel.ViewModel);

            return View(
                new UpcomingPassedEvents()
                {
                    Passed = events.Where(x => x.StartDateTime <= DateTime.Now),
                    Upcoming = events.Where(x => x.StartDateTime > DateTime.Now)
                });
        }
    }
}