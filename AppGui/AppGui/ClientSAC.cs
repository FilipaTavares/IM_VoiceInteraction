﻿using AppGui.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async void request(string[] args)
        {
            try
            {
                client.Timeout = TimeSpan.FromMilliseconds(1);
                string response = await client.GetStringAsync("?format=json");

                Console.WriteLine(response);
                Console.WriteLine("COMPUTE RESPOSTA");
                handleResponse(response, args);
            }
            catch (HttpRequestException e)
            {
                if (e.InnerException is WebException)
                {
                    //dManager.manageDialogueSACExceptions("webException");
                    Console.WriteLine("WEB EXCEPTION");
                }

            }

            catch (TaskCanceledException e)
            {
                //ver tipo 5 segundos timeout e aos 3 chamar

                //dManager.manageDialogueSACExceptions("timeout");
                Console.WriteLine("CATCH TIMEOUT");
            }
        }

        private void handleResponse(string response, string[] args)
        {
            dynamic json = JsonConvert.DeserializeObject(response);

            if (!isServiceAvailable(json))
            {
                dManager.manageDialogueSAC();
            }

            else
            {

                if (args.Length == 1 && args[0].ToString().Equals("TYPE1"))
                {
                    dManager.manageDialogueSAC(getAllTicketsInfo(json));
                }

                else if (args.Length == 1 && args[0].ToString().Equals("TYPE5"))
                {
                    dManager.manageDialogueSAC(getTicket(json, "A", "licenciatura e mestrado"), args[0]);
                }

                else
                {
                    dManager.manageDialogueSAC(getTicket(json, "A", args[2]), args[0]);
                }

            }
        }

        private bool isServiceAvailable(dynamic json)
        {
            if (json.items["@attributes"].count == 0)
                return false;

            return true;
        }

        private TicketData getTicket(dynamic json, string letter, string description)
        {
            string s = json.items.item[0]["@attributes"].enabled;
            Console.WriteLine("TESTEEE");
            Console.WriteLine(letter);
            for (int i = 0; i < json.items.item.Count; i++)
            {
                if (int.Parse(json.items.item[i]["@attributes"].enabled.ToString()) == 1 && json.items.item[i].letter.ToString().Equals(letter))
                {
                    Console.WriteLine("entrou");
                    TicketData ticket = new TicketData(letter, description, true);
                    ticket.Latest = int.Parse(json.items.item[i].latest.ToString());

                    double atendingTime = Double.Parse(json.items.item[i].ast.ToString()) / 60.0;
                    double waitingTime = Double.Parse(json.items.item[i].awt.ToString()) / 60.0;

                    ticket.AverageAtendingTime = (int) Math.Round(atendingTime, MidpointRounding.AwayFromZero);
                    ticket.AverageWaitingTime = (int) Math.Round(waitingTime, MidpointRounding.AwayFromZero);
                    ticket.ClientsWaiting = int.Parse(json.items.item[i].wc.ToString());
                    return ticket;
                }
            }

            return new TicketData(letter, description, false);
        }

        private List<TicketData> getAllTicketsInfo(dynamic json)
        {
            List<TicketData> list = new List<TicketData>();

            for (int i = 0; i < json.items.item.Count; i++)
            {

                if (int.Parse(json.items.item[i]["@attributes"].enabled.ToString()) == 1)
                {
                    string letter = json.items.item[i].letter;
                    string description = cleanTicketDescriptionName(json.items.item[i].desc.ToString());
                    TicketData ticket = new TicketData(letter, description, true);

                    ticket.Latest = int.Parse(json.items.item[i].latest.ToString());
                    double averageAtendingTime = (double)int.Parse(json.items.item[i].ast.ToString()) / 60;
                    ticket.AverageAtendingTime = (int) Math.Round(averageAtendingTime, MidpointRounding.AwayFromZero);
                    double averageWaitingTime = (double)int.Parse(json.items.item[i].awt.ToString()) / 60;
                    ticket.AverageWaitingTime = (int)Math.Round(averageWaitingTime, MidpointRounding.AwayFromZero);
                    ticket.ClientsWaiting = int.Parse(json.items.item[i].wc.ToString());

                    list.Add(ticket);
                }
            }

            return list;
        }

        private string cleanTicketDescriptionName(string desc)
        {
            Console.WriteLine(desc);

            if (desc.Equals("Lic. (1º ciclo), Mestrado (2º ciclo)"))
                return "de licenciatura e mestrado";

            else if (desc.Equals("Atendimento Prioritário"))
                return "de atendimento prioritário";

            else if (desc.Equals("Doutoramentos, Agregações"))
                return "de doutoramentos e agregações";

            else if (desc.Equals("Exchange / Intercâmbio (Incoming)"))
                return "de intercâmbio";

            else if (desc.Equals("Estágios Internacionais"))
                return "de estágios internacionais";

            else if (desc.Equals("Mobilidade Erasmus (Alunos UA)"))
                return "de erasmus";

            else
            {
                return "de inserção profissional";
            }

        }
    }
}