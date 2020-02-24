using System;

namespace RCSimulator
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            /**
             * Choose environment and simulator to test.
             */
            Environment environment = new EmptyRoom();
            ISimulator simulator = new ConsoleSimulator(environment);

            // Add at least on vehicle to the simulator. 
            simulator.AddVehicle(new MonsterTruck());
            simulator.Run();
        }
    }
}
