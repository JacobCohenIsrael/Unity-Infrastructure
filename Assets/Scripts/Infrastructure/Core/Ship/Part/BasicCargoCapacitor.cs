using System;

namespace Infrastructure.Core.Ship.Part
{
    public class BasicCargoCapacitor : ShipPart
    {
        protected override void AddStats()
        {
            AddStat(ShipStats.CargoCapacity, 6);
        }
    }
}

