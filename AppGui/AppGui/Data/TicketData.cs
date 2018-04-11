namespace AppGui.Data
{
    public class TicketData
    {

        private string letter;
        private string description;
        private int latest;
        private int averageAtendingTime; // ver se minutos ou segundos  
        private int averageWaitingTime;
        private int clientsWaiting;
        private bool found;

        public string Letter
        {
            get
            {
                return letter;
            }

            set
            {
                letter = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public int Latest
        {
            get
            {
                return latest;
            }

            set
            {
                latest = value;
            }
        }

        public int AverageAtendingTime
        {
            get
            {
                return averageAtendingTime;
            }

            set
            {
                averageAtendingTime = value;
            }
        }

        public int AverageWaitingTime
        {
            get
            {
                return averageWaitingTime;
            }

            set
            {
                averageWaitingTime = value;
            }
        }

        public int ClientsWaiting
        {
            get
            {
                return clientsWaiting;
            }

            set
            {
                clientsWaiting = value;
            }
        }

        public bool Found
        {
            get
            {
                return found;
            }

            set
            {
                found = value;
            }
        }

        public TicketData(string letter, bool found)
        {
            this.Letter = letter;
            this.Found = found;
        }

    }
}