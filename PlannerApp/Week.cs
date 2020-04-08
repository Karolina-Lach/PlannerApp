using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp
{
    class Week
    {
        DateTime _monday;
        public DateTime Monday => _monday;

        DateTime _tuesday;
        public DateTime Tuesday => _tuesday;

        DateTime _wednesday;
        public DateTime Wednesday => _wednesday;

        DateTime _thursday;
        public DateTime Thursday => _thursday;

        DateTime _friday;
        public DateTime Friday => _friday;

        DateTime _saturday;
        public DateTime Saturday => _saturday;

        DateTime _sunday;
        public DateTime Sunday => _sunday;

        public List<EventItem> Events;

        public Week(DateTime Monday)
        {
            this._monday = Monday.Date;
            this._tuesday = Monday.AddDays(1).Date;
            this._wednesday = Monday.AddDays(2).Date;
            this._thursday = Monday.AddDays(3).Date;
            this._friday = Monday.AddDays(4).Date;
            this._saturday = Monday.AddDays(5).Date;
            this._sunday = Monday.AddDays(6).Date;

            using (var db = new EventContext())
            {
                Events = db.Events.Where(b => b.Date >= this._monday && b.Date <= this._sunday).ToList();
            }
                
        }
    }
}
