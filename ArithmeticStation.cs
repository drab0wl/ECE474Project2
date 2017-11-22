using System.Windows.Forms;
using System;

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
        public int CurrentROB { get { return (int)(_ROBIndex % Algorithm.MAX_ROBS); } }

        /// <summary>
        /// Flag whether or not unit is in use
        /// </summary>
        public bool InUse { get { return _InUse; } }

        public bool Broadcasted { get; set; }

        public int Result {  get { return _Result; } }

        public bool Exception { get { return _Exception; } }

        public int Cycles { get { return _Cycles; } }

        public bool ReadyForBroadcast { get { return _ReadyForBroadcast; } }

        private bool _ReadyForBroadcast;
        private int _ROBIndex;
        private int _Cycles;
        private bool _InUse;
        private ReservationStation Station;
        private int _Result;
        private bool _Exception;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ArithmeticStation()
        {
            this._InUse = false;
            this._Exception = false;
            this.Broadcasted = false;
            this._ReadyForBroadcast = false;
        }

        /// <summary>
        /// Feeds a reservation station to the math unit
        /// </summary>
        /// <param name="input">Reservation Station</param>
        /// <returns>Success Flag</returns>
        public bool GiveStation(ReservationStation input, int robIndex)
        {
            bool success = false;
            if (input != null)
            {
                if (!InUse)
                {
                    this.Station = input;
                    this._ROBIndex = robIndex;
                    try
                    {
                        switch (Station.Op)
                        {
                            case (int)OP.Add:
                                _Result = input.Vj + input.Vk;
                                this._Cycles = 2;
                                break;
                            case (int)OP.Sub:
                                _Result = input.Vj - input.Vk;
                                this._Cycles = 2;
                                break;
                            case (int)OP.Mult:
                                _Result = input.Vj * input.Vk;
                                this._Cycles = 10;
                                break;
                            case (int)OP.Div:
                                _Result = input.Vj / input.Vk;
                                this._Cycles = 40;
                                break;
                        }
                    }
                    catch(ArithmeticException e)
                    {
                        this._Exception = true;
                        this._Cycles = 38;
                    }

                    this._InUse = true;
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
        public void Cycle()
        {
            if (InUse)
            {
                if (--this._Cycles == 0)
                {
                    this._InUse = false;
                    this._ReadyForBroadcast = true;
                }
            }
        }

        /// <summary>
        /// !!DEPRECATED!! Performs operation in unit
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
            }
            catch
            {
                MessageBox.Show("Processor attempted an invalid mathematical operation.  Values are invalid.  Check input file and try again.");
            }
            return result;
        }
    }
}
