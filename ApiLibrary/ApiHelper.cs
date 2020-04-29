using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    /// <summary>
    /// Statyczna klasa <c>ApiHelper</c>
    /// Klasa inicjująca klienta HTTP.
    /// </summary>
    public static class ApiHelper
    {
        /// <summary>
        /// Przechowuje klienta API
        /// </summary>
        public static HttpClient ApiClient { get; set; }

        /// <summary>
        /// Inicjuje klienta Http
        /// </summary>
        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
