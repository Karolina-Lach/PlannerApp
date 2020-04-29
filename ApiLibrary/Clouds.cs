using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    /// <summary>
    /// Klasa <c>Clouds</c>
    /// Zawiera informacje o chmurach.
    /// Klasa jest częścią modelu pobieranego z systemu API.
    /// Zobacz <see cref="Root"/>
    /// </summary>
    public class Clouds
    {
        /// <summary>
        /// Przechowuje wartość zachmrzenia w %
        /// </summary>
        public int all { get; set; }
    }
}
