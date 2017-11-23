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
        public List<ReorderBuffer> ROBs { get; }

        private List<ReservationStation> AddStations;
        private List<ReservationStation> MultStations;
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
            stations[index].ROBIndex = this.IssuePointer;
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
                        MultUnit.GiveStation(station, station.ROBIndex);
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
                            AddUnit.GiveStation(station, station.ROBIndex);
                            dispatched = true;
                            break;
                        }
                    }
                }
            }
            if (station != null)
            {
                station.Busy = false;
            }
            return station;
        }
        
        /// <summary>
        /// Performs the broadcast operation
        /// </summary>
        /// <returns>Reservation station broadcasted</returns>
        public ArithmeticStation Broadcast()
        {
            MultUnit.Cycle();
            AddUnit.Cycle();
            ArithmeticStation mathStation = null;
            
            // Checks multiply unit first for broadcast, then add unit
            if (!MultUnit.InUse && MultUnit.ReadyForBroadcast && !MultUnit.Broadcasted)
            {
                mathStation = MultUnit;
                MultUnit.Broadcasted = true;
            }
            else if (!AddUnit.InUse && AddUnit.ReadyForBroadcast && !AddUnit.Broadcasted)
            {
                mathStation = AddUnit;
                AddUnit.Broadcasted = true;
            }

            // Broadcast values
            if (mathStation != null && !mathStation.InUse)
            {
                for (int i = 0; i < AddStations.Count; i++)
                {
                    if (AddStations[i].Qj == mathStation.CurrentROB)
                    {
                        AddStations[i].Vj = mathStation.Result;
                        AddStations[i].Qj = DEFAULT_Q_VALUE;
                    }
                    if (AddStations[i].Qk == mathStation.CurrentROB)
                    {
                        AddStations[i].Vk = mathStation.Result;
                        AddStations[i].Qk = DEFAULT_Q_VALUE;
                    }
                }

                for (int i = 0; i < MultStations.Count; i++)
                {
                    if (MultStations[i].Qj == mathStation.CurrentROB)
                    {
                        MultStations[i].Vj = mathStation.Result;
                        MultStations[i].Qj = DEFAULT_Q_VALUE;
                    }
                    if (MultStations[i].Qk == mathStation.CurrentROB)
                    {
                        MultStations[i].Vk = mathStation.Result;
                        MultStations[i].Qk = DEFAULT_Q_VALUE;
                    }
                }

                // Update ROB's value
                if (!mathStation.Exception)
                {
                    ROBs[mathStation.CurrentROB].Value = mathStation.Result;
                }
                else
                {
                    ROBs[mathStation.CurrentROB].Exception = true;
                }
                ROBs[mathStation.CurrentROB].Done = true;
                mathStation.Broadcasted = true;                
            }
            return mathStation;
        } 

        public ReorderBuffer Commit()
        {
            ReorderBuffer returnRob = null; 
            foreach (ReorderBuffer rob in ROBs)
            {
                if (rob.Done && rob.Index == CommitPointer)
                {
                    // No matter if RAT points to ROB update RF
                    RAT[ROBs[CommitPointer].RegisterFile].RegisterFile = ROBs[CommitPointer].Value;
                    // If RAT points to ROB update RAT to RF
                    for (int i = 0; i < RAT.Count; i++)
                    {
                        if (RAT[i].RAT == ROBs[CommitPointer].Index)
                        {
                            RAT[i].RAT = DEFAULT_RAT;
                        }
                    }
                    returnRob = rob;
                    break;
                }
            }
            if (returnRob != null)
            {
                _CommitPointer++;
            }
            return returnRob;
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

        public void ClearRATonCommit()
        {
            foreach(RatEntry ratRF in RAT)
            {
                ratRF.RAT = DEFAULT_RAT;
            }
        }
        
        public void ClearROBonCommit()
        {
            foreach(ReorderBuffer rob in ROBs)
            {
                rob.Done = false;
                rob.Exception = false;
                rob.RegisterFile = 0;
                rob.Value = 0;
            }
        }

        public void ClearRSonCommit()
        {
            foreach(ReservationStation rs in AddStations)
            {
                rs.Busy = false;
                rs.Dispatched = false;
                rs.Qj = DEFAULT_Q_VALUE;
                rs.Qk = DEFAULT_Q_VALUE;
                rs.Ready = true;
                rs.ROBIndex = 0;
                rs.Vj = 0;
                rs.Vk = 0;
            }
        }
    }
}
