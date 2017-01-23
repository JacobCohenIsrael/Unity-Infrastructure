using System;

namespace Infrastructure.Core.Ship.Part
{
    public class BasicEngine : ShipPart
    {
        protected override void AddStats()
        {
            AddStat(ShipStats.JumpDistance, 6);
            AddStat(ShipStats.EnergyCapacity, 40);
            AddStat(ShipStats.EnergyRegen, 5);
        }
    }
}

