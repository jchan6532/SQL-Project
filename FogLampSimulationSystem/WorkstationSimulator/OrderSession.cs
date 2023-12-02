/*
 * FILE : OrderSession.cs
 * PROJECT : PROG3070 - Gerritt Hooyer, Justin Chan
 * PROGRAMMER : Gerritt Hooyer, Justin Chan
 * FIRST VERSION : 2023-11-20
 * DESCRIPTION :
 * Runs the simulator in Runner or Workstation mode.
 */

namespace WorkstationSimulator
{
    /// <summary>
    /// Model class for representing one row entry in the database Order Session table
    /// </summary>
    internal class OrderSession
    {
        #region Public Properties

        /// <summary>
        /// Work station ID for the order session
        /// </summary>
        public int WorkstationId
        { 
            get;
        }

        /// <summary>
        /// Order ID for the order session
        /// </summary>
        public int OrderId
        { 
            get;
        }

        /// <summary>
        /// Total lamps built for the order session
        /// </summary>
        public int LampsBuilt
        { 
            get;
        }

        /// <summary>
        /// Total defects builts for the order session
        /// </summary>
        public int DefectsBuilt
        { 
            get;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterized constructor that takes in a work station ID, order ID, lamps built, defects built, and instantiates a new
        /// Order Session object
        /// </summary>
        /// <param name="workstationId">work station ID</param>
        /// <param name="orderId">order ID</param>
        /// <param name="lampsBuilt">lamps built</param>
        /// <param name="defectsBuilt">defects built</param>
        public OrderSession(int workstationId, int orderId, int lampsBuilt, int defectsBuilt)
        {
            WorkstationId = workstationId;
            OrderId = orderId;
            LampsBuilt = lampsBuilt;
            DefectsBuilt = defectsBuilt;
        }

        #endregion
    }
}
