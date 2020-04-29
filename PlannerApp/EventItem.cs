using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp
{

    /// <summary>
    /// Klasa <c>EventItem</c>.
    /// Zawiera model wydarzenia w kalendarzu.
    /// </summary>
    public class EventItem
    {

        /// <summary>
        /// Przchowuje ID wydarzenia.
        /// </summary>
        public int EventItemId { get; set; }

        /// <summary>
        /// Przechowuje datę wydarzenia.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Przechowuje godzinę rozpoczęcia wydarzenia.
        /// </summary>
        public int Beginning { get; set; }

        /// <summary>
        /// Przechowuje godzinę końca wydarzenia.
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// Przchowuje nazwę wydarzenia.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Konstruktor klasy <c>EventItem</c>
        /// </summary>
        /// <param name="Date">Data wydarzenia</param>
        /// <param name="Beginning">Rozpoczęcie wydarzenia</param>
        /// <param name="End">Koniec wydarzenia</param>
        /// <param name="Name">Nazwa wydarzenia</param>
        public EventItem(DateTime Date, int Beginning, int End, string Name)
        {
            this.Date = Date.Date;
            this.Beginning = Beginning;
            this.End = End;
            this.Name = Name;
        }

        /// <summary>
        /// Konstruktor klasy <c>EventItem</c>
        /// </summary>
        public EventItem()
        {

        }
    }


    /// <summary>
    /// Klasa <c>EventContext</c>, która dodaje <c>EventItem</c> do bazy danych w technologii EntityFramework.
    /// </summary>
    public class EventContext : DbContext
    {

        /// <summary>
        /// Lista wydarzeń dodawanych do bazy danych
        /// </summary>
        public DbSet<EventItem> Events { get; set; }
    }
}
