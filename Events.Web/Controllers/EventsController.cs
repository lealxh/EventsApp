﻿using Events.Data;
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
        Event LoadEvent(int Id)
        {
            String UserId = User.Identity.GetUserId();
            bool isAdmin = this.isAdmin();
            var eventToEdit = db.Events.Where(x => x.Id == Id).
                FirstOrDefault(x => x.AuthorId == UserId || isAdmin);

             return eventToEdit;
        }

      

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int Id, EventInputModel m)
        {
            Event e = LoadEvent(Id);
            if (e == null)
            {
                this.AddNotification("Cannot edit event # " + Id, NotificationType.ERROR);
                return RedirectToAction("My");

            }
            if (m != null && ModelState.IsValid)
            {
                e.Description = m.Description;
                e.Duration = m.Duration;
                e.IsPublic = m.IsPublic;
                e.Location = m.Location;
                e.StartDateTime = m.StartDateTime;
                e.Title = m.Title;
                db.SaveChanges();

                
                this.AddNotification("Event edited successfully", NotificationType.INFO);
                return RedirectToAction("My");

            }
          
            return View(m);
        }

        [Authorize]
        public ActionResult Edit(int? Id)
        {

            if (Id == null)
            {
                this.AddNotification("Cannot edit event # " + Id, NotificationType.ERROR);
                return RedirectToAction("My");
            }

            Event e = LoadEvent(Id.Value);
            if (e == null)
            {
                this.AddNotification("Cannot edit event # "+Id, NotificationType.ERROR);
                return RedirectToAction("My");
                
            }
         
            var model = EventInputModel.CreateFromEvent(e);
            return View(model);
        }


        [Authorize]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                this.AddNotification("Cannot delete event # " + Id, NotificationType.ERROR);
                return RedirectToAction("My");
            }
            Event e = LoadEvent(Id.Value);
            if (e == null)
            {
               this.AddNotification("Cannot delete event # " + Id, NotificationType.ERROR);
               return RedirectToAction("My");

            }



            var model = EventViewModel.CreateFromEvent(e);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(int Id,EventInputModel v)
        {
            Event e = LoadEvent(Id);
            if (e == null)
            {
                this.AddNotification("Cannot edit event # " + Id, NotificationType.ERROR);
                return RedirectToAction("My");

            }
            else
             {

                db.Events.Remove(e);
                db.SaveChanges();


                this.AddNotification("Event deleted successfully", NotificationType.INFO);
                return RedirectToAction("My");

            }

           
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