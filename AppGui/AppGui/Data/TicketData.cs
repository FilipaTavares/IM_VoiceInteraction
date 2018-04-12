namespace AppGui.Data
{
    public class TicketData
    {

        private string letter;
        private string description;
        private int latest;
        private int averageAtendingTime; 
        private int averageWaitingTime;
        private int clientsWaiting;
        private bool enabled;

        public TicketData(string letter, string description, bool enabled)
        {
            this.letter = letter;
            this.description = description;
            this.enabled = enabled;
        }

        public string Letter { get => letter; set => letter = value; }

        public string Description { get => description; set => description = value; }

        public int Latest { get => latest; set => latest = value; }

        public int AverageAtendingTime { get => averageAtendingTime; set => averageAtendingTime = value; }

        public int AverageWaitingTime { get => averageWaitingTime; set => averageWaitingTime = value; }

        public int ClientsWaiting { get => clientsWaiting; set => clientsWaiting = value; }

        public bool Enabled { get => enabled; set => enabled = value; }
    }
}