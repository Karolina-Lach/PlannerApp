using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    /// <summary>
    /// Klasa <c>Main</c>
    /// Zawiera ogólne infomrajce dotyczącego pogody.
    /// Klasa jest częścią modelu pobieranego z systemu API.
    /// Zobacz <see cref="Root"/>
    /// </summary>
    public class Main
    {

        /// <summary>
        /// Przechowuje wartość temperatury. Domyślnie wyrażona w Kelvinach.
        /// </summary>
        public double temp { get; set; }

        /// <summary>
        /// Przechowuję wartość ciśnienia powietrza w hPa.
        /// </summary>
        public int pressure { get; set; }

        /// <summary>
        /// Przechowuje wartość wilgotności powietrza w %.
        /// </summary>
        public int humidity { get; set; }

        /// <summary>
        /// Przechowuje wartość minimalnej możliwej temperatury w danym momencie. 
        /// </summary>
        public double temp_min { get; set; }

        /// <summary>
        /// Przechowuje wartość maksymalnej możliwej temperatury w danym momencie.
        /// </summary>
        public double temp_max { get; set; }
    }
}
