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
        private Tts t;
        private Answers answers;

        //state of the dialogue like memory :)
        //private ...

        public DialogueManager()
        {
            canteen = new ClientCanteen(this);
            t = new Tts();
            answers = new Answers();


        }

        public void handleIMcommand(string command)
        {
            dynamic json = JsonConvert.DeserializeObject(command);

            switch ((string)json.recognized[0].ToString())
            {
                case "CANTEENS":
                    Console.WriteLine("CANTINASSSASAS");

                    //use Canteen API
                    canteen.request((string)json.recognized[1].ToString(), (string)json.recognized[2].ToString());

                    break;
                case "SAS":
                    
                    break;
                case "SAC":
                    
                    break;
                case "NEWS":

                    break;
                case "WEATHER":

                    break;
            }
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

    }
}
