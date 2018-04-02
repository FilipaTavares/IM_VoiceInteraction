﻿using AppGui.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppGui
{
    class ClientSAC
    {
        private HttpClient client;
        private DialogueManager dManager;


        public ClientSAC(DialogueManager dManager)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://services.web.ua.pt/sac/senhas");
            this.dManager = dManager;
        }

        public void request(string[] args)
        {
            string queryParams = "?format=json";

            if (args.Length == 1 && args[0].ToString().Equals("TYPE1"))
                client.GetStringAsync(queryParams).ContinueWith((response) => handleResponse(response.Result, args));

            else
            {
                queryParams += "&letter=" + args[1];
                client.GetStringAsync(queryParams).ContinueWith((response) => handleSingleResponse(response.Result, args));

            }
        }

        private void handleSingleResponse(string response, string[] args)
        {
            dynamic json = JsonConvert.DeserializeObject(response);

            Console.WriteLine(json.ToString());

            dManager.manageDialogueSAC(getTicket(json, args[1]), args);
        }

        private void handleResponse(string response, string[] args)
        {

            dynamic json = JsonConvert.DeserializeObject(response);

            Console.WriteLine(json.ToString());

            // TODO 
            dManager.manageDialogueSAC(getAllTicketsInfo(json), args);

        }

        private TicketData getTicket(dynamic json, string letter)
        {
            return new TicketData(letter, false);
        }

        private List<TicketData> getAllTicketsInfo(dynamic json)
        {
            List<TicketData> list = new List<TicketData>();
            return list;
        }
    }
}