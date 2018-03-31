using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace AppGui
{
    class ClientCanteen
    {

        private HttpClient client;
        private DialogueManager dManager;

        public ClientCanteen(DialogueManager dManager) {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://services.web.ua.pt/sas/ementas");
            this.dManager = dManager;
        }

        public void request(string mealType, string canteen) {
            string queryParams = "?format=json";
            client.GetStringAsync(queryParams).ContinueWith((response) => handleCanteenResponse(response.Result,mealType,canteen));
        }


        private void handleCanteenResponse(string response, string mealType, string canteen)
        {
 
            dynamic json = JsonConvert.DeserializeObject(response);

            foreach (dynamic canteenResponse in json.menus.menu)
            {
                //just testing
                if ((canteenResponse["@attributes"].canteen.ToString().Contains(canteen)) && (canteenResponse["@attributes"].meal.ToString().Equals(mealType)))
                {
                    
                    CanteenData canteenObj = new CanteenData();
                    canteenObj.Canteen = canteen;
                    canteenObj.Meal = canteenResponse["@attributes"].meal.ToString();
                    canteenObj.Date = canteenResponse["@attributes"].date.ToString();
                    canteenObj.Weekday = canteenResponse["@attributes"].weekday.ToString();
                    canteenObj.WeekdayNr = int.Parse(canteenResponse["@attributes"].weekdayNr.ToString());
                    canteenObj.Disabled = canteenResponse["@attributes"].disabled.ToString();

                    dManager.manageDialogueCanteen(canteenObj);

                    break;
                }
            }
        }
    }
}
