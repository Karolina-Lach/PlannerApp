using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    /* Klasa która łączy się z zewnętrznym systemem api, pobiera informację wg formatu JSON i deserializuje dane do odpowiedniej klasy */
    public class WeatherProcessor
    {
        public static async Task<Root> LoadWeatherInfo(string city = "Wrocław")
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={ city }&appid=57029f5595ee698e81cdb8b93b77b6c1";

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
