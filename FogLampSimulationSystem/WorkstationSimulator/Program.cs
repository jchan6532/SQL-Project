
/*
 * FILE : Program.cs
 * PROJECT : PROG3070 - Gerritt Hooyer, Justin Chan
 * PROGRAMMER : Gerritt Hooyer, Justin Chan
 * FIRST VERSION : 2023-11-20
 * DESCRIPTION :
 * Runs the simulator in Runner or Workstation mode.
 */
using System;

namespace WorkstationSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create our runner mode variable and check if the arg was passed.
            bool runnerMode = false;
            if (args.Length > 0)
            {
                if (args[0] == "-runner")
                {
                    runnerMode = true;
                }
            }

            // Check whether in Runner Mode
            if (runnerMode)
            {
                // If true, instantiate a simManager w/ no parameters
                // This creates a runner sim
                SimulationManager simManager = new SimulationManager();
                // While a key has not been pressed, run the simulation.
                while (!Console.KeyAvailable)
                {
                    TickRunner(simManager);
                }
            }
            else
            {
                // Create a variable for our Workstation ID
                int workstationId = 0;
                Console.Write("Enter your workstation ID: ");
                // Attempt to parse, exit on bad input
                if (!Int32.TryParse(Console.ReadLine(), out workstationId))
                {
                    return;
                }
                // Instantiate a simManager for a workstation and a random number generator
                // Rand is instantiated here to make our lives easier when checking for defects
                SimulationManager simManager = new SimulationManager(workstationId);
                Random rand = new Random();
                // While a key has not been pressed, run the simulation.
                while (!Console.KeyAvailable)
                {
                    TickWorkstation(simManager, rand);
                }
            }
        }

        /// <summary>
        /// Performs the functionality of a single tick of the Workstation Simulation.
        /// </summary>
        /// <param name="simManager">The SimualtionManager object instantiated w/ a Workstation ID.</param>
        /// <param name="rand">A Random object for random number generation w/o having to reseed on every pass.</param>
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
            // If we have gone over the amount of time that it takes to build a fan, do a defect check
            if (simManager.FanTickCount >= simManager.SimWorkstation.CurrentFanBuildTime)
            {
                // This will create a defectCheck such that if it is > 1, it will be a defect
                // This is done to accurately mode the defect rate as specified in the config.
                float defectCheck = (float)rand.NextDouble() * (1 + simManager.SimWorkstation.WorkstationEmployee.DefectRateModifier);
                // If the Workstation has enough parts, go ahead 'build' the defect or good part.
                if (simManager.SimWorkstation.HasEnoughParts)
                {
                    // Do our check, build the correct item.
                    if (defectCheck > 1)
                    {
                        simManager.SimWorkstation.BuildDefect();
                    }
                    else
                    {
                        simManager.SimWorkstation.BuildLamp();
                    }
                }
                // Reset out counter.
                simManager.FanTickCount = 0;
            }
        }

        /// <summary>
        /// Runs a single tick of the runner simulation.
        /// </summary>
        /// <param name="simManager">The SimulationManager object that is instantiated to simulate a runner.</param>
        static void TickRunner(SimulationManager simManager)
        {
            // Sleeps for the necessary amount of time based on tick rate and simulation speed
            // Simulation speed is a multiplier, meaning that a Simulation Speed of 2 will run @ 2x
            // realtime speed.
            simManager.Sleep();
            // Tick forward our refill counter
            simManager.RefillTickCount += simManager.TickIncrement;
            // Once enough ticks have gone by, refill the bins and reset the counter
            if (simManager.RefillTickCount >= simManager.RefillInterval)
            {
                simManager.SimRunner.RefillBins();
                simManager.RefillTickCount = 0;
            }
        }

    }
}

