using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Xml.Linq;
using System.IO;

namespace AppGui
{
    class ClientCanteen
    {

        private HttpClient client;
        private DialogueManager dManager;

        public ClientCanteen(DialogueManager dManager) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://services.web.ua.pt/sas/ementas?date=week&place=santiago");
            this.dManager = dManager;
        }

        public void request(string mealType, string canteen) {
            client.GetStringAsync("").ContinueWith((response) => handleResponse(response.Result,mealType,canteen));
        }


        private void handleResponse(string response, string mealType, string canteen)
        {
            XDocument document = XDocument.Load(new StringReader(response));
            List<CanteenData> meals = (from r in document.Descendants("menu")
                                       where ((string)r.Attribute("canteen")).Equals("Refeitório de Santiago") || ((string)r.Attribute("canteen")).Equals("Refeitório do Crasto")
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
                                       }).ToList<CanteenData>();


            foreach (var r in meals)
            {
                Console.WriteLine("---------------------------------------------- " + r.Canteen);
                Console.WriteLine("---------------------------------------------- " + r.Meal);
                Console.WriteLine("---------------------------------------------- " + r.Date);
                Console.WriteLine("---------------------------------------------- " + r.Weekday);
                Console.WriteLine("---------------------------------------------- " + r.WeekdayNr);
                Console.WriteLine("---------------------------------------------- " + r.Disabled);
                Console.WriteLine("---------------------------------------------- " + r.Meat);
                Console.WriteLine("---------------------------------------------- " + r.Fish);
                Console.WriteLine("---------------------------------------------- " + r.Diet);
                Console.WriteLine("---------------------------------------------- " + r.Vegetarian);
            }

        }
    }
}
