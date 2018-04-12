using System;
using System.Collections.Generic;
using System.Globalization;
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

            WeatherData weather = null;

            if (args[1].ToString().Equals("today"))
                weather = GetWeather(json, 0, "hoje", DateTime.Today);

            else if (args[1].ToString().Equals("tomorrow"))
                weather = GetWeather(json, 1, "amanhã", DateTime.Today.AddDays(1));

            else if (args[1].ToString().Equals("dayOfWeek"))
            {
                DateTime today = DateTime.Today;
                int daysToAdd = getNextWeekday(today, int.Parse(args[3].ToString()));
                weather = GetWeather(json, daysToAdd, args[2].ToString(), today.AddDays(daysToAdd));
            }

            else if (args[1].ToString().Equals("numberOfDay"))
            {
                int day = int.Parse(args[2].ToString());
                DateTime today = DateTime.Today;
                int month = today.Month;
                int year = today.Year;

                if (day > today.Day && day > DateTime.DaysInMonth(year, month) && month != 12)
                    month += 1;

                else if (day > today.Day && day > DateTime.DaysInMonth(year, month) && month == 12)
                {
                    month = 1;
                    year += 1;
                }

                Console.WriteLine(month + "/" + day + "/" + year);

                DateTime date;

                if (DateTime.TryParse(day + "-" + month + "-" + year, out date))
                {
                    TimeSpan diff1 = date.Subtract(today);

                    if (diff1.Days > 16)
                    {
                        dManager.manageDialogueWeather(date, "out of range");
                        return;
                    }

                    else
                    {
                        weather = GetWeather(json, diff1.Days, "no dia " + day, date);
                    }

                }

                else
                {
                    Console.WriteLine("NAO FEZ PARSE");
                    dManager.manageDialogueWeather(weather.Date, "invalid");
                    return;
                }
            }
            Console.WriteLine("dataaaaaaaaaaaa " + weather);
            dManager.manageDialogueWeather(weather, args[0].ToString());

        }

        private WeatherData GetWeather(dynamic json, int index, string dayDescription, DateTime date)
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

        public static int getNextWeekday(DateTime start, int day)
        {
            return (day - (int)DateTime.Today.DayOfWeek + 7) % 7;
        }
    }
}