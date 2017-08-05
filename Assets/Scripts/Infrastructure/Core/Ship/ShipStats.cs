using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Infrastructure.Core.Ship
{
    [Serializable]
    public class ShipStats
    {
        [JsonProperty("hull")]
        public int Hull = 0;

        public int ShieldRegen  = 0;
        public int ShieldCapacity = 0;

        [JsonProperty("energyRegen")]
        public int EnergyRegen = 0;

        [JsonProperty("energyCapacity")]
        public int EnergyCapacity = 0;

        [JsonProperty("jumpDistance")]
        public int JumpDistance = 0;

        [JsonProperty("cargoCapacity")]
        public int CargoCapacity;
    }
}