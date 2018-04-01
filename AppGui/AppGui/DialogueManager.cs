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
        private Tts t;
        private Answers answers;

        //state of the dialogue like memory :)
        //private ...

        public DialogueManager()
        {
            canteen = new ClientCanteen(this);
            parking = new ClientSAS(this);
            t = new Tts();
            answers = new Answers();


        }

        public void handleIMcommand(string command)
        {
            dynamic json = JsonConvert.DeserializeObject(command);

            switch ((string)json.recognized[0].ToString())
            {
                case "CANTEENS":
                    

                    //use Canteen API
                    canteen.request((string)json.recognized[1].ToString(), (string)json.recognized[2].ToString());

                    break;
                case "SAS":
                    Console.WriteLine("SAS");

                    string[] array = new string[json.recognized.Count-1];
                    for (int i = 1; i < json.recognized.Count; i++)//0 alredy handle
                        array[i-1] = (string)json.recognized[i].ToString();

                    parking.request(array);
                    
                    break;
                case "SAC":
                    
                    break;
                case "NEWS":

                    break;
                case "WEATHER":

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
                switch (args[0])
                {
                    case ("TYPE1"):
                        if (park.Livre > 0)
                        {
                            phrase = answers.getParkIsFree(park);
                        }
                        else {
                            phrase = answers.getParkIsNotFree(park);
                        }
                        break;
                    case ("TYPE2"):
                            phrase = answers.getParkFreeSpots(park);
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

            if (park.Count > 0) {
                phrase = answers.getAllParksFree(park);
            } else {
                phrase = answers.getParkServiceUnavailable();
            }

            t.Speak(phrase);
        }

    }
}
