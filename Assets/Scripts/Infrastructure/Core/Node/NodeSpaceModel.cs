using Infrastructure.Core.Ship;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Infrastructure.Core.Player
{
    [Serializable]
    public class NodeSpaceModel : Base.Model.Model
    {
        [JsonProperty("name")]
        public string Name;
        
        [JsonProperty("ships")]
        public Dictionary<int, ShipModel> Ships;
    }
}