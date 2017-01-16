using System;
using Infrastructure.Core.Player;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerLandOnStarEvent : Infrastructure.Base.Event.Event
    {
        public PlayerModel player;
        public PlayerLandOnStarEvent(PlayerModel player)
        {
            this.player = player;
        }
    }
}

