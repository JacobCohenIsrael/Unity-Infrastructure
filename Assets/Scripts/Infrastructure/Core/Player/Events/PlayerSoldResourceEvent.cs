using System;
using Infrastructure.Core.Resource;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerSoldResourceEvent
    {
        public Player.PlayerModel player;
        public ResourceSlotModel resouceSlot;
        public PlayerSoldResourceEvent(Player.PlayerModel player, ResourceSlotModel resourceSlot)
        {
            this.player = player;
            this.resouceSlot = resourceSlot;
        }
    }
}

