using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class Algorithm
    {
        public Queue<Instruction> InstructionQueue { get; }
        public List<RatEntry> RAT { get; }

        private List<ReservationStation> AddStations;
        private List<ReservationStation> MultStations;
        private List<ReorderBuffer> ROBs;
        public int IssuePointer { get { return (int)(_IssuePointer % MAX_ROBS); } }
        private int _IssuePointer = 0;
        public int CommitPointer { get { return _CommitPointer; } }
        public int _CommitPointer = 0;

        private ArithmeticStation AddUnit;
        private ArithmeticStation MultUnit;

        public static readonly uint MAX_RAT_ENTRIES = 8;
        public static readonly int MAX_ADD_STATIONS = 3;
        public static readonly uint MAX_MULT_STATIONS = 2;
        public static readonly uint DEFAULT_RAT = 0xFF;
        public static readonly uint ROB_INDEX = 0;
        public static readonly uint CAPTURE_VALUE = 1;
        public static readonly int DEFAULT_Q_VALUE = -1;
        public static readonly uint MAX_ROBS = 6;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Algorithm()
        {
            // Create data structures
            InstructionQueue = new Queue<Instruction>();
            RAT = new List<RatEntry>();
            AddStations = new List<ReservationStation>();
            MultStations = new List<ReservationStation>();
            AddUnit = new ArithmeticStation();
            MultUnit = new ArithmeticStation();
            ROBs = new List<ReorderBuffer>();

            // Create reservation stations
            for (int i = 0; i < MAX_ADD_STATIONS; i++)
            {
                ReservationStation station = new ReservationStation();
                station.Index = i;
                AddStations.Add(station);
            }
            for (int i = 0; i < MAX_MULT_STATIONS; i++)
            {
                ReservationStation station = new ReservationStation();
                station.Index = i + MAX_ADD_STATIONS;
                MultStations.Add(station);
            }
            
            // Create ROBs
            for (int i = 0; i < MAX_ROBS; i++)
            {
                ReorderBuffer rob = new ReorderBuffer();
                rob.Index = i;
                ROBs.Add(rob);
            }
        }

        /// <summary>
        /// Feed instruction to reservation station
        /// </summary>
        /// <param name="stations">Reservation Station Structure</param>
        /// <param name="index">Reservation Station Index</param>
        /// <returns>Reservation station created from instruction</returns>
        private ReservationStation SetRS(List<ReservationStation> stations, int index)
        {
            ReservationStation reservationStation = stations[index];
            stations[index].Busy = true;
            stations[index].Ready = false;

            Instruction inst = this.InstructionQueue.Dequeue();
            stations[index].Op = inst.Op;
            // Check RAT for placeholders
            if (IsRATPointingToRF(inst.RF1))
            {
                stations[index].Vj = inst.V1;
                stations[index].Qj = DEFAULT_Q_VALUE;
            }
            else
            {
                stations[index].Qj = (int)this.RAT[inst.RF1].RAT;
            }
            if (IsRATPointingToRF(inst.RF2))
            {
                stations[index].Vk = inst.V2;
                stations[index].Qk = DEFAULT_Q_VALUE;
            }
            else
            {
                stations[index].Qk = (int)this.RAT[inst.RF2].RAT;
            }
            return reservationStation;
        }

        /// <summary>
        /// Checks if RAT is pointing to the RF
        /// </summary>
        /// <param name="register">Register Index</param>
        /// <returns>Flag if RAT is pointing to RF</returns>
        public bool IsRATPointingToRF(int register)
        {
            bool retVal = true;
            if (RAT[register].RAT != DEFAULT_RAT)
            {
                retVal = false;
            }
            return retVal;
        }

        /// <summary>
        /// Performs the issue operation
        /// </summary>
        /// <returns>The reservation station dispatched</returns>
        public ReservationStation Issue()
        {
            ReservationStation reservationStation = null;
            if (this.InstructionQueue.Count > 0)
            {

                // See which data structure instruction belongs to
                if (CheckPointers())
                {
                    Instruction inst = this.InstructionQueue.First();
                    switch (inst.Op)
                    {
                        case (int)OP.Add:
                        case (int)OP.Sub:
                            for (int i = 0; i < MAX_ADD_STATIONS; i++)
                            {
                                if (AddStations[i].Ready)
                                {
                                    reservationStation = SetRS(AddStations, i);
                                    break;
                                }
                            }
                            break;
                        case (int)OP.Mult:
                        case (int)OP.Div:
                            for (int i = 0; i < MAX_MULT_STATIONS; i++)
                            {
                                if (MultStations[i].Ready)
                                {
                                    reservationStation = SetRS(MultStations, i);
                                    break;
                                }
                            }
                            break;
                    }
                    if (reservationStation != null)
                    {
                        reservationStation.Dispatched = false;
                        // Update RAT
                        this.RAT[inst.DestReg].RAT = (uint)this.IssuePointer;

                        // Update ROB
                        this.ROBs[IssuePointer].RegisterFile = inst.DestReg;
                        this._IssuePointer++;
                    }
                }
                // Stations are ready after no longer busy for one cycle
                for (int i = 0; i < MAX_MULT_STATIONS; i++)
                {
                    if (!MultStations[i].Busy)
                    {
                        MultStations[i].Ready = true;
                    }
                }
                for (int i = 0; i < MAX_ADD_STATIONS; i++)
                {
                    if (!AddStations[i].Busy)
                    {
                        AddStations[i].Ready = true;
                    }
                }
            }
            return reservationStation;
        }

        /// <summary>
        /// Performs the dispatch operation
        /// </summary>
        /// <returns>Reservation station of instruction dispatched</returns>
        public ReservationStation Dispatch()
        {
            ReservationStation station = null;
            bool dispatched = false;
            int robIndex = (int)((_IssuePointer - 1) % MAX_ROBS);
            if (!MultUnit.InUse)
            {
                for (int i = 0; i < MultStations.Count; i++)
                {
                    if ((MultStations[i].Qj == DEFAULT_Q_VALUE) && 
                        (MultStations[i].Qk == DEFAULT_Q_VALUE) && 
                        !MultStations[i].Dispatched && 
                        MultStations[i].Busy)
                    {
                        station = MultStations[i];
                        station.Dispatched = true;
                        
                        MultUnit.GiveStation(station, robIndex);
                        dispatched = true;
                        break;
                    }
                }
            }
            // Ensures only one station is dispatched per operation
            if (!dispatched)
            {
                if (!AddUnit.InUse)
                {
                    for (int i = 0; i < AddStations.Count; i++)
                    {
                        if ((AddStations[i].Qj == DEFAULT_Q_VALUE) && 
                            (AddStations[i].Qk == DEFAULT_Q_VALUE) && 
                            !AddStations[i].Dispatched && 
                            AddStations[i].Busy)
                        {
                            station = AddStations[i];
                            station.Dispatched = true;
                            AddUnit.GiveStation(station, robIndex);
                            dispatched = true;
                            break;
                        }
                    }
                }
            }
            station.Busy = false;
            return station;
        }
        
        /// <summary>
        /// Performs the broadcast operation
        /// </summary>
        /// <returns>Reservation station broadcasted</returns>
        public int[] Broadcast()
        {
            int[] retVal = {(int)DEFAULT_RAT, 0 };

            // Checks multiply unit first for broadcast, then add unit
            if (MultUnit.Cycle())
            {
                retVal[CAPTURE_VALUE] = MultUnit.PerformOp();
                retVal[ROB_INDEX] = MultUnit.CurrentROB;
                MultUnit.Broadcasted = true;
            }
            else if(AddUnit.Cycle())
            {
                retVal[CAPTURE_VALUE] = AddUnit.PerformOp();
                retVal[ROB_INDEX] = AddUnit.CurrentROB;
                AddUnit.Broadcasted = true;
            }
            // Broadcast values
            if (retVal[ROB_INDEX] != (int)DEFAULT_RAT)
            {
                for (int i = 0; i < AddStations.Count; i++)
                {
                    if (AddStations[i].Qj == retVal[ROB_INDEX])
                    {
                        AddStations[i].Vj = retVal[CAPTURE_VALUE];
                        AddStations[i].Qj = DEFAULT_Q_VALUE;
                    }
                    if (AddStations[i].Qk == retVal[ROB_INDEX])
                    {
                        AddStations[i].Vk = retVal[CAPTURE_VALUE];
                        AddStations[i].Qk = DEFAULT_Q_VALUE;
                    }
                }

                for (int i = 0; i < MultStations.Count; i++)
                {
                    if (MultStations[i].Qj == retVal[ROB_INDEX])
                    {
                        MultStations[i].Vj = retVal[CAPTURE_VALUE];
                        MultStations[i].Qj = DEFAULT_Q_VALUE;
                    }
                    if (MultStations[i].Qk == retVal[ROB_INDEX])
                    {
                        MultStations[i].Vk = retVal[CAPTURE_VALUE];
                        MultStations[i].Qk = DEFAULT_Q_VALUE;
                    }
                }

                // Update ROB's value
                for (int i = 0; i < ROBs.Count; i++)
                {
                    if (ROBs[i].Index == retVal[ROB_INDEX])
                    {
                        ROBs[i].Value = retVal[CAPTURE_VALUE];
                        ROBs[i].Done = true;
                        break;
                    }
                }

                // Commented out for ROB
                //for (int i = 0; i < RAT.Count; i++)
                //{
                //    if (RAT[i].RAT == retVal[STATION_INDEX])
                //    {
                //        RAT[i].RegisterFile = retVal[CAPTURE_VALUE];
                //        RAT[i].RAT = DEFAULT_RAT;
                //    }
                //}
            }
            return retVal;
        } 

        public void Commit()
        {
            for (int i = 0; i < RAT.Count; i++)
            {
                if (RAT[i].RAT == CommitPointer)
                {
                    // update rf and rat to rf
                }
            }
        }

        private bool CheckPointers()
        {
            bool issueReady = false;

            if (IssuePointer == 0)
            {
                issueReady = true;
            }
            else if ((_IssuePointer > _CommitPointer) &&
                    (_IssuePointer - _CommitPointer < MAX_ROBS))
            {
                issueReady = true;
            }
            return issueReady;
        }
    }
}
