using Newtonsoft.Json;
using System;

namespace Infrastructure.Core.Player.Events
{
    [Serializable]
    public class ShipEnteredNodeEvent
    {
        [JsonProperty("node")]
        NodeSpaceModel NodeSpace;
    }
}