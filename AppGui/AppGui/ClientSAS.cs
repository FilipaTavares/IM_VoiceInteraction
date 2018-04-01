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
    class ClientSAS
    {
        private HttpClient client;
        private DialogueManager dManager;
        

        public ClientSAS(DialogueManager dManager)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://services.web.ua.pt/parques/parques");
            this.dManager = dManager;
        }

        public void request(string[] args)
        {
            client.GetStringAsync("").ContinueWith((response) => handleResponse(response.Result, args));
        }

        private void handleResponse(string response, string[] args)
        {

            dynamic json = JsonConvert.DeserializeObject(response);

            Console.WriteLine(json.ToString());

            if (args.Length == 1 && args[0].ToString().Equals("TYPE1"))
            {//get all parks

                dManager.manageDialogueSAS(getAllPark(json), args);
            }
            else {//get only one

                dManager.manageDialogueSAS(getPark(json, args[1]), args);
            }

        }

        private ParkData getPark(dynamic json,string parkName)
        {
            for (int i=1; i<json.Count;i++) {//0 is timestamp
                if (json[i].Nome.ToString().Equals(parkName)) {
                    ParkData park = new ParkData(parkName,true);
                    park.Capacidade = int.Parse(json[i].Capacidade.ToString());
                    park.Ocupado = int.Parse(json[i].Ocupado.ToString());
                    park.Livre = int.Parse(json[i].Livre.ToString());
                    return park;
                }
            }

            return new ParkData(parkName,false);
        }

        private List<ParkData> getAllPark(dynamic json)
        {
            List<ParkData> list = new List<ParkData>();
            for (int i = 1; i < json.Count; i++)
            {//0 is timestamp

                ParkData park = new ParkData(json[i].Nome.ToString(),true);
                park.Capacidade = int.Parse(json[i].Capacidade.ToString());
                park.Ocupado = int.Parse(json[i].Ocupado.ToString());
                park.Livre = int.Parse(json[i].Livre.ToString());
                list.Add(park);
                
            }

            return list;
        }
    }
}
