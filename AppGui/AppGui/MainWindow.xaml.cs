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

        private DialogueManager dManager;

        public MainWindow()
        {
            InitializeComponent();

      
            dManager = new DialogueManager();

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

            dManager.handleIMcommand(com);

        }


    }
}
