using System;
using System.Collections.Generic;

namespace RCSimulator
{
    public class MonsterTruck : Vehicle
    {
        public MonsterTruck()
        {
            allowedCommands = new List<string>() { "L", "R", "F", "B" };
        }
    }
}