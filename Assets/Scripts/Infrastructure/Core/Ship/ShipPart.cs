using System;
using System.Collections.Generic;

namespace Infrastructure.Core.Ship
{
    public abstract class ShipPart
    {
        public Dictionary<ShipStats, int> stats;

        public ShipPart()
        {
            stats = new Dictionary<ShipStats, int>();
            AddStats();
        }

        public void AddStat(ShipStats statName, int value)
        {
            stats.Add(statName, value);
        }

        protected abstract void AddStats();
    }
}

