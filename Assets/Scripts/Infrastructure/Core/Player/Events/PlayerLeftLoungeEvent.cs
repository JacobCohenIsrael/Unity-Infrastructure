using System;
using Infrastructure.Core.Player;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerLeftLoungeEvent : Infrastructure.Base.Event.Event
    {
        public PlayerModel player;
        public PlayerLeftLoungeEvent(PlayerModel player)
        {
            this.player = player;
        }
    }
}

