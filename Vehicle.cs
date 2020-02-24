using System;
using System.Collections.Generic;

namespace RCSimulator
{
    public class Vehicle
    {
        private string _testSuite;

        /// <summary>
        /// Current position of the vehicle.
        /// </summary>
        public Coord Position { get; set; }

        /// <summary>
        /// Current Heading the vehicle is faceing.
        /// </summary>
        public Heading Direction { get; set; }

        /// <summary>
        /// Available commands for the specific vehicle. Commands should be in upper case.
        /// </summary>
        protected List<string> allowedCommands;

        /// <summary>
        /// A string with the vehicles allowed commands.
        /// </summary>
        public string AllowedCommandsString
        {
            get
            {
                return allowedCommands != null ? string.Join(" ", allowedCommands.ToArray()) : "";
            }
        }

        public string Status { get; set; } = "No tests performed.";

        /// <summary>
        /// A string of commands to perform when test suite is run.
        /// </summary>
        public string TestSuite
        {
            get { return _testSuite; }
            set
            {
                if (string.IsNullOrEmpty(value) || !ValidTestSuite(value))
                {
                    throw new ArgumentException($"{value} is not a valid string of commands. Allowed commands are {AllowedCommandsString}");
                }
                _testSuite = value.ToUpper();

            }
        }


        /// <summary>
        /// Run the test suite one command at a time and return vehicles position after each step.
        /// </summary>
        public IEnumerable<Coord> RunTestSuite()
        {
            foreach (char command in TestSuite)
            {
                RunCommand(command);
                yield return Position;
            }
        }

        /// <summary>
        /// Run one specified command to the vehicle.
        /// </summary>
        /// <param name="command">Command to run.</param>
        public virtual void RunCommand(char command)
        {
            switch (command)
            {
                case 'L':
                    MakeTurn(left: true);
                    break;
                case 'R':
                    MakeTurn(left: false);
                    break;
                case 'F':
                    Move(forward: true);
                    break;
                case 'B':
                    Move(forward: false);
                    break;
            }
        }

        /// <summary>
        /// Move the vehicles position one position forward or backward.
        /// </summary>
        /// <param name="forward">Indicates direction of the movement.</param>
        protected virtual void Move(bool forward)
        {
            int x = Position.x;
            int y = Position.y;

            switch (Direction)
            {
                case Heading.North:
                    y = forward ? y + 1 : y - 1;
                    break;
                case Heading.East:
                    x = forward ? x + 1 : x - 1;
                    break;
                case Heading.South:
                    y = forward ? y - 1 : y + 1;
                    break;
                case Heading.West:
                    x = forward ? x - 1 : x + 1;
                    break;
            }
            Position = new Coord(x, y);
        }

        /// <summary>
        /// Chage vehicels direction by turning.
        /// </summary>
        /// <param name="left">True if turning left.</param>
        protected virtual void MakeTurn(bool left)
        {
            switch (Direction)
            {
                case Heading.North:
                    Direction = left ? Heading.West : Heading.East;
                    break;
                case Heading.East:
                    Direction = left ? Heading.North : Heading.South;
                    break;
                case Heading.South:
                    Direction = left ? Heading.East : Heading.West;
                    break;
                case Heading.West:
                    Direction = left ? Heading.South : Heading.North;
                    break;
            }
        }


        /// <summary>
        /// Check if given string contains only valid commands. If so return true.
        /// </summary>
        /// <param name="input">String with commands.</param>
        private bool ValidTestSuite(string testSuite)
        {
            foreach (char c in testSuite)
            {
                if (!allowedCommands.Contains(c.ToString().ToUpper()))
                {
                    return false;
                }
            }
            return true;
        }

        public void SetHeading(string heading)
        {
            switch (heading.ToUpper())
            {
                case "N":
                    Direction = Heading.North;
                    break;
                case "E":
                    Direction = Heading.East;
                    break;
                case "S":
                    Direction = Heading.South;
                    break;
                case "W":
                    Direction = Heading.West;
                    break;
                default:
                    throw new ArgumentException($"Provided heading {heading} is not allowed. Must be one of N, E, S, W.");
            }
        }

    }
}
