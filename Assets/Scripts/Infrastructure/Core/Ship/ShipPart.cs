using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Infrastructure.Core.Ship
{
    [Serializable]
    public class ShipPart
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("partStats")]
        public ShipStats PartStats;
    }
}

