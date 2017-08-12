using Newtonsoft.Json;
using System;

namespace Infrastructure.Core.Player.Events
{
    [Serializable]
    public class ShipLeftNodeEvent
    {
        [JsonProperty("playerId")]
        public int PlayerId;
    }
}