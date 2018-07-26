﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Data
{
    public class Event
    {
        public Event()
        {
            this.IsPublic = true;
            this.StartDateTime = DateTime.Now;
            this.Comments = new HashSet<Comment>();
        }
        
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        public TimeSpan? Duration { get; set; }

        public String AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public String Description { get; set; }

        public bool IsPublic { get; set; }

        public String Location { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
