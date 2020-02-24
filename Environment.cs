using System;
using System.Collections.Generic;

namespace RCSimulator
{
    public class Environment
    {
        private int sizeX = 0;
        private int sizeY = 0;

        /// <summary>
        /// A list of all vehicels in the environment.
        /// </summary>
        public List<Vehicle> Vehicles { get; } = new List<Vehicle>();

        /// <summary>
        /// Set the size of the environment.
        /// </summary>
        /// <param name="sizeX">Horizontal size.</param>
        /// <param name="sizeY">Vertical size.</param>
        public void SetSize(int sizeX, int sizeY)
        {
            if (sizeX < 1 || sizeY < 1)
            {
                throw new ArgumentException("Each side of the environment must be an positive integer greater than 1.");
            }

            this.sizeX = sizeX;
            this.sizeY = sizeY;
        }

        /// <summary>
        /// Add a vehicle to the environment. Returns true if the vehicle was added.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        public virtual bool AddVehicle(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Run all vehicles test suits one command at a time.
        /// </summary>
        public virtual void RunAllTestSuites()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return true if provided coord is in the grid.
        /// </summary>
        /// <param name="position">The position to check.</param>
        protected bool CoordInGrid(Coord position)
        {
            return (position.x >= 0 && position.x < sizeX && position.y >= 0 && position.y < sizeY);
        }
    }
}