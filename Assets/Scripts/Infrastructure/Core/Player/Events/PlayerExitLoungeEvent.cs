using System;
using Infrastructure.Core.Player;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerExitLoungeEvent : Infrastructure.Base.Event.Event
    {
        public PlayerModel player;
        public PlayerExitLoungeEvent(PlayerModel player)
        {
            this.player = player;
        }
    }
}

