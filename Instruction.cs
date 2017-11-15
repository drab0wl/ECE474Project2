namespace Project1
{
    /// <summary>
    /// Operation enumeration
    /// </summary>
    public enum OP
    {
        Add,
        Sub,
        Mult,
        Div
    }

    /// <summary>
    /// Class used for each instruction
    /// </summary>
    class Instruction
    {
        public int Op { get; }
        public int DestReg { get; }
        public int V1 { get; }
        public int V2 { get; }
        public int RF1 { get; }
        public int RF2 { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="op">Operation</param>
        /// <param name="dest">Destination Register</param>
        /// <param name="v1">Source Register 1</param>
        /// <param name="v2">Source Register 2</param>
        public Instruction( int op, int dest, int v1, int v2, int rf1, int rf2)
        {
            this.Op = op;
            this.DestReg = dest;
            this.V1 = v1;
            this.V2 = v2;
            this.RF1 = rf1;
            this.RF2 = rf2;
        }

        /// <summary>
        /// Translates integer to an operation string
        /// </summary>
        /// <param name="op">Operation Integer</param>
        /// <returns>Operation STring</returns>
        public static string GetOpString(int op)
        {
            string operation;
            switch (op)
            {
                case (int)OP.Add:
                    operation = "Add";
                    break;
                case (int)OP.Sub:
                    operation = "Sub";
                    break;
                case (int)OP.Mult:
                    operation = "Mult";
                    break;
                case (int)OP.Div:
                    operation = "Div";
                    break;
                default:
                    operation = "Error";
                    break;
            }
            return operation;
        }
    }
}
