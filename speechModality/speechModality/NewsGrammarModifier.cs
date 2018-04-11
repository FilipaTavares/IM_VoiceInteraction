using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace speechModality
{
    class NewsGrammarModifier
    {

        private SpeechRecognitionEngine sr;

        private string xmlFile;
        
        public NewsGrammarModifier(SpeechRecognitionEngine sr)
        {
            this.sr = sr;
            this.xmlFile = Environment.CurrentDirectory + "\\ptG.grxml";
        }

        private string replaceXMLinvalidCaracters(string phrase) {
            return phrase.Replace("&", " e ").Replace("<","").Replace(">", "").Replace("�","");
        }

        public void addGrammar(string[] news) {

            StringBuilder sb = new StringBuilder();

            //sb.Append("<!-- PLACEHOLD FOR DYNAMIC NEWS HERE DYNAMIC GENERATE -->\n\n");
            for (int i = 0; i < news.Length; i++) {
                sb.Append("<item><sapi:subset sapi:match=\"ordered-subset\">" + replaceXMLinvalidCaracters(news[i]) + "</sapi:subset><tag>out=\""+ i +"\"</tag></item>\n");
                //sb.Append("<item>" + replaceXMLinvalidCaracters(n) + "</item>\n");

            }

            //NOT EFFICIENT !!!!!! TODO small prioraty
            StringBuilder xmlOutput = new StringBuilder();
            //string text = File.ReadAllText(xmlFile + "\\ptG.grxml");
            //text = text.Replace("<!-- PLACEHOLD FOR DYNAMIC NEWS HERE DYNAMIC GENERATE -->\n<rule id=\"dynamicNews\">", sb.ToString());
            //File.WriteAllText(xmlFile, text);

            string line;
            StreamReader reader = new StreamReader(xmlFile);
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("<rule id=\"dynamicNews\">"))
                {
                    xmlOutput.Append("<rule id=\"dynamicNews\">\n");
                    xmlOutput.Append("<one-of>\n");
                    xmlOutput.Append(sb.ToString());
                    xmlOutput.Append("</one-of>\n");
                    xmlOutput.Append("</rule>\n");
                    xmlOutput.Append("</grammar>\n");
                    break;
                }
                else {
                    xmlOutput.Append(line + "\n");
                }
            }
            reader.Close();
            
            Console.WriteLine(xmlFile);
            //Console.WriteLine(xmlOutput.ToString());
            File.WriteAllText(xmlFile, xmlOutput.ToString());
            Grammar gr=new Grammar(xmlFile);

            
            sr.LoadGrammar(gr);
        }


    }
}
