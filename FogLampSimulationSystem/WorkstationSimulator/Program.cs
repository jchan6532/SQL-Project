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

            if (args.Length > 0)
            {
                if (args[0] == "-runner")
                {
                    StartRunner();
                }
            }
            else
            {
                StartWorkstation();
            }
        }

        static void StartWorkstation()
        {
            int workstationId = 0;
            Console.Write("Enter your workstation ID: ");

            if (!Int32.TryParse(Console.ReadLine(), out workstationId))
            {
                return;
            }

            SimulationManager simManager = new SimulationManager(workstationId);
            Random rand = new Random();
            long elapsed = 0;

            while (!Console.KeyAvailable)
            {
                Console.WriteLine($"\nTime Elapsed: {elapsed}(s)");
                int tickIncrement = 60 / simManager.TickRate;
                elapsed += tickIncrement;
                if (simManager.SimulationSpeed > 0)
                {
                    int sleepTime = (int)(tickIncrement * 1000 / simManager.SimulationSpeed);
                    Thread.Sleep(sleepTime);
                }
                if (simManager.SimWorkstation.CurrentOrder == null)
                {
                    continue;
                }
                simManager.FanTickCount += tickIncrement;

                // This warns the runner that there are empty parts, beginning the incrementing of the
                // refillTickCount. The idea is that the runner has only now begun their run to grab more parts,
                // and there will be a 5 minute delay before they get the parts and drop them off, instead of instantly
                // delivering parts to the worker every 5 minutes if they need them.
                if (simManager.SimWorkstation.ShouldWarnRunner)
                {
                    simManager.RefillTickCount += tickIncrement;
                }

                Console.WriteLine($"\nWorkstation ID          : {simManager.SimWorkstation.WorkstationId}");
                Console.WriteLine($"Current Order ID        : {simManager.SimWorkstation.CurrentOrder.OrderId}");
                Console.WriteLine($"Current Order Fulfilled : {simManager.SimWorkstation.CurrentOrder.OrderFulfilled}");
                Console.WriteLine($"Current Order Amount    : {simManager.SimWorkstation.CurrentOrder.OrderAmount}");
                Console.WriteLine($"Current Order Session Lamps Built : {simManager.SimWorkstation.CurrentOrderSession.LampsBuilt}");
                Console.WriteLine($"Current Order Session Defects Built : {simManager.SimWorkstation.CurrentOrderSession.DefectsBuilt}");

                if (simManager.FanTickCount >= simManager.SimWorkstation.CurrentFanBuildTime)
                {
                    float defectCheck = (float)rand.NextDouble();
                    if (simManager.SimWorkstation.HasEnoughParts)
                    {
                        if (simManager.SimWorkstation.WorkstationEmployee.DefectRateModifier < defectCheck)
                        {
                            simManager.SimWorkstation.BuildLamp();
                        }
                        else
                        {
                            simManager.SimWorkstation.BuildDefect();
                        }
                    }
                    simManager.FanTickCount = 0;
                }
                if (simManager.RefillTickCount >= simManager.RefillInterval)
                {
                    Console.WriteLine("\nRefilled the bins!\n");
                    simManager.SimWorkstation.RefillBins();
                    simManager.RefillTickCount = 0;
                }
            }
        }

        static void StartRunner()
        {

        }
    }
}
