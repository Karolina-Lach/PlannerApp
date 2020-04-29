using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    
    /// <summary>
    /// Klasa <c>WeatherProcessor</c>
    /// Klasa zawiera metodą łącząca się z zewnętrznym systemem API.
    /// </summary>
    public class WeatherProcessor
    {

        /// <summary>
        /// Łączy się z zewnętrznym systemem API.
        /// </summary>
        /// <param name="city">Miasto, z którego ma być pobrana pogoda. Domyślnie Wrocław.</param>
        /// <returns>Obiekt typu Root</returns>
        /// Zobacz <see cref="Root"/>
        /// <remarks>
        /// Metoda łączy się z OpenWeather.
        /// <para>
        ///     Jeśli połączenie jest udane, pobierane są dane w formacie JSON. Dane są deserializowane i zapisywane w obiekcie typu Root. Zobacz <see cref="Root"/>
        /// </para>
        /// <para>
        /// <exception>Jeśli połączenie się nie powiedzie, wyrzucany jest wyjątek z tekstem zwracanym przez uzyskaną odpowiedź</exception>
        /// </para>  
        /// </remarks>
        public static async Task<Root> LoadWeatherInfo(string city = "Wrocław")
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={ city }&appid=57029f5595ee698e81cdb8b93b77b6c1"
;

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    String json = await response.Content.ReadAsStringAsync();
                    Root weatherInfo = JsonConvert.DeserializeObject<Root>(json);

                    return weatherInfo;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
