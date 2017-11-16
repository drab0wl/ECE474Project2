using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Project1
{
    public partial class Form1 : Form
    {
        private Algorithm system;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            // Build up stations and RF
            for (int i = 0; i < (Algorithm.MAX_ADD_STATIONS + Algorithm.MAX_MULT_STATIONS); i++)
            {
                this.resStationsDGV.Rows.Add();
                this.resStationsDGV.Rows[i].HeaderCell.Value = "RS" + i;
            }
            for (int i = 0; i < Algorithm.MAX_RAT_ENTRIES; i++)
            {
                this.ratTableDGV.Rows.Add();
                this.ratTableDGV.Rows[i].HeaderCell.Value = "RF" + i;
            }
            for (int i = 0; i < Algorithm.MAX_ROBS; i++)
            {
                this.robDGV.Rows.Add();
                this.robDGV.Rows[i].HeaderCell.Value = "ROB" + i;
            }
        }

        /// <summary>
        /// Opens file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            OpenFileDialog openDx = new OpenFileDialog();
            if (openDx.ShowDialog() == DialogResult.OK)
            {
                if (openDx.CheckFileExists)
                {
                    // Clear all forms
                    ClearSystem();

                    this.stepButton.Enabled = true;
                    using (StreamReader reader = new StreamReader(openDx.OpenFile()))
                    {
                        // Get instructions and cycles
                        string instructionNumber = reader.ReadLine();
                        string cycleNumber = reader.ReadLine();
                        uint instructions;
                        uint.TryParse(instructionNumber, out instructions);
                        uint cycles;
                        uint.TryParse(cycleNumber, out cycles);

                        system = new Algorithm();
                        // Load instructions into Instruction Queue
                        Queue<int[]> instructionTokenArrays = new Queue<int[]>();
                        for (int i = 0; i < instructions; i++)
                        {
                            string[] instrTokens_str = reader.ReadLine().Split(' ');

                            int[] instrTokens = Array.ConvertAll(instrTokens_str, int.Parse);
                            instructionTokenArrays.Enqueue(instrTokens);                           
                        }
                        // Load values into RF
                        for (int i = 0; i < Algorithm.MAX_RAT_ENTRIES; i++)
                        {
                            string line = reader.ReadLine();
                            int rfVal;
                            int.TryParse(line, out rfVal);
                            RatEntry ratEntry = new RatEntry();
                            ratEntry.RegisterFile = rfVal;
                            ratEntry.RAT = Algorithm.DEFAULT_RAT;
                            system.RAT.Add(ratEntry);
                            AddValuesToRF(i, rfVal);
                        }

                        for (int i = 0; i < instructions; i++)
                        {
                            int[] instruction = instructionTokenArrays.Dequeue();
                            int rf1 = instruction[2];
                            int rf2 = instruction[3];
                            instruction[2] = system.RAT[instruction[2]].RegisterFile;
                            instruction[3] = system.RAT[instruction[3]].RegisterFile;

                            Instruction instr = new Instruction(instruction[0], instruction[1], instruction[2], instruction[3], rf1, rf2);
                            system.InstructionQueue.Enqueue(instr);
                            AddInstructionToDGV(instr);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Empties all tables of values
        /// </summary>
        private void ClearSystem()
        {
            this.textBox1.Text = "0";
            foreach (DataGridViewRow row in resStationsDGV.Rows)
            {
                foreach (DataGridViewTextBoxCell cell in row.Cells)
                {
                    cell.Value = null;
                }
            }
            foreach (DataGridViewRow row in ratTableDGV.Rows)
            {
                foreach (DataGridViewTextBoxCell cell in row.Cells)
                {
                    cell.Value = null;
                }
            }
            for (int i = instructionQueueDGV.Rows.Count; i > 0; i--)
            {
                instructionQueueDGV.Rows.RemoveAt(i - 1);
            }

            if (system != null)
            {
                if (system.InstructionQueue != null)
                {
                    system.InstructionQueue.Clear();
                }
                if (system.RAT != null)
                {
                    system.RAT.Clear();
                }
            }
        }

        /// <summary>
        /// Add instruction to Instruction Queue DataGridView
        /// </summary>
        /// <param name="instr">Instruction to be added</param>
        private void AddInstructionToDGV(Instruction instr)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.HeaderCell.Value = "I" + this.instructionQueueDGV.Rows.Count;
            for (int i = 0; i < 4; i++)
            {
                DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                row.Cells.Add(cell);
            }
            row.Cells[0].Value = Instruction.GetOpString(instr.Op);
            row.Cells[1].Value = instr.DestReg;
            row.Cells[2].Value = instr.V1;
            row.Cells[3].Value = instr.V2;
            this.instructionQueueDGV.Rows.Add(row);
        }

        /// <summary>
        /// Set RF values
        /// </summary>
        /// <param name="index">Register</param>
        /// <param name="value">Value</param>
        private void AddValuesToRF(int index, int value)
        {
            this.ratTableDGV.Rows[index].Cells[0].Value = value;
            this.ratTableDGV.Rows[index].Cells[1].Value = "Empty";
        }

        /// <summary>
        /// Fill reservation station DataGridView
        /// </summary>
        /// <param name="station">Reservation station information to fill DataGridView</param>
        private void AddValuesToRS(ReservationStation station)
        {
            if (station != null)
            {
                // Should this be rob?
                DataGridViewRow row = this.resStationsDGV.Rows[station.Index];
                row.Cells["rsBusyCol"].Value = station.Busy;
                row.Cells["rsOpCol"].Value = Instruction.GetOpString(station.Op);
                row.Cells["rsVjCol"].Value = station.Vj;
                row.Cells["rsVkCol"].Value = station.Vk;
                if (station.Qj != -1)
                {
                    row.Cells["rsQjCol"].Value = "ROB" + station.Qj;
                    row.Cells["rsVjCol"].Value = null;
                }
                if (station.Qk != -1)
                {
                    row.Cells["rsQkCol"].Value = "ROB" + station.Qk;
                    row.Cells["rsVkCol"].Value = null;
                }
            }
        }
        
        /// <summary>
        /// Handles step button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stepButton_Click(object sender, EventArgs e)
        {
            // Update cycle text box
            int cycle = int.Parse(this.textBox1.Text);
            cycle++;
            this.textBox1.Text = cycle.ToString();

            // Get destination register file index
            int RF = (int)Algorithm.DEFAULT_RAT;
            if (system.InstructionQueue.Count > 0)
            {
                RF = system.InstructionQueue.First().DestReg;
            }

            ReorderBuffer rob = system.Commit();
            if (rob != null)
            {
                CommitUpdateDGV(rob);
            }

            // Broadcast if any values are available
            int[] broadcastValues = system.Broadcast();

            if (broadcastValues[Algorithm.ROB_INDEX] != Algorithm.DEFAULT_RAT)
            {
                BroadcastUpdate(broadcastValues);
            }

            // Dispatch if any stations are ready
            ReservationStation dispatchStation = system.Dispatch();
            while (dispatchStation != null)
            {
                this.resStationsDGV.Rows[dispatchStation.Index].Cells["rsBusyCol"].Value = false;
                dispatchStation = system.Dispatch();
            }

            // Issue instructions if available
            int issuePointer = system.IssuePointer;
            ReservationStation issueStation = system.Issue();
            if (issueStation != null)
            {
                this.instructionQueueDGV.Rows.RemoveAt(0);
                AddValuesToRS(issueStation);
                this.robDGV.Rows[issuePointer].Cells["regCol"].Value = "RF" + RF;
                this.robDGV.Rows[issuePointer + 1].Cells["issueCol"].Value = this.textBox1.Text;
                this.ratTableDGV.Rows[RF].Cells["ratRATCol"].Value = "ROB" + issuePointer;
            }
        }

        /// <summary>
        /// Updates values in DataGridViews from broadcast
        /// </summary>
        /// <param name="values">Reservation Station, Value</param>
        private void BroadcastUpdate(int[] values)
        {
            // Update Reservation Stations
            foreach (DataGridViewRow row in this.resStationsDGV.Rows)
            {
                foreach(DataGridViewTextBoxCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.Equals("ROB" + values[Algorithm.ROB_INDEX]))
                    {
                        if (cell.ColumnIndex.Equals(4))
                        {
                            cell.Value = null;
                            row.Cells[2].Value = values[Algorithm.CAPTURE_VALUE];
                        }
                        else if (cell.ColumnIndex.Equals(5))
                        {
                            cell.Value = null;
                            row.Cells[3].Value = values[Algorithm.CAPTURE_VALUE];
                        }
                    }
                }
            }

            this.robDGV.Rows[values[Algorithm.ROB_INDEX]].Cells[1].Value = values[Algorithm.CAPTURE_VALUE];
            this.robDGV.Rows[values[Algorithm.ROB_INDEX]].Cells[2].Value = true;

            // Commented out.  This needs to be done in commit
            // Update RAT
            //foreach(DataGridViewRow row in this.ratTableDGV.Rows)
            //{
            //    foreach (DataGridViewTextBoxCell cell in row.Cells)
            //    {
            //        if (cell.Value != null && cell.Value.Equals("RS" + values[Algorithm.STATION_INDEX]))
            //        {
            //            row.Cells[0].Value = values[Algorithm.CAPTURE_VALUE];
            //            row.Cells[1].Value = "Empty";
            //        }
            //    }
            //}
        }

        private void CommitUpdateDGV(ReorderBuffer rob)
        {
            string currentRAT = this.ratTableDGV.Rows[rob.RegisterFile].Cells["ratRatCol"].Value.ToString();
            if (currentRAT.Equals("ROB" + rob.Index))
            {
                this.ratTableDGV.Rows[rob.RegisterFile].Cells["ratRatCol"].Value = "RF" + rob.RegisterFile.ToString();
            }
            this.ratTableDGV.Rows[rob.RegisterFile].Cells["ratRFCol"].Value = rob.Value;
            this.robDGV.Rows[rob.Index + 1].Cells["commitCol"].Value = this.textBox1.Text;
        }

        /// <summary>
        /// Exits application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }        
    }
}
