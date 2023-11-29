using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkstationSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SimulationManager simManager = new SimulationManager(1);
            Random rand = new Random();
            int elapsed = 0;

            while (!Console.KeyAvailable)
            {
                int tickIncrement = 60 / simManager.TickRate;

                simManager.FanTickCount += tickIncrement;
                simManager.RefillTickCount += tickIncrement;
                elapsed += tickIncrement;

                if (simManager.FanTickCount >= simManager.SimWorkstation.CurrentFanBuildTime)
                {
                    float defectCheck = (float)rand.NextDouble();
                    if (simManager.SimWorkstation.WorkstationEmployee.DefectRateModifier < defectCheck)
                    {
                        Console.WriteLine("Built a fan!");
                    }
                    else
                    {
                        Console.WriteLine("Built a defect!");
                    }
                    simManager.FanTickCount = 0;
                }

                if (simManager.RefillTickCount >= simManager.RefillInterval)
                {
                    Console.WriteLine("Refilled the bins!");
                    simManager.RefillTickCount = 0;
                }

                if (simManager.SimulationSpeed > 0)
                {
                    int sleepTime = (int)(tickIncrement * 1000 / simManager.SimulationSpeed);
                    Thread.Sleep(sleepTime);
                }

                Console.WriteLine($"Time Elapsed: {elapsed}(s)");
            }

        }
    }
}
