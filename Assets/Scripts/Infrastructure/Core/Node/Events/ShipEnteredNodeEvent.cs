using Infrastructure.Core.Ship;
using Newtonsoft.Json;
using System;

namespace Infrastructure.Core.Player.Events
{
    [Serializable]
    public class ShipEnteredNodeEvent
    {
        [JsonProperty("ship")]
        public ShipModel ShipModel;

        [JsonProperty("playerId")]
        public int PlayerId;
    }
}