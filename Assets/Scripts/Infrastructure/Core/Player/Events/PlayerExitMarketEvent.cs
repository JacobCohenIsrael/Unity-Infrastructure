using System;
using Infrastructure.Core.Player;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerExitMarketEvent : Infrastructure.Base.Event.Event
    {
        public PlayerModel player;
        public PlayerExitMarketEvent(PlayerModel player)
        {
            this.player = player;
        }
    }
}

