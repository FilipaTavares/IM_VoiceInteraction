using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppGui.Data;

namespace AppGui
{
    class Answers
    {
        private Random random;

        public Answers() { random = new Random(); }

        private string[] canteenDisable = new string[] {
            "Parece que a cantina do <NOME_CANTINA> está encerrada",
            "Lamento informar mas a cantina está encerrada",
            "Infelizmente a cantina do <NOME_CANTINA> está encerrada"
        };

        private string[] parkNotFound = new string[] {
            "Lamento informar mas não encontrei nenhum parque de estacionamento com o nome <NOME_PARQUE_ESTACIONAMENTO>",
            "Infelizmente não localizei o parque de estacionamento <NOME_PARQUE_ESTACIONAMENTO>"
        };

        private string[] parkIsFree = new string[] {
            "Sim,",
            "Sim, o parque de estacionamento <NOME_PARQUE_ESTACIONAMENTO> tem <NUM_LIVRES> lugares livres",
            "Afirmativo, o parque de estacionamento <NOME_PARQUE_ESTACIONAMENTO> ainda tem <NUM_LIVRES> lugares livres",
        };

        private string[] parkIsNotFree = new string[] {
            "Não",
            "Não, o parque de estacionamento <NOME_PARQUE_ESTACIONAMENTO> está completamente ocupado",
            "Estás com azar, o parque de estacionamento <NOME_PARQUE_ESTACIONAMENTO> está completamente ocupado",
            "Que infortunio, parece que o parque de estacionamento <NOME_PARQUE_ESTACIONAMENTO> está completamente ocupado"
        };

        private string[] parkFreeSpots = new string[] {
            "O parque de estacionamento <NOME_PARQUE_ESTACIONAMENTO> tem <NUM_LIVRES> lugares livres",
            "Existem <NUM_LIVRES> lugares livres no parque de estacionamento <NOME_PARQUE_ESTACIONAMENTO>"
        };

        private string[] parkServiceUnavailable = new string[] {
            "Estranho não consegui encontrar nenhum parque de estacionamento",
            "O serviço de parque de estacionamento não parece estar a funcionar"
        };
    
        private string[] allParksFreeSTART = new string[] {
            "Ok, encontrei os seguintes parques de estacionamento:",
            "Os seguintes parques de estacionamento estão livres:"
        };

        private string[] allParksFree = new string[] {
            "O parque de estacionamento <NOME_PARQUE_ESTACIONAMENTO> tem <NUM_LIVRES> lugares livres",
            "O parque <NOME_PARQUE_ESTACIONAMENTO> tem <NUM_LIVRES> lugares disponiveis",
            "O estacionamento <NOME_PARQUE_ESTACIONAMENTO> apresenta <NUM_LIVRES> lugares livres"
        };

          private string[] ticketsDescriptionStart = new string[] {
            "Ok, encontrei as seguintes filas em atendimento:",
            "Estas são as filas que estão a atender:"
          };

        private string[] ticketDescription = new string[] {
            "A fila <NOME_DA_FILA> que trata de assuntos <DESCRIÇÃO> vai no número <NÚMERO_DA_SENHA>. \n"
        };

         private string[] ticketNotFound = new string[] {
            "Lamento informar mas não encontrei nenhuma senha da fila <NOME_DA_FILA> em funcionamento",
            "Infelizmente não encontrei nenhuma fila em atendimento com a descrição <NOME_DA_FILA>"
        };

        private string[] ticketsServiceUnavailable = new string[] {
            "Não consegui encontrar nenhuma fila em atendimento. O serviço deve estar fechado.",
            "Estás com azar, o serviço de atendimento da universidade não está em funcionamento."
        };

        private string[] lastTicketNumber = new string[] {
            "Para a fila <NOME_DA_FILA> foi o número <NÚMERO_DA_SENHA>",
            "A fila de atendimento <NOME_DA_FILA> vai no número <NÚMERO_DA_SENHA>",
            "Neste momento, a última senha atendida da fila <NOME_DA_FILA> tem o número <NÚMERO_DA_SENHA>"
        };

        // ver plural e singular
         private string[] ticketAverageWaitingTime = new string[] {
            "Na fila <NOME_DA_FILA> o tempo médio de espera é de <TEMPO_ESPERA> minutos e o tempo médio de atendimento é de <TEMPO_ATENDIMENTO> minutos",
            "Na fila de atendimento <NOME_DA_FILA> demora-se cerca de <TEMPO_ESPERA> minutos à espera e <TEMPO_ATENDIMENTO> minutos a ser atendido",
            "Neste momento, na fila <NOME_DA_FILA> espera-se cerca de <TEMPO_ESPERA> minutos e é-se atendido em <TEMPO_ATENDIMENTO> minutos",
            "Vais ter de esperar <TEMPO_ESPERA> minutos na fila <NOME_DA_FILA> para seres atendido em cerca de <TEMPO_ATENDIMENTO> minutos"
        };

        private string[] ticketPeopleWaiting = new string[] {
            "Para a fila <NOME_DA_FILA> estão à espera <CLIENTES_EM_ESPERA> pessoas",
            "<CLIENTES_EM_ESPERA> pessoas estão à espera de serem atendida na fila <NOME_DA_FILA>",
            "Neste momento, a fila <NOME_DA_FILA> tem <CLIENTES_EM_ESPERA> à espera"
        };

        /*
         * FRASES PARA NEWS 
         */
        private string[] newsServiceUnavailable = new string[] {
            "Estranho não consegui encontrar nenhuma notícia, secalhar existem problemas no servidor.",
            "O serviço de notícias não parece estar a funcionar, não encontrei nenhuma."
        };

        private string[] allNewsSTART = new string[] {
            "Ok, encontrei as seguintes notícias:",
            "Encontrei as seguintes novidades"
        };
        private string[] allNews = new string[] {
            "<TITULO_NOTICA>, a seguir",
            "<TITULO_NOTICA>, depois",
            "<TITULO_NOTICA>, seguidamente",
            "<TITULO_NOTICA>, em seguida",
        };

        /*
         * Fr
         */

        public string getDisableCanteen(string canteenName) {return canteenDisable[random.Next(0, canteenDisable.Length)].Replace("<NOME_CANTINA>",canteenName);}
        public string getParkNotFound(string parkName){return parkNotFound[random.Next(0, parkNotFound.Length)].Replace("<NOME_PARQUE_ESTACIONAMENTO>", parkName);}
        public string getParkIsFree(ParkData park) {return parkIsFree[random.Next(0, parkIsFree.Length)].Replace("<NOME_PARQUE_ESTACIONAMENTO>", park.Nome).Replace("<NUM_LIVRES>", park.Livre.ToString());}

        public string getParkIsNotFree(ParkData park) {return parkIsNotFree[random.Next(0, parkIsNotFree.Length)].Replace("<NOME_PARQUE_ESTACIONAMENTO>", park.Nome);}

        public string getParkFreeSpots(ParkData park){return parkFreeSpots[random.Next(0, parkFreeSpots.Length)].Replace("<NOME_PARQUE_ESTACIONAMENTO>", park.Nome).Replace("<NUM_LIVRES>", park.Livre.ToString()); }

        public string getAllParksFree(List<ParkData> park)
        {
            StringBuilder sb = new StringBuilder(allParksFreeSTART[random.Next(0, allParksFreeSTART.Length)]);
            sb.Append(".\n");
            //TODO SORT PARK FOR FREE SPACE
            foreach (var p in park)
            {
                sb.Append(allParksFree[random.Next(0, allParksFree.Length)].Replace("<NOME_PARQUE_ESTACIONAMENTO>", p.Nome).Replace("<NUM_LIVRES>", p.Livre.ToString()));
                sb.Append(".\n");//n sei se o speak tem em conta pontuação
            }
            return sb.ToString();
        }
    
        public string getParkServiceUnavailable() { return parkServiceUnavailable[random.Next(0, parkServiceUnavailable.Length)]; }

        public string getTicketsInfo(List<TicketData> tickets) { 
            StringBuilder sb = new StringBuilder(ticketsDescriptionStart[0]);

            foreach(TicketData ticket in tickets)
            {
                sb.Append(ticketDescription[random.Next(0, ticketDescription.Length)]
                    .Replace("<NOME_DA_FILA>", ticket.Letter).Replace("<DESCRIÇÃO>", ticket.Description)
                    .Replace("<NÚMERO_DA_SENHA>", ticket.Latest.ToString()));
            }
            
            return sb.ToString();
        }

        public string getTicketNotFound(TicketData ticket) { return ticketNotFound[random.Next(0, ticketNotFound.Length)].Replace("<NOME_DA_FILA>", ticket.Description);}

        public string getTicketsServiceUnavailable() { return ticketsServiceUnavailable[random.Next(0, ticketsServiceUnavailable.Length)]; }

        public string getlastTicketNumber(TicketData ticket) { return lastTicketNumber[random.Next(0, lastTicketNumber.Length)].Replace("NOME_DA_FILA", ticket.Description).Replace("NÚMERO_DA_SENHA", ticket.Latest.ToString()); }
        
        public string getTicketAverageWaitingTime(TicketData ticket) { return ticketAverageWaitingTime[random.Next(0, ticketAverageWaitingTime.Length)].Replace("NOME_DA_FILA", ticket.Description).Replace("TEMPO_ESPERA", ticket.AverageWaitingTime.ToString()).Replace("TEMPO_ATENDIMENTO", ticket.AverageAtendingTime.ToString()); }
        
        public string getTicketPeopleWaiting(TicketData ticket) { return ticketPeopleWaiting[random.Next(0, ticketPeopleWaiting.Length)].Replace("NOME_DA_FILA", ticket.Description).Replace("CLIENTES_EM_ESPERA", ticket.ClientsWaiting.ToString()); }

        public string getNewsServiceUnavailable() { return newsServiceUnavailable[random.Next(0, newsServiceUnavailable.Length)]; }

        public string getAllNews(List<NewsData> news)
        {
            StringBuilder sb = new StringBuilder(allNewsSTART[random.Next(0, allNewsSTART.Length)]);
            sb.Append(".\n");
            //TODO SORT PARK FOR FREE SPACE
            foreach (var p in news)
            {
                sb.Append(allNews[random.Next(0, allNews.Length)].Replace("<TITULO_NOTICA>", p.Title));
                sb.Append(".\n");//n sei se o speak tem em conta pontuação
            }
            return sb.ToString();
        }
    }
    
}
