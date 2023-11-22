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
                if (input == ConsoleKey.D1)
                {
                    Console.WriteLine("successfully went online");
                }
                else if (input == ConsoleKey.D2)
                {
                    Console.WriteLine("successfully went offline");
                }

                Console.WriteLine("1. Go Online");
                Console.WriteLine("2. Go Offline");
                Console.WriteLine("3. Go Exit");

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

                Console.Clear();
            } while (input != ConsoleKey.D3 );


        }
    }
}
