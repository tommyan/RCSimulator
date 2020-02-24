namespace RCSimulator
{
    public interface ISimulator
    {
        /// <summary>
        /// Add a vehicle to the simulation. 
        /// </summary>
        /// <param name="vehicle">Vehicle to add</param>
        void AddVehicle(Vehicle vehicle);

        /// <summary>
        /// Run the simulator
        /// </summary>
        void Run();
    }
}