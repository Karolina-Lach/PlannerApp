using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    /// <summary>
    /// Klasa <c>Sys</c>
    /// Zawiera informacje systemowe pobieranych danych.
    /// Klasa jest częścią modelu pobieranego z systemu API.
    /// Zobacz <see cref="Root"/>
    /// </summary>
    public class Sys
    {

        /// <summary>
        /// Wewnętrzny parametr.
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// Wewnętrzny parametr.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Wewnętrzny parametr.
        /// </summary>
        public double message { get; set; }

        /// <summary>
        /// Kod kraju.
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// Czas wschodu słońca.
        /// </summary>
        public int sunrise { get; set; }

        /// <summary>
        /// Czas zachodu słońca.
        /// </summary>
        public int sunset { get; set; }
    }
}
