using System;
using Infrastructure.Core.Player;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerJumpEvent : Infrastructure.Base.Event.Event
    {
        public PlayerModel player;
        public PlayerJumpEvent(PlayerModel player)
        {
            this.player = player;
        }
    }
}

