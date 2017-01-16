using System;

namespace Infrastructure.Core.Ship.Part
{
    public class BasicEngine : ShipPart
    {
        protected override void AddStats()
        {
            AddStat(ShipStats.JumpDistance, 6f);
            AddStat(ShipStats.EnergyCapacity, 40f);
            AddStat(ShipStats.EnergyRegen, 15f);
        }
    }
}

