using System;

namespace RCSimulator
{
    public class ConsoleSimulator : ISimulator
    {
        private readonly Environment environment;

        public ConsoleSimulator(Environment environment)
        {
            this.environment = environment;
            SetRoomSize();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            setVehicleStartingPosition(vehicle);
            while (!environment.AddVehicle(vehicle))
            {
                Console.WriteLine($"Provided position {vehicle.Position.ToString()} is not a valid starting position in the environment.");
                setVehicleStartingPosition(vehicle);
            }

            setVehicleTestSuite(vehicle);
        }

        public void Run()
        {
            if (environment.Vehicles.Count == 0)
            {
                Console.WriteLine("There are no Vehicles to test. Please add at least one.");
                return;
            }
            environment.RunAllTestSuites();
            PrintResult();
        }

        private void PrintResult()
        {
            foreach (Vehicle vehicle in environment.Vehicles)
            {
                if (vehicle.Status != null)
                {
                    Console.WriteLine(vehicle.Status);
                }
            }
        }

        private void setVehicleTestSuite(Vehicle vehicle)
        {
            while (true)
            {
                Console.WriteLine($"Enter commands to execute. Available commads are {vehicle.AllowedCommandsString}");
                try
                {
                    vehicle.TestSuite = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void setVehicleStartingPosition(Vehicle vehicle)
        {
            /**
             * Get the user to enter starting positions and heading of the vehicle.
             */
            while (true)
            {
                Coord position;
                Console.WriteLine("Enter starting position and the heading of the test vehicle.");
                string[] input = Console.ReadLine().Split(' ');

                if (input.Length == 3 && Int32.TryParse(input[0], out position.x) && Int32.TryParse(input[1], out position.y))
                {
                    try
                    {
                        vehicle.Position = position;
                        vehicle.SetHeading(input[2]);
                        break;  // Input was correct, exit while loop.
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else    // User input something other then two integers and a heading.
                {
                    Console.WriteLine("You must enter two integers for the starting position and a heading fot the vehicle. (e.g. 2 4 N)");
                }
            }
        }

        /**
         * Get the user to input size of the enviroment.
         */
        private void SetRoomSize()
        {
            while (true)
            {
                int x, y;
                Console.WriteLine("Please enter the size of the environment. Two positive integers for X and Y (e.g. 5 8).");
                string[] input = Console.ReadLine().Split(' ');

                // Check to make sure input is correct. Room size should be two integers > 1.
                if (input.Length == 2 && Int32.TryParse(input[0], out x) && Int32.TryParse(input[1], out y))
                {
                    try
                    {
                        environment.SetSize(x, y);
                        // Exit while loop when correct value for enviroment has been entered.
                        break;
                    }
                    catch (ArgumentException ex)    // At least one input integer was not correct.
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else    // User input something other then two integers.
                {
                    Console.WriteLine("You must enter two integers.");
                }

            }
        }
    }
}