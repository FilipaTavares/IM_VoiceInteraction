using AppGui.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGui
{
    class DialogueManager
    {

        private ClientCanteen canteen;
        private ClientSAS parking;
        private ClientSAC tickets;
        private ClientNews news;
        private ClientWeather weather;
        private Tts t;
        private Answers answers;

        //state of the dialogue like memory :)
        //private ...
        private void greathingsCallback()
        {
            t.Speak(answers.getGreathings());
        }

        public DialogueManager()
        {
            canteen = new ClientCanteen(this);
            parking = new ClientSAS(this);
            tickets = new ClientSAC(this);
            news = new ClientNews(this);
            weather = new ClientWeather(this);
            answers = new Answers();
            t = new Tts(greathingsCallback);
            

        }

        public void handleIMcommand(string command)
        {
            dynamic json = JsonConvert.DeserializeObject(command);

            string[] array;
            array = new string[json.recognized.Count - 1];
            for (int i = 1; i < json.recognized.Count; i++)          //0 alredy handled
                array[i - 1] = (string)json.recognized[i].ToString();

            switch ((string)json.recognized[0].ToString())
            {
                case "CANTEENS":
                    //use Canteen API
                    canteen.request((string)json.recognized[1].ToString(), (string)json.recognized[2].ToString());

                    break;

                case "SAS":
                    Console.WriteLine("SAS");
                    parking.request(array);
                    
                    break;

                case "SAC":
                    Console.WriteLine("SAC");
                    tickets.request(array);
                    
                    break;
                case "NEWS":

                    Console.WriteLine("NEWS");
                    news.request(array);

                    break;

                case "WEATHER":
                    Console.WriteLine("WEATHER");

                    array = new string[json.recognized.Count - 1];
                    for (int i = 1; i < json.recognized.Count; i++)
                        array[i - 1] = (string)json.recognized[i].ToString();

                    weather.request(array);
                    break;
                case "HELP":
                    Console.WriteLine("HELP");
                    manageDialogueHelp();


                    break;
            }
        }

        public void close()
        {
            //do some close stuff!!!
            t.close();
        }

        public void manageDialogueCanteen(CanteenData canteen) {

            string phrase = "";

            if (canteen.Disabled.Equals("0"))
            {
                //cantina aberta
            }
            else {
                phrase = answers.getDisableCanteen(canteen.Canteen);
            }

            
            t.Speak(phrase);
        }

        public void manageDialogueSAS(ParkData park, string[] args)
        {

            string phrase = "";
            
            if (park.Found)
            {
                switch (args[1])
                {
                    case ("SUBTYPE1"):
                    case ("SUBTYPE2"):
                        if (park.Livre > 0)
                            phrase = answers.getParkIsFree(park);
                        else
                            phrase = answers.getParkIsNotFree(park);
                        
                        break;
                    case ("SUBTYPE3"):
                        if (park.Livre > 0)
                            phrase = answers.getParkFreeSpots(park);
                        else
                            phrase = answers.getParkNoFreeSpots(park);
                        
                        break;
                }
            }
            else
            {
                phrase = answers.getParkNotFound(park.Nome);
            }


            t.Speak(phrase);
        }

        public void manageDialogueSAS(List<ParkData> park, string[] args) {
            string phrase = "";
            Console.WriteLine("ENTER DIALOGUE SAS");
            if (park.Count > 0) {
                if (args[0].Equals("TYPE1"))
                    phrase = answers.getAllParksFree(park);
                else //TYPE3
                    phrase = answers.getPraksHelp(park);

            } else {
                phrase = answers.getParkServiceUnavailable();
            }

            t.Speak(phrase);
        }

        public void manageDialogueSAC()
        {
            string phrase = answers.getTicketsServiceUnavailable();

            t.Speak(phrase);

            Console.WriteLine(phrase);
        }

        public void manageDialogueSAC(List<TicketData> tickets) {

            string phrase = "";

            bool isEmpty = !tickets.Any();  

            if (isEmpty)
            {
                phrase = answers.getTicketsServiceUnavailable();
            }

            else
            {
                phrase = answers.getTicketsInfo(tickets);
            }

            t.Speak(phrase);
            Console.WriteLine(phrase);
        }


         public void manageDialogueSAC(TicketData ticket, string type) {

            string phrase = "";

            if (ticket.Enabled)
            {
                Console.WriteLine("ENABLED");

                if (type.Equals("TYPE2"))
                {
                    phrase = answers.getlastTicketNumber(ticket);
                    Console.WriteLine("GET LAST TICKET NUMBER");
                }

                else if (type.Equals("TYPE3"))
                {
                    phrase = answers.getTicketAverageWaitingTime(ticket);
                    Console.WriteLine("GET AVERAGE WAITING TICKET NUMBER");
                }

                else if (type.Equals("TYPE4"))
                {
                    phrase = answers.getTicketPeopleWaiting(ticket);
                    Console.WriteLine("GET PEOPLE WAITING TICKET NUMBER");
                }

                else if (type.Equals("TYPE5"))
                {
                    phrase = answers.getTicketLineA(ticket);
                    Console.WriteLine("GET INFO ABOUT TICKET A TO PAY FEES");
                }

            }

            else
            {
                if (type.Equals("TYPE5"))
                {
                    phrase = answers.getTicketLineAClosed(ticket);
                    Console.WriteLine("TICKET A CLOSED - PROPINAS");
                }

                else
                {
                    phrase = answers.getTicketNotFound(ticket);
                    Console.WriteLine("TICKET NOT FOUND");
                }
            }
            
            t.Speak(phrase);
            Console.WriteLine(phrase);
        }

        public void manageDialogueNews(List<NewsData> news, string[] args)
        {
            string phrase = "";

            if (news.Count < 0){
                if (args[0].Equals("TYPE3"))
                    phrase = answers.getHelpNews(true);
                else
                    phrase = answers.getNewsServiceUnavailable();
            }
            else { 
                switch (args[0]) {
                    case "TYPE1":
                        phrase = answers.getAllNews(news);

                        List<string> lNews = new List<string>();
                        foreach (NewsData nD in news)
                        {
                            lNews.Add(nD.Title);
                        }

                        Console.WriteLine("UPDATE GRAMMAR");
                        t.addNewsToGrammar(lNews);

                        break;
                    case "TYPE2":
                        phrase = answers.getNewsDescription(news[int.Parse(args[1])]);
                        break;
                    case "TYPE3":
                        phrase = answers.getHelpNews(true);
                        break;
                }
            }


            t.Speak(phrase);
            Console.WriteLine(phrase);
        }

        public void manageDialogueWeather(WeatherData weather, string type)
        {
            string phrase = "";

            if (type.Equals("TYPE1"))
            {
                phrase = answers.getWeatherInDay(weather);
            }

            else
            {
                phrase = answers.getWeatherRain(weather);
            }

            t.Speak(phrase);
            Console.WriteLine(phrase);
        }

        public void manageDialogueWeather(DateTime date, string flag)
        {
            string phrase = "";
            if (flag.Equals("invalid"))
                phrase = answers.getWeatherDayInvalid(date);
        
            else
            {
                phrase = answers.getWeatherDayOutOfRange(date);
            }
            t.Speak(phrase);
            Console.WriteLine(phrase);

        }

        public void manageDialogueHelp() {
            t.Speak(answers.getHelp());
        }

        public void manageDialogueWeatherConnectionErrors(string error, string description)
        {
            string phrase = "";

            switch (error)
            {
                case "web exception":
                    phrase = answers.getConnectionError(description);
                    break;

                case "warning timeout":
                    phrase = answers.getWarningSlowConnection(description);
                    break;

                case "timeout":
                    phrase = answers.getConnectionTimeoutError(description);
                    break;
            }

            t.Speak(phrase);
        }

    }
}
