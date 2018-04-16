using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Xml.Linq;
using System.IO;
using System.Globalization;

namespace AppGui
{
    class ClientCanteen
    {

        private HttpClient client;
        private DialogueManager dManager;
        private CultureInfo culture;

        public ClientCanteen(DialogueManager dManager)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://services.web.ua.pt/sas/ementas?date=week&place=santiago");
            this.dManager = dManager;
            this.culture = new CultureInfo("en-US");
        }

        public void request(string[] args)
        {
            client.GetStringAsync("").ContinueWith((response) => handleResponse(response.Result, args));
        }

        private void handleResponse(string response, string[] args)
        {
            foreach (var item in args)
            {
                Console.WriteLine(item.ToString());
            }

            XDocument document = XDocument.Load(new StringReader(response));

            if (args[0].Equals("TYPE2"))
            {
                string canteen = args[2].Equals("Crasto") ? "Refeitório do Crasto" : "Refeitório de Santiago";
                string meal = args[1].ToString();
                DateTime date = DateTime.Today;
                string dayDescription = "";

                if (args[3].ToString().Equals("SUBTYPE1") || args[4].ToString().Equals("today"))
                {
                    date = DateTime.Today;
                    dayDescription = "hoje";
                }

                else if (args[4].ToString().Equals("tomorrow"))
                {
                    date = DateTime.Today.AddDays(1);
                    dayDescription = "amanhã";
                }

                else if (args[4].ToString().Equals("dayOfWeek"))
                {
                    DateTime today = DateTime.Today;
                    int daysToAdd = getNextWeekday(today, int.Parse(args[6].ToString()));
                    date = today.AddDays(daysToAdd);
                    dayDescription = args[5].ToString(); // ver se é o indice 5
                }

                else if (args[4].ToString().Equals("numberOfDay"))
                {
                    int day = int.Parse(args[5].ToString());

                    DateTime today = DateTime.Today;
                    int month = today.Month;
                    int year = today.Year;

                    if (day > today.Day && day > DateTime.DaysInMonth(year, month) && month != 12)
                        month += 1;

                    else if (day < today.Day && month == 12)
                    {
                        month = 1;
                        year += 1;
                    }

                    else if (day < today.Day && month != 12)
                    {
                        month += 1;
                    }

                    Console.WriteLine(month + "/" + day + "/" + year);

                    bool parsed = DateTime.TryParse(day + "-" + month + "-" + year, out date);

                    if (!parsed)
                    {
                        dManager.manageDialogueCanteenInvalidDate(day, month);
                        return;
                    }

                    else
                    {
                        dayDescription = "no dia " + day;
                    }
                }

                string format = "ddd, dd MMM yyyy";   // Use this format.
                Console.WriteLine(date.ToString(format, culture)); // Write to console.

                var meals = (from r in document.Descendants("menu").Where
                                  (r => r.Attribute("canteen").Value.Equals(canteen)).Where
                                  (r => r.Attribute("meal").Value.Equals(meal)).Where
                                  (r => int.Parse(r.Attribute("weekdayNr").Value) == (int)date.DayOfWeek).Where
                                  (r => r.Attribute("date").Value.Contains(date.ToString(format, culture)))
                                  from d in r.Elements("items")
                                         //where !d.IsEmpty // elimina cantinas fechadas - vê se items = <items /> era fixe mas vou tirar
                                         // pq se estiver fechado é diferente do que nao dar para ver a data para um dia longe

                                select new CanteenData
                                {
                                    Canteen = r.Attribute("canteen").Value,
                                    Meal = r.Attribute("meal").Value,
                                    Date = r.Attribute("date").Value,
                                    Weekday = r.Attribute("weekday").Value,
                                    WeekdayNr = int.Parse(r.Attribute("weekdayNr").Value),
                                    Disabled = r.Attribute("disabled").Value,
                                    Meat = (d.IsEmpty || d.Descendants("item").ElementAt(1).IsEmpty) ? "0" : d.Descendants("item").ElementAt(1).Value,
                                    Fish = (d.IsEmpty || d.Descendants("item").ElementAt(2).IsEmpty) ? "0" : d.Descendants("item").ElementAt(2).Value,
                                    Diet = (d.IsEmpty || d.Descendants("item").ElementAt(3).IsEmpty) ? "0" : d.Descendants("item").ElementAt(3).Value,
                                    Vegetarian = (d.IsEmpty || d.Descendants("item").ElementAt(4).IsEmpty) ? "0" : d.Descendants("item").ElementAt(4).Value,
                                    Option = (d.IsEmpty || d.Descendants("item").ElementAt(5).IsEmpty) ? "0" : d.Descendants("item").ElementAt(5).Value
                                }).FirstOrDefault();


                /**
                var data =  from r in document.Descendants("menu")
                            where r.Attribute("canteen").Value.Equals(canteen)
                            where r.Attribute("meal").Value.Equals(meal)
                            where int.Parse(r.Attribute("weekdayNr").Value) == (int)date.DayOfWeek
                            where r.Attribute("date").Value.Contains(date.ToString(format, culture))
                            from d in r.Elements("items")
                            where !d.IsEmpty // elimina cantinas fechadas - vê se items = <items />
                            select new CanteenData
                            {
                                // Atenção usar (string) e nao toString senao vêm as ""
                                Canteen = (string)r.Attribute("canteen"),
                                Meal = (string)r.Attribute("meal"),
                                Date = (string)r.Attribute("date"),
                                Weekday = (string)r.Attribute("weekday"),
                                WeekdayNr = int.Parse((string)r.Attribute("weekdayNr")),
                                Disabled = (string)r.Attribute("disabled"),
                                Meat = d.Descendants("item").ElementAt(1).IsEmpty ? "0" : (string)d.Descendants("item").ElementAt(1).Value,
                                Fish = d.Descendants("item").ElementAt(2).IsEmpty ? "0" : (string)d.Descendants("item").ElementAt(2).Value,
                                Diet = d.Descendants("item").ElementAt(3).IsEmpty ? "0" : (string)d.Descendants("item").ElementAt(3).Value,
                                Vegetarian = d.Descendants("item").ElementAt(4).IsEmpty ? "0" : (string)d.Descendants("item").ElementAt(4).Value
                            };
    */

                // verificar quando nao existe o defeito -- dia fora da previsao
                meals.DayDescription = dayDescription;
                dManager.manageDialogueCanteen(meals);

                Console.WriteLine("---------------------------------------------- " + meals.Canteen);
                Console.WriteLine("---------------------------------------------- " + meals.Meal);
                Console.WriteLine("---------------------------------------------- " + meals.Date);
                Console.WriteLine("---------------------------------------------- " + meals.Weekday);
                Console.WriteLine("---------------------------------------------- " + meals.WeekdayNr);
                Console.WriteLine("---------------------------------------------- " + meals.Disabled);
                Console.WriteLine("---------------------------------------------- " + meals.Meat);
                Console.WriteLine("---------------------------------------------- " + meals.Fish);
                Console.WriteLine("---------------------------------------------- " + meals.Diet);
                Console.WriteLine("---------------------------------------------- " + meals.Vegetarian);
            }
        }

        private int getNextWeekday(DateTime start, int day)
        {
            return (day - (int)DateTime.Today.DayOfWeek + 7) % 7;
        }

    }
}
