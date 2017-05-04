using System;
using Infrastructure.Core.Resource;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerSoldResourceEvent
    {
        public Player.PlayerModel player;
        public PlayerSoldResourceEvent(Player.PlayerModel player)
        {
            this.player = player;
        }
    }
}

