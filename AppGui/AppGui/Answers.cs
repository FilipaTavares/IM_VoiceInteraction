using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppGui.Data;
using System.Text.RegularExpressions;

namespace AppGui
{
    class Answers
    {
        private Random random;
        private CultureInfo culture;

        public Answers() {
            random = new Random();
            this.culture = new CultureInfo("pt-PT");
        }

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
            "Infelizmente não encontrei nenhuma fila em atendimento com a descrição <NOME_DA_FILA>",
            "A fila <NOME_DA_FILA> não está aberta"
        };

        private string[] ticketsServiceUnavailable = new string[] {
            "Não encontrei nenhuma fila em atendimento. O serviço deve estar fechado.",
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
            "<CLIENTES_EM_ESPERA> pessoas estão à espera na fila <NOME_DA_FILA>",
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
            "<TITULO_NOTICA> \n\n\n"
        };

        private string[] newsDescription = new string[] {
            "<DESCRICAO>"
        };

        // 13 14 31 0 chuva moderada domingo 15/04/2018 00:00:00
        // return minTemp + " " + maxTemp + " " + windSpeed + " " + humidity + " " + description + " " + dayDescription
            //    + " " + Date.ToString();

        private string[] weatherInDay = new string[]
        {
            "<DIA> a previsão é de <DESC> com temperatura mínima de <MIN> graus e máxima de <MAX>.\nO vento vai soprar a <VEL> quilómetros por hora."
        };

        private string[] weatherDayInvalid = new string[] // exemplo 30 de fevereiro
        {
            "Desculpa, mas o dia que pediste <DIA> não existe",
            "Desculpa, mas o dia que pediste <DIA> é inválido"
        };

        private string[] weatherDayOutOfRange = new string[]
        {
            "Desculpa, não consigo ver o tempo para o dia <DIA>.\nA previsão é só até 17 dias",
            "Estás com azar, para o dia <DIA> não é possível saber o tempo.\nA previsão só vai até 17 dias"
        };

        private string[] weatherRainTrue = new string[]
        {
            "Sim.\n<DIA> a previsão é de <DESC>.",
            "Sim.\nÉ melhor levares o guarda-chuva.\n<DIA> a previsão é de <DESC>"
        };

        private string[] weatherRainFalse = new string[]
        {
            "Não.\n<DIA> a previsão é de <DESC>.",
            "Não.\nPodes deixar o guarda-chuva em casa.\n<DIA> a previsão é de <DESC>"
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
            sb.Append("\n");

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

        public string getlastTicketNumber(TicketData ticket) { return lastTicketNumber[random.Next(0, lastTicketNumber.Length)].Replace("<NOME_DA_FILA>", ticket.Description).Replace("<NÚMERO_DA_SENHA>", ticket.Latest.ToString()); }
        
        public string getTicketAverageWaitingTime(TicketData ticket) { return ticketAverageWaitingTime[random.Next(0, ticketAverageWaitingTime.Length)].Replace("<NOME_DA_FILA>", ticket.Description).Replace("<TEMPO_ESPERA>", ticket.AverageWaitingTime.ToString()).Replace("<TEMPO_ATENDIMENTO>", ticket.AverageAtendingTime.ToString()); }
        
        public string getTicketPeopleWaiting(TicketData ticket) { return ticketPeopleWaiting[random.Next(0, ticketPeopleWaiting.Length)].Replace("<NOME_DA_FILA>", ticket.Description).Replace("<CLIENTES_EM_ESPERA>", ticket.ClientsWaiting.ToString()); }

        public string getNewsServiceUnavailable() { return newsServiceUnavailable[random.Next(0, newsServiceUnavailable.Length)]; }

        public string getAllNews(List<NewsData> news)
        {
            StringBuilder sb = new StringBuilder(allNewsSTART[random.Next(0, allNewsSTART.Length)]);
            sb.Append(".\n");
            //TODO SORT PARK FOR FREE SPACE
            foreach (var p in news)
            {
                sb.Append(allNews[random.Next(0, allNews.Length)].Replace("<TITULO_NOTICA>", p.Title));
            }
            return sb.ToString();
        }

        public string getNewsDescription(NewsData newsData) { return newsDescription[random.Next(0, newsDescription.Length)].Replace("<DESCRICAO>", Regex.Replace(newsData.Description, "<.*?>", String.Empty)); ; }

        public string getWeatherInDay(WeatherData weather)
        {
            if (weather.DayDescription.Equals("hoje") || weather.DayDescription.Equals("amanhã") || weather.DayDescription.Contains("no dia"))
            {
                return weatherInDay[random.Next(0, weatherInDay.Length)].Replace("<DIA>", weather.DayDescription).Replace("<DESC>", weather.Description).Replace("<MIN>", weather.MinTemp.ToString()).
                    Replace("<MAX>", weather.MaxTemp.ToString()).Replace("<VEL>", weather.WindSpeed.ToString());
            }

            else
            {
                return weatherInDay[random.Next(0, weatherInDay.Length)].Replace("<DIA>", weather.DayDescription + ", no dia " + weather.Date.Day).Replace("<DESC>", weather.Description).Replace("<MIN>", weather.MinTemp.ToString()).
                    Replace("<MAX>", weather.MaxTemp.ToString()).Replace("<VEL>", weather.WindSpeed.ToString());
            }

        }

        public string getWeatherDayOutOfRange(DateTime date) { return weatherDayOutOfRange[random.Next(0, weatherDayOutOfRange.Length)].Replace("<DIA>", date.Day + " de " + culture.DateTimeFormat.GetMonthName(date.Month)); }

        public string getWeatherDayInvalid(DateTime date) { return weatherDayInvalid[random.Next(0, weatherDayInvalid.Length)].Replace("<DIA>", date.Day + " de " + culture.DateTimeFormat.GetMonthName(date.Month)); }

        public string getWeatherRain(WeatherData weather) {
            if (weather.Description.Contains("chuva"))
            {
                return getWeatherRainAnswer(weather, weatherRainTrue);
            }

            else
            {
                return getWeatherRainAnswer(weather, weatherRainFalse);
            }
           
               

            }

        private string getWeatherRainAnswer(WeatherData weather, string[] rainArray)
        {
            if (weather.DayDescription.Equals("hoje") || weather.DayDescription.Equals("amanhã") || weather.DayDescription.Contains("no dia"))
            {
                return rainArray[random.Next(0, rainArray.Length)].Replace("<DIA>", weather.DayDescription).Replace("<DESC>", weather.Description);
            }

            else
            {
                return rainArray[random.Next(0, rainArray.Length)].Replace("<DIA>", weather.DayDescription + ", no dia " + weather.Date.Day).Replace("<DESC>", weather.Description);
            }
        }
    }

    }
