using System;
using Infrastructure.Core.Resource;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerBoughtResourceEvent
    {
        public Player.PlayerModel player;
        public BuyResourceModel resource;

        public PlayerBoughtResourceEvent(Player.PlayerModel player, BuyResourceModel resource)
        {
            this.player = player;
            this.resource = resource;
        }
    }
}

