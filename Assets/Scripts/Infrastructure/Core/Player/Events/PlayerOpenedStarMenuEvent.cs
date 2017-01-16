using System;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerOpenedStarMenuEvent
    {
        public Player.PlayerModel player;
        public PlayerOpenedStarMenuEvent(Player.PlayerModel player)
        {
            this.player = player;
        }
    }
}

