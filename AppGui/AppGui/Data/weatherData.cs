using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGui.Data
{
    class WeatherData
    {

        private int minTemp;
        private int maxTemp;
        private int windSpeed;
        private int humidity;
        private string description;

        private string dayDescription;   // hoje, amanhã, na segunda, dia 23
        private DateTime date;

        public int MinTemp { get => minTemp; set => minTemp = value; }
        public int MaxTemp { get => maxTemp; set => maxTemp = value; }
        public int WindSpeed { get => windSpeed; set => windSpeed = value; }
        public int Humidity { get => humidity; set => humidity = value; }
        public string Description { get => description; set => description = value; }
        public string DayDescription { get => dayDescription; set => dayDescription = value; }
        public DateTime Date { get => date; set => date = value; }

        public override string ToString()
        {
            return minTemp + " " + maxTemp + " " + windSpeed + " " + humidity + " " + description + " " + dayDescription
                + " " + Date.ToString();
        }

    }
}
