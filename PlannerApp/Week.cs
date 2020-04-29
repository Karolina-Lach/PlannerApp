using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp
{

    /// <summary>
    /// Klasa <c>Week</c>
    /// Zawiera model tygodnia wyświetlanego w kalendarzu.
    /// </summary>
    class Week
    {

        /// <summary>
        /// Przechowuje datę poniedziałku.
        /// </summary>
        DateTime _monday { get; }
        public DateTime Monday => _monday;

        /// <summary>
        /// Przechowuje datę wtorku.
        /// </summary>
        DateTime _tuesday { get; }
        public DateTime Tuesday => _tuesday;

        /// <summary>
        /// Przechowuję datę środy.
        /// </summary>
        DateTime _wednesday { get; }
        public DateTime Wednesday => _wednesday;

        /// <summary>
        /// Przechowuje datę czwartku
        /// </summary>
        DateTime _thursday { get; }
        public DateTime Thursday => _thursday;

        /// <summary>
        /// Przechowuje datę piątku.
        /// </summary>
        DateTime _friday { get; }
        public DateTime Friday => _friday;

        /// <summary>
        /// Przechowuję datę soboty.
        /// </summary>
        DateTime _saturday { get; }
        public DateTime Saturday => _saturday;

        /// <summary>
        /// Przechowuje datę niedzieli.
        /// </summary>
        DateTime _sunday { get; }
        public DateTime Sunday => _sunday;

        /// <summary>
        /// Przechowuje listę <c>EventItem</c>, których data odpowiada jednej z dat tygodnia.
        /// Zobacz <see cref="EventItem"/> po więcej szczegółów na temat <c>EventItem</c>
        /// </summary>
        public List<EventItem> Events;


        /// <summary>
        /// Konstruktor klasy <c>Week</c>
        /// </summary>
        /// <param name="Monday">Data poniedziałku</param>
        /// <remarks>
        /// Na podstawie poniedziałkowej daty generowane są daty pozostałych dni.
        /// Lista wydarzeń <c>Events</c> inicjalizowana jest z bazy danych. Zobacz <see cref="EventContext"/> po więcej szczegółów.
        /// </remarks>
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
