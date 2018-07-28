using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Web.Models
{
    public class PagedData<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}