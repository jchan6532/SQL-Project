using System;

namespace WorkstationSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool runnerMode = false;
            if (args.Length > 0)
            {
                if (args[0] == "-runner")
                {
                    runnerMode = true;
                }
            }


            if (runnerMode)
            {
                SimulationManager simManager = new SimulationManager();
                while (!Console.KeyAvailable)
                {
                    TickRunner(simManager);
                }
            }
            else
            {
                int workstationId = 0;
                Console.Write("Enter your workstation ID: ");
                if (!Int32.TryParse(Console.ReadLine(), out workstationId))
                {
                    return;
                }
                SimulationManager simManager = new SimulationManager(workstationId);
                Random rand = new Random();
                

                while (!Console.KeyAvailable)
                {
                    TickWorkstation(simManager, rand);
                }
            }
        }

        static void TickWorkstation(SimulationManager simManager, Random rand)
        {
            // Sleeps for the necessary amount of time based on tick rate and simulation speed
            // Simulation speed is a multiplier, meaning that a Simulation Speed of 2 will run @ 2x
            // realtime speed.
            simManager.Sleep();
            Console.WriteLine($"\nWorkstation ID          : {simManager.SimWorkstation.WorkstationId}");
            // If there isn't an order, skip ticking the fan build timer forward
            if (simManager.SimWorkstation.CurrentOrder != null)
            {
                simManager.FanTickCount += simManager.TickIncrement;
                Console.WriteLine($"Current Order ID        : {simManager.SimWorkstation.CurrentOrder.OrderId}");
                Console.WriteLine($"Current Order Fulfilled : {simManager.SimWorkstation.CurrentOrder.OrderFulfilled}");
                Console.WriteLine($"Current Order Amount    : {simManager.SimWorkstation.CurrentOrder.OrderAmount}");
                Console.WriteLine($"Current Order Session Lamps Built : {simManager.SimWorkstation.CurrentOrderSession.LampsBuilt}");
                Console.WriteLine($"Current Order Session Defects Built : {simManager.SimWorkstation.CurrentOrderSession.DefectsBuilt}");

            }
            if (simManager.FanTickCount >= simManager.SimWorkstation.CurrentFanBuildTime)
            {
                float defectCheck = (float)rand.NextDouble() * (1 + simManager.SimWorkstation.WorkstationEmployee.DefectRateModifier);

                if (simManager.SimWorkstation.HasEnoughParts)
                {
                    if (defectCheck > 1)
                    {
                        simManager.SimWorkstation.BuildDefect();
                    }
                    else
                    {
                        simManager.SimWorkstation.BuildLamp();
                    }
                }
                simManager.FanTickCount = 0;
            }
        }
        static void TickRunner(SimulationManager simManager)
        {
            simManager.Sleep();
            simManager.RefillTickCount += simManager.TickIncrement;
            if (simManager.RefillTickCount >= simManager.RefillInterval)
            {
                simManager.SimRunner.RefillBins();
                simManager.RefillTickCount = 0;
            }
        }

    }
}

