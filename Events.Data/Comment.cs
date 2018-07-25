using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Data
{
    public class Comment
    {
        public Comment()
        {
            this.Date = DateTime.Now;
        }
        public int Id { get; set; }

        public String Text { get; set; }

        public DateTime Date { get; set; }

        public int AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }


    }
}
