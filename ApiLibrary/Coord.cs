using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    /// <summary>
    /// Klasa <c>Coord</c>
    /// Zawiera informacje o współrzędnych miasta.
    /// Klasa jest częścią modelu pobieranego z systemu API.
    /// Zobacz <see cref="Root"/>
    /// </summary>
    public class Coord
    {
        /// <summary>
        ///  Wartość długości geograficznej.
        /// </summary>
        public double lon { get; set; }
        /// <summary>
        /// Wartość szerokości geograficznej.
        /// </summary>
        public double lat { get; set; }
    }
}
