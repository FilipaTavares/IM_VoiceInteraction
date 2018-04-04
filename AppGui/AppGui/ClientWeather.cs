using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppGui.Data;
using Newtonsoft.Json;

namespace AppGui
{
    class ClientWeather
    {
        private HttpClient client;
        private DialogueManager dManager;

        public ClientWeather(DialogueManager dManager)
        {
            client = new HttpClient();

            client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/forecast/daily?appid=bd5e378503939ddaee76f12ad7a97608&id=2742611&units=metric&lang=pt&cnt=17");
            this.dManager = dManager;
        }

        public void request(string[] args)
        {   
            client.GetStringAsync("").ContinueWith((response) => handleResponse(response.Result, args));
        }

        private void handleResponse(string response, string[] args)
        {
            dynamic json = JsonConvert.DeserializeObject(response);

            if (args[0].ToString().Equals("TYPE1"))
            {
                if (args[1].ToString().Equals("today"))
                    getDay(json, 0, "hoje", DateTime.Today);

                else if (args[1].ToString().Equals("tomorrow"))
                    getDay(json, 1, "amanhã", DateTime.Today.AddDays(1));

                else if (args[1].ToString().Equals("dayOfWeek"))
                {
                    DateTime today = DateTime.Today;
                    int daysToAdd = getWeekday(today, int.Parse(args[3].ToString()));
                    getDay(json, 1, args[2].ToString(), today.AddDays(daysToAdd));
                }

                else
                {
                    int day = int.Parse(args[2].ToString());
                    DateTime today = DateTime.Today;
                    int month = today.Month;
                    int year = today.Year;

                    if (day > today.Day && month != 12)
                        month += 1;

                    else if (day > today.Day && month != 12)
                    {
                        month = 1;
                        year += 1;
                    }

                    DateTime date;

                    if (DateTime.TryParse(month + "/" + month + "/" + year, out date))
                        Console.WriteLine("yay");

                    else
                        Console.WriteLine("no");

                    Console.WriteLine(date);
                    Console.WriteLine(today);

                }
            }
        }

        private WeatherData getDay(dynamic json, int index, string dayDescription, DateTime date)
        {
            WeatherData weather = new WeatherData();
            weather.DayDescription = dayDescription;
            weather.Date = date;
            weather.Description = json.list[index].weather[0].description;
            double minTemp = json.list[index].temp.min;
            double maxTemp = json.list[index].temp.max;
            double windSpeed = json.list[index].speed * 3.6; // m/s para km/h
            weather.MinTemp = (int)Math.Round(minTemp, MidpointRounding.AwayFromZero);
            weather.MaxTemp = (int)Math.Round(maxTemp, MidpointRounding.AwayFromZero);
            weather.Humidity = json.list[index].humidity;
            weather.WindSpeed = (int)Math.Round(windSpeed, MidpointRounding.AwayFromZero);
            Console.WriteLine(weather.ToString());
            return weather;
        }

        public static int getWeekday(DateTime start, int day)
        {
            return ((day - (int)start.DayOfWeek + 7) % 7) + 1; // [1, 7]
        }

    }
}