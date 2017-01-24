using System;
using Infrastructure.Core.Resource;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerBoughtResourceEvent
    {
        public Player.PlayerModel player;
        public ResourceSlotModel resouceSlot;
        public PlayerBoughtResourceEvent(Player.PlayerModel player, ResourceSlotModel resourceSlot)
        {
            this.player = player;
            this.resouceSlot = resourceSlot;
        }
    }
}

