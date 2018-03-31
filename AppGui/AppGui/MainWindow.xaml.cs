using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using mmisharp;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AppGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MmiCommunication mmiC;
        private HttpClient ementasClient;
        private HttpClient sacClient;
        private HttpClient sasClient;
    
        private Tts t;

        public MainWindow()
        {
            InitializeComponent();

            t = new Tts();

            mmiC = new MmiCommunication("localhost", 8000, "User1", "GUI");
            mmiC.Message += MmiC_Message;
            mmiC.Start();

            ementasClient = new HttpClient();
            ementasClient.BaseAddress = new Uri("http://services.web.ua.pt/sas/ementas");

        }

        private void MmiC_Message(object sender, MmiEventArgs e)
        {
            Console.WriteLine(e.Message);
            var doc = XDocument.Parse(e.Message);
            var com = doc.Descendants("command").FirstOrDefault().Value;
            dynamic json = JsonConvert.DeserializeObject(com);

            Shape _s = null;
            switch ((string)json.recognized[0].ToString())
            {
                case "CANTEENS":
                    string queryParams = "?format=json";
                    Console.WriteLine("CANTINASSSASAS");
                    ementasClient.GetStringAsync(queryParams).ContinueWith((response)=>handleCanteenResponse(response.Result, (string)json.recognized[2].ToString()));
                    break;
                case "CIRCLE": _s = circle;
                    break;
                case "TRIANGLE": _s = triangle;
                    break;
            }

            App.Current.Dispatcher.Invoke(() =>
            {
                switch ((string)json.recognized[1].ToString())
                {
                    case "GREEN":
                        _s.Fill = Brushes.Green;
                        break;
                    case "BLUE":
                        _s.Fill = Brushes.Blue;
                        break;
                    case "RED":
                        _s.Fill = Brushes.Red;
                        break;
                }
            });



        }

        private void handleCanteenResponse(string response, string canteen) {

            dynamic json = JsonConvert.DeserializeObject(response);

            foreach (dynamic canteenResponse in json.menus.menu) {
                //just testing
                if ((canteenResponse["@attributes"].canteen.ToString().ToUpper().Contains(canteen) == true)) { 
                    Console.WriteLine("PARSE!!");
                    t.Speak(canteenResponse["@attributes"].disabled.ToString());
                    break;
                }
            }
        }
    }
}
