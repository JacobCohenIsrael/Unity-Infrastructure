using System;
using Infrastructure.Core.Resource;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerBoughtResourceEvent
    {
        public Player.PlayerModel player;
        public PlayerBoughtResourceEvent(Player.PlayerModel player)
        {
            this.player = player;
        }
    }
}

