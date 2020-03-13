using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PlannerApp
{
    // Przechowuje pojedynczą jednostkę z listy
    class TodoItem
    {
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsDone { get; set; }
    }
}
