using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WorkStationSimulator.Services;

namespace WorkStationSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            string employeeId = PromptEmployeeId();

            WorkStation workStation = new WorkStation(employeeId);
        }

        static string PromptEmployeeId()
        {
            Console.Write("Enter your employee ID: ");
            string empId = Console.ReadLine();

            return empId;
        }
    }
}
