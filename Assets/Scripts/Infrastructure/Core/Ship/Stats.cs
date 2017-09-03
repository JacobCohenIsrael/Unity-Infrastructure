using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Infrastructure.Core.Ship
{
    [Serializable]
    public class Stats
    {
        [JsonProperty("hull")] 
        public int Hull;

        [JsonProperty("shieldRegen")]
        public int ShieldRegen;
        
        [JsonProperty("shieldCapacity")]
        public int ShieldCapacity;

        [JsonProperty("energyRegen")]
        public int EnergyRegen;

        [JsonProperty("energyCapacity")]
        public int EnergyCapacity;

        [JsonProperty("jumpDistance")]
        public int JumpDistance;

        [JsonProperty("cargoCapacity")]
        public int CargoCapacity;
    }
}