using System;
using System.Collections.Generic;

namespace Infrastructure.Core.Ship
{
    public abstract class ShipPart
    {
        public Dictionary<ShipStats, float> stats;

        public ShipPart()
        {
            stats = new Dictionary<ShipStats, float>();
            AddStats();
        }

        public void AddStat(ShipStats statName, float value)
        {
            stats.Add(statName, value);
        }

        protected abstract void AddStats();
    }
}

