using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppGui
{
    class ClientWeather
    {
        private HttpClient client;
        private DialogueManager dManager;


        public ClientWeather(DialogueManager dManager)
        {
            client = new HttpClient();

            client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/forecast/daily?appid=bd5e378503939ddaee76f12ad7a97608&id=2742611&units=metric&lang=pt");
            this.dManager = dManager;
        }

        public void request(string[] args)
        {
            client.GetStringAsync("").ContinueWith((response) => handleResponse(response.Result, args));
        }

        private void handleResponse(string result, object args)
        {
            throw new NotImplementedException();
        }
    }
}
