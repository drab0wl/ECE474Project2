using System.Windows.Forms;

namespace Project1
{
    /// <summary>
    /// Class used for Math Units
    /// </summary>
    class ArithmeticStation
    {
        /// <summary>
        /// Reservation station currently filling the math unit
        /// </summary>
        public int CurrentStation { get { return Station.Index; } }

        /// <summary>
        /// Flag whether or not unit is in use
        /// </summary>
        public bool InUse { get { return Full; } }

        public bool Broadcasted { get; set; }

        private int Cycles;
        private bool Full;
        private ReservationStation Station;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ArithmeticStation()
        {
            this.Full = false;
            this.Broadcasted = false;
        }

        /// <summary>
        /// Feeds a reservation station to the math unit
        /// </summary>
        /// <param name="input">Reservation Station</param>
        /// <returns>Success Flag</returns>
        public bool GiveStation(ReservationStation input)
        {
            bool success = false;
            if (input != null)
            {
                if (!Full)
                {
                    this.Station = input;
                    switch (Station.Op)
                    {
                        case (int)OP.Add:
                        case (int)OP.Sub:
                            this.Cycles = 2;
                            break;
                        case (int)OP.Mult:
                            this.Cycles = 10;
                            break;
                        case (int)OP.Div:
                            this.Cycles = 40;
                            break;
                    }
                    this.Full = true;
                    this.Broadcasted = false;
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            return success;
        }

        /// <summary>
        /// Cycles the math unit
        /// </summary>
        /// <returns>Incomplete Flag</returns>
        public bool Cycle()
        {
            bool retVal = false;
            if (Full)
            {
                if ((--this.Cycles) == 0)
                {
                    retVal = true;
                }
                if (Broadcasted)
                {
                    Full = false;
                }
            }
            return retVal;
        }

        /// <summary>
        /// Performs operation in unit
        /// </summary>
        /// <returns>Operation Result</returns>
        public int PerformOp()
        {
            int result = 0;
            try
            {
                switch (Station.Op)
                {
                    case (int)OP.Add:
                        result = Station.Vj + Station.Vk;
                        break;
                    case (int)OP.Sub:
                        result = Station.Vj - Station.Vk;
                        break;
                    case (int)OP.Mult:
                        result = Station.Vj * Station.Vk;
                        break;
                    case (int)OP.Div:
                        result = Station.Vj / Station.Vk;
                        break;
                }
                Station.Busy = false;
            }
            catch
            {
                MessageBox.Show("Processor attempted an invalid mathematical operation.  Values are invalid.  Check input file and try again.");
            }
            return result;
        }
    }
}
