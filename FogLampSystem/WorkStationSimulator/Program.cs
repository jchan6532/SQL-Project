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
            WorkStation workStation = new WorkStation();
            ConsoleKey input = ConsoleKey.D3;

            do
            {
                Console.Clear();

                if (input == ConsoleKey.D1)
                {
                    PromptExpericeLevel(workStation);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"successfully went online - assigned to work station ID{workStation.WorkStationID}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (input == ConsoleKey.D2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("successfully went offline");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                PromptMainMenu(workStation);

                
            } while (input != ConsoleKey.D3 );


        }
        static void PromptMainMenu(WorkStation workStation)
        {
            ConsoleKey input = ConsoleKey.D3;

            Console.WriteLine("1. Go Online");
            Console.WriteLine("2. Go Offline");
            Console.WriteLine("3. Go Exit");
            Console.WriteLine("4. Start processing fog lamps");
            input = Console.ReadKey().Key;

            if (input == ConsoleKey.D1)
            {
                if (workStation.IsOnline())
                    throw new Exception("cannot go online when you are already online");

                workStation.GoOnline();
            }
            else if (input == ConsoleKey.D2)
            {
                if (!workStation.IsOnline())
                    throw new Exception("cannot go offline when you are already offline");

                workStation.GoOffline();
            }
            else if (input == ConsoleKey.D3 && workStation.IsOccupied)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nsuccessfully went offline");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (input == ConsoleKey.D4)
            {
                Console.Clear();
                workStation.ProcessWorkStation();
            }

        }

        static void PromptExpericeLevel(WorkStation workStation)
        {
            ConsoleKey input = ConsoleKey.NoName;

            List<string> employeeTypes = WorkStation.GetEmployeeTypes();
            do
            {
                Console.Clear();

                Console.WriteLine("What is your experience level");
                foreach (string employeeType in employeeTypes)
                {
                    Console.WriteLine($"{employeeTypes.IndexOf(employeeType)}. {employeeType}");
                }


                input = Console.ReadKey().Key;
                if (input == ConsoleKey.D1)
                {
                    workStation.EmployeeType = Constants.EmployeeType.New;
                }
                else if (input == ConsoleKey.D2)
                {
                    workStation.EmployeeType = Constants.EmployeeType.Super;
                }
                else if (input == ConsoleKey.D3)
                {
                    workStation.EmployeeType = Constants.EmployeeType.Experienced;
                }
            } while (!(input >= ConsoleKey.D1 && input <= ConsoleKey.D3));
        }

    }
}
