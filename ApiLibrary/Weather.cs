using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    /// <summary>
    /// Klasa <c>Weather</c>
    /// Zawiera ogólne dane dotyczące pogody.
    /// Klasa jest częścią modelu pobieranego z systemu API.
    /// Zobacz <see cref="Root"/>
    /// </summary>
    public class Weather
    {

        /// <summary>
        /// Id stanu pogody
        /// </summary>
        public int id { get; set; }


        /// <summary>
        /// Grupa parametrów pogodowych np. deszcz, chmury itp.
        /// </summary>
        public string main { get; set; }


        /// <summary>
        /// Opis warunków pogodowych
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Kod ikony, która odpowiada warunkom pogodowym.
        /// </summary>
        public string icon { get; set; }
    }
}
