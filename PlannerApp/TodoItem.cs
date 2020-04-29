using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PlannerApp
{
    
    /// <summary>
    /// Klasa <c>TodoItem</c>
    /// Zawiera model elementu listy rzeczy do zrobienia
    /// </summary>
    class TodoItem
    {

        /// <summary>
        /// Przechowuje opis elementu <c>TodoItem</c>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Przechowuje deadline elementu <c>TodoItem</c>
        /// </summary>
        public DateTime Deadline { get; set; }
        /// <summary>
        /// Przechowuje stan elementu <c>TodoItem</c>
        /// </summary>
        public bool IsDone { get; set; }
    }
}
