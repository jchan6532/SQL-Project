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

            PrintLoggedInMessage(workStation);
            workStation.ProcessWorkStation();
        }

        static void PrintLoggedInMessage(WorkStation workStation)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Welcome back ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(workStation.EmployeeName);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($" you are assigned to work station ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(workStation.WorkStationID);
            Console.WriteLine();
        }

        static string PromptEmployeeId()
        {
            Console.Write("Enter your employee ID: ");
            string empId = Console.ReadLine();

            return empId;
        }
    }
}
