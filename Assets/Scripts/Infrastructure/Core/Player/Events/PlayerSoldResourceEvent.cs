using System;
using Infrastructure.Core.Resource;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerSoldResourceEvent
    {
        public Player.PlayerModel player;
        public SellResourceModel resource;
        public PlayerSoldResourceEvent(Player.PlayerModel player, SellResourceModel resource)
        {
            this.player = player;
            this.resource = resource;
        }
    }
}

