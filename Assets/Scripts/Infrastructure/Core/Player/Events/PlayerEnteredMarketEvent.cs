using System.Collections.Generic;
using Infrastructure.Core.Resource;
using Infrastructure.Core.Star;
using Newtonsoft.Json;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerEnteredMarketEvent : Infrastructure.Base.Event.Event
    {
        [JsonProperty("player")]
        public PlayerModel Player;

        [JsonProperty("resourceSlotList")]
        public Dictionary<string, ResourceSlotModel> ResourceSlotList;
    }
}

