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
        private HashSet<string> invalidParks;
        private Dictionary<string,string> mapping;

        public ClientSAS(DialogueManager dManager)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://services.web.ua.pt/parques/parques");
            this.dManager = dManager;
            this.invalidParks = new HashSet<string>();
            invalidParks.Add("ZTC");
            invalidParks.Add("ESTGA");
            invalidParks.Add("ISCAA Funcionarios");// TODO ADICIONAR ESTE

            mapping = new Dictionary<string, string>();
            mapping.Add("Residencias","das Residências");
            mapping.Add("Biblioteca", "da Biblioteca");
            mapping.Add("Subterraneo", "Subterrâneo");
            mapping.Add("Ceramica", "de Cerâmica");
            mapping.Add("Linguas", "de Línguas");
            mapping.Add("Incubadora", "da Incubadora");
            mapping.Add("ISCAA Publico", "do Isca");
        }

        public void request(string[] args)
        {
            client.GetStringAsync("").ContinueWith((response) => handleResponse(response.Result, args));

        }

        private void handleResponse(string response, string[] args)
        {

            dynamic json = JsonConvert.DeserializeObject(response);

            //Console.WriteLine(json.ToString());
            switch (args[0]) {

                case "TYPE1":
                case "TYPE3":
                    dManager.manageDialogueSAS(getAllPark(json), args);
                    break;
                case "TYPE2":
                    dManager.manageDialogueSAS(getPark(json, args[2]), args);
                    break;

            }

        }

        private ParkData getPark(dynamic json,string parkName)
        {
            for (int i=1; i<json.Count;i++) {//0 is timestamp
                if (json[i].Nome.ToString().Equals(parkName)) {
                    ParkData park = new ParkData(mapping[parkName], true);
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
                if (!invalidParks.Contains(json[i].Nome.ToString())) { 
                    ParkData park = new ParkData(mapping[json[i].Nome.ToString()],true);
                    park.Capacidade = int.Parse(json[i].Capacidade.ToString());
                    park.Ocupado = int.Parse(json[i].Ocupado.ToString());
                    park.Livre = int.Parse(json[i].Livre.ToString());
                    list.Add(park);
                }
                
            }

            return list;
        }
    }
}
