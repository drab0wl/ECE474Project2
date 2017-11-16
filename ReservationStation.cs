namespace Project1
{
    /// <summary>
    /// Class used for a reservation station
    /// </summary>
    class ReservationStation
    {
        public bool Busy { get; set; }
        public int Op { get; set; }
        public int Vj { get; set; }
        public int Vk { get; set; }
        public int Qj { get; set; }
        public int Qk { get; set; }
        public bool Dispatched { get; set; }
        public int Index { get; set; }
        public bool Ready { get; set; }
        public int ROBIndex { get; set; }        

        public ReservationStation()
        {
            Busy = false;
            Dispatched = false;
            Ready = true;
            Qj = Algorithm.DEFAULT_Q_VALUE;
            Qk = Algorithm.DEFAULT_Q_VALUE;
            ROBIndex = Algorithm.DEFAULT_Q_VALUE;
        } 
    }
}
