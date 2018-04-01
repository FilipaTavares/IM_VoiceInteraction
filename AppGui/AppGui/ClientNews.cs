using AppGui.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppGui
{
    class ClientNews
    {

        private HttpClient client;
        private DialogueManager dManager;

        public ClientNews(DialogueManager dManager)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://services.sapo.pt/UA/Online/contents_xml");
            this.dManager = dManager;
        }

        public void request() {
            string queryParams = "?jsonText=true";
            //because of utf-8
            client.GetByteArrayAsync(queryParams).ContinueWith((response) => handle(Encoding.UTF8.GetString(response.Result)));
            //client.GetStringAsync(queryParams).ContinueWith((response) => handle(response.Result));
        }

        private void handle(string response)
        {
            Console.WriteLine("Handle");
            dynamic json = JsonConvert.DeserializeObject(response);

            List<NewsData> newsList = new List<NewsData>();
            foreach (dynamic item in json.rss.channel.item) {
                NewsData news = new NewsData();
                news.Title = item.title;
                news.Description = item.description;
                news.Date = item.pubDate;

                newsList.Add(news);
            }

            dManager.manageDialogueNews(newsList);
        }
    }
}
