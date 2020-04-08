using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp
{
    public class EventItem
    {
        public int EventItemId { get; set; }
        public DateTime Date { get; set; }
        public int Beginning { get; set; }
        public int End { get; set; }

        public string Name { get; set; }

        public EventItem(DateTime Date, int Beginning, int End, string Name)
        {
            this.Date = Date.Date;
            this.Beginning = Beginning;
            this.End = End;
            this.Name = Name;
        }

        public EventItem()
        {

        }
    }

    public class EventContext : DbContext
    {
        public DbSet<EventItem> Events { get; set; }
    }
}
