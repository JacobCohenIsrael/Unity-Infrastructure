using System.Collections.Generic;
using Infrastructure.Core.Resource;
using Infrastructure.Core.Star;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerEnteredMarketEvent : Infrastructure.Base.Event.Event
    {
        public PlayerModel Player;
        public Dictionary<string, ResourceSlotModel> ResourceSlotList;
    }
}

