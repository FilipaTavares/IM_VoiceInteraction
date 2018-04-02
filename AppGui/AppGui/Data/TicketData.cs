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

        public TicketData(string letter, bool found)
        {
            this.letter = letter;
            this.found = found;
        }

        public string Letter { get => letter; set => letter = value; }

        public string Description { get => description; set => description = value; }

        public int Latest { get => latest; set => latest = value; }

        public int AverageAtendingTime { get => averageAtendingTime; set => averageAtendingTime = value; }

        public int AverageWaitingTime { get => averageWaitingTime; set => averageWaitingTime = value; }

        public int ClientsWaiting { get => clientsWaiting; set => clientsWaiting = value; }

        public bool Found { get => found; set => found = value; }
    }
}