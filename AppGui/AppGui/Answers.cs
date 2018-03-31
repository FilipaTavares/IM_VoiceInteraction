using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string getDisableCanteen(string canteenName) {

            return canteenDisable[random.Next(0, canteenDisable.Length)].Replace("<NOME_CANTINA>",canteenName);
            
        }
    }
}
