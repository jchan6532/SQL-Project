/*
 * FILE : Bin.cs
 * PROJECT : PROG3070 - Gerritt Hooyer, Justin Chan
 * PROGRAMMER : Gerritt Hooyer, Justin Chan
 * FIRST VERSION : 2023-11-20
 * DESCRIPTION :
 * Runs the simulator in Runner or Workstation mode.
 */

namespace WorkstationSimulator
{
    /// <summary>
    /// Model class for representing one row entry in the database Bin table
    /// </summary>
    internal class Bin
    {
        #region Public Properties

        /// <summary>
        /// The bin ID for the current Bin
        /// </summary>
        public int BinId
        { 
            get;
            set;
        }

        /// <summary>
        /// The part ID/part type that is referenced by the current bin, represents what type of part is in this bin
        /// </summary>
        public int PartId
        { 
            get;
            set;
        }

        /// <summary>
        /// The number of parts in the current bin
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// The maximum capacity of part counts for the bin
        /// </summary>
        public int RefillAmount
        { 
            get;
            set;
        }

        /// <summary>
        /// The part name for the current bin, represents the name of the part that occupies the bin
        /// </summary>
        public string PartName { get; set; }

        #endregion
    }
}
