namespace RCSimulator
{
    /**
     * An empty environment to to use during a simulation.
     * 
     * Empty room can only handle one vehicle and there are no obstacles besides
     * a wall that surrounds the grid.
     */
    public class EmptyRoom : Environment
    {
        public override bool AddVehicle(Vehicle vehicle)
        {
            bool result = false;

            if (CoordInGrid(vehicle.Position))
            {
                Vehicles.Clear();
                Vehicles.Add(vehicle);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Run vehicles test suit and set appropiate status after execution.
        /// </summary>
        public override void RunAllTestSuites()
        {
            // Make sure there are a vehicle to run a test suite on.
            // For EmptyRoom there are only one possible vehicle.
            if (Vehicles.Count != 1) { return; }
            Vehicle vehicle = Vehicles[0];

            string status = "No test suite found.";
            if (vehicle.TestSuite != null)
            {
                bool crash = false;
                foreach (Coord position in vehicle.RunTestSuite())
                {
                    // There are no obstacles so enoguh to check vehicle still in grid.
                    if (!CoordInGrid(position))
                    {
                        crash = true;
                        break;
                    }

                }
                status = crash ?
                    $"Vehicle crashed into a wall at {vehicle.Position.ToString()}" :
                    $"Simulation was succesful. Vehicle ended at {vehicle.Position.ToString()} facing {vehicle.Direction}";
            }
            vehicle.Status = status;
        }
    }
}