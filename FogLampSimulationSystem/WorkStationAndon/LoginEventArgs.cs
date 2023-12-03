using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkStationAndon
{
    public class LoginEventArgs : EventArgs
    {
        public int EmployeeID { get; }
        public LoginEventArgs(int employeeID)
        {
            EmployeeID = employeeID;
        }

    }
}
