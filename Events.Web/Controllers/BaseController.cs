using Events.Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Events.Web.Controllers
{
    public class BaseController:Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        public bool isAdmin()
        {
            String CurrentUserId = this.User.Identity.GetUserId();
            return (CurrentUserId != null && (User.IsInRole("Administrator")));
        }
    }
}