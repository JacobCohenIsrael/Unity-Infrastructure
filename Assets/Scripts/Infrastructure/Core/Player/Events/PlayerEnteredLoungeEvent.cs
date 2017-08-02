using System;
using Infrastructure.Core.Player;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerEnteredLoungeEvent : Infrastructure.Base.Event.Event
    {
        public PlayerModel player;
        public PlayerEnteredLoungeEvent(PlayerModel player)
        {
            this.player = player;
        }
    }
}

