using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{

    /// <summary>
    /// Klasa <c>Wind</c>
    /// Zawiera dane dotyczące wiatru.
    /// Klasa jest częścią modelu pobieranego z systemu API.
    /// Zobacz <see cref="Root"/>
    /// </summary>
    public class Wind
    {

        /// <summary>
        /// Przechowuje informacje dotyczącą prędkości wiatru.
        /// </summary>
        public double speed { get; set; }

        /// <summary>
        /// Przechowuje informacje dotyczącą kierunku wiatru.
        /// </summary>
        public int deg { get; set; }
    }
}
