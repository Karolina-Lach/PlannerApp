using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    /// <summary>
    /// Statyczna klasa <c>City</c>.
    /// Przechowuje nazwę miasta, z którego wyświetlana jest pogoda.
    /// </summary>
    public static class City
    {
        public static string nameOfTheCity { get; set; }
    }
}
