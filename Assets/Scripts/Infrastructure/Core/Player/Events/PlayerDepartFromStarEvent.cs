using System;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerDepartFromStarEvent
    {
        public Player.PlayerModel player;
        public PlayerDepartFromStarEvent(Player.PlayerModel player)
        {
            this.player = player;
        }
    }
}

