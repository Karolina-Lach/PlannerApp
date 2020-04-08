using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    /* Klasa przechowująca nazwę miasta z którego wyświetlana jest pogoda */
    public static class City
    {
        public static string nameOfTheCity { get; set; }
    }
}
